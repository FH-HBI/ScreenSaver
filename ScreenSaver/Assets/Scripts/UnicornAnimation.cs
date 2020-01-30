using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Gtec.Unicorn;
using DSP;
public class UnicornAnimation : MonoBehaviour
{
    [SerializeField] string testFileName;
    [SerializeField] Text text;
    [SerializeField] bool useRecordedFile;
    [SerializeField] Button toggleButton;

    private ParticleSystem ps = null;
    private TestCSVParser csvP = null;
    private Unicorn unicornDevice = null;

    private uint FrameLength;
    private float[] result = new float[8];
    private byte[] receiveBuffer;
    private GCHandle receiveBufferHandle;

    private bool animationRunning = true;
    private bool acquisitionRunning = false;

    public List<float[]> arrays = new List<float[]>();

    private double[] eeg1Buffer = new double[60];
    private int startFrames = 480;
    private double startAvg;
    private int count;
    private int pos = 0;
    private bool baselineFound = false;

    void Start()
    {
        if (useRecordedFile)
        {
            print("using recorded file");
            toggleButton.GetComponentInChildren<Text>().text = "Recorded file active";
            initTestFileParser();
        }
        else
        {
            print("using live capture");
            toggleButton.GetComponentInChildren<Text>().text = "Live capture active";
            initUnicorn();
        }
        ps = GetComponent<ParticleSystem>();
        // init 8 buffer arrays
        for (int i = 0; i < 8; i++)
        {
            arrays.Add(new float[60]);
        }
    }
    private void clearBufferArrays()
    {
        arrays = new List<float[]>();
        for (int i = 0; i < 8; i++)
        {
            arrays.Add(new float[60]);
        }
        pos = 0;
    }
    private void initUnicorn()
    {
        count = 0;
        startAvg = 0;
        unicornDevice = new Unicorn("UN-2019.02.86");
        print(unicornDevice.GetDeviceInformation().DeviceVersion);
        FrameLength = 1;
        uint numberOfAcquiredChannels = unicornDevice.GetNumberOfAcquiredChannels();
        print(numberOfAcquiredChannels);
        receiveBuffer = new byte[FrameLength * sizeof(float) * numberOfAcquiredChannels];
        receiveBufferHandle = GCHandle.Alloc(receiveBuffer, GCHandleType.Pinned);
        unicornDevice.StartAcquisition(false);       
        acquisitionRunning = true;
    }
    private void initTestFileParser()
    {
        csvP = new TestCSVParser("Assets/TestFiles/" + testFileName);
    }
    private void OnDestroy()
    {
        try
        {
            unicornDevice.StopAcquisition();
        }
        catch (Exception e)
        {
            print(e.Message);
        }
        unicornDevice.Dispose();
        receiveBufferHandle.Free();
    }
    public void toggleSource()
    {
        if (useRecordedFile) // switch from recorded file to live capture
        {
            clearBufferArrays();
            toggleButton.GetComponentInChildren<Text>().text = "Live capture active";
            print("switched to live capture");
            useRecordedFile = false;
            initUnicorn();
            acquisitionRunning = true;
        }
        else // switch from live capture to recorded file
        {
            toggleButton.GetComponentInChildren<Text>().text = "Recorded file active";
            print("switched to test file");
            useRecordedFile = true;
            initTestFileParser();
            try
            {
                unicornDevice.StopAcquisition();
            }
            catch (Exception e)
            {
                print(e.Message);
            }
            unicornDevice.Dispose();
            unicornDevice = null;
            acquisitionRunning = false;
        }
    }
    void Update()
    {
        // ESCAPE: Pause the animation (does not stop acquisition), show toggle button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (animationRunning)
            {
                animationRunning = false;
                toggleButton.gameObject.SetActive(true);
                text.text += "\nPAUSIERT";
                Time.timeScale = 0f;
            }
            else
            {
                toggleButton.gameObject.SetActive(false);
                animationRunning = true;
                Time.timeScale = 1f;
            }
        }

        float[] values = new float[8];

        // for non-live testing purposes, read line of data from prerecorded file
        if (useRecordedFile)
            values = csvP.ReadLine(8);
        else if (acquisitionRunning)
            values = ReadUnicornData();

        if (animationRunning)
        {
            if (useRecordedFile) // if using our test file
            {
                for (int i = 0; i < 8; i++)
                {
                    arrays[i][pos] = Math.Abs(values[i] / 2000000) + 3; // normalize values in some way, save them in the buffer channels
                }
            }              

            if (!useRecordedFile)
            {
                double value0 = Math.Abs(values[0]); // filtered value of eeg1

                if (count < startFrames)  // find an initial baseline value
                {
                    if (count > 59)
                        startAvg += value0 / (startFrames-60);
                    count++;
                }
                else
                {
                    baselineFound = true;
                    eeg1Buffer[pos] = value0;  // filtered value of eeg1
                }
            }

            // move to next buffer position for next update call
            pos = (pos + 1) % arrays[0].Length;
            // if all buffers are full (every time 60 values have been read)
            if (pos == 0)
            {
                float arrayMax = 0;
                if (useRecordedFile)
                {
                    // find the max value out of all 8 buffers
                    for (int i = 0; i < 8; i++)
                    {
                        if (arrayMax < arrays[i].Max())
                            arrayMax = arrays[i].Max();
                    }
                }
                else
                {
                    // difference of buffer max and baseline, scaled down
                    if (baselineFound) arrayMax = (Math.Abs((float)eeg1Buffer.Max() - (float)startAvg)) / 30 + 3;
                }

                // change noise strength to array max
                var noise = ps.noise;
                noise.strength = arrayMax;
                print(arrayMax);

                // on-screen debug text
                text.text = arrayMax.ToString("N2") + "\nbaseline: " + startAvg.ToString("N2");
                if (acquisitionRunning) text.text += "\nacquisition running";
            }
        }
    }

    private float[] ReadUnicornData()
    {
        try
        {
            unicornDevice.GetData(FrameLength, receiveBufferHandle.AddrOfPinnedObject(), (uint)(receiveBuffer.Length / sizeof(float)));
        }
        
        catch (Exception e)
        {
            text.text = e.Message;
            receiveBuffer = new byte[FrameLength * sizeof(float) * 17];
            receiveBufferHandle = GCHandle.Alloc(receiveBuffer, GCHandleType.Pinned);
        }

        for (int k = 0; k < 8; k++)
        {
            byte[] tmp = new byte[4];
            for (int j = 4 * k + 0; j < 4 * k + 4; j++)
            {
                tmp[j % 4] = receiveBuffer[j];
            }
            result[k] = BitConverter.ToSingle(tmp, 0);
        }
        return result;
    }
}
