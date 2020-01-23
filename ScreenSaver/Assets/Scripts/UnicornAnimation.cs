using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Gtec.Unicorn;

public class UnicornAnimation : MonoBehaviour
{
    [SerializeField] string testFileName;
    [SerializeField] Text text;
    [SerializeField] bool useRecordedFile = false;

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

    private int pos = 0;

    void Start()
    {
        if (useRecordedFile)
            csvP = new TestCSVParser("Assets/TestFiles/" + testFileName);

        else
        {
            unicornDevice = new Unicorn("UN-2019.02.86");
            print(unicornDevice.GetDeviceInformation().DeviceVersion);
            FrameLength = 1;
            uint numberOfAcquiredChannels = unicornDevice.GetNumberOfAcquiredChannels();
            print(numberOfAcquiredChannels);
            receiveBuffer = new byte[FrameLength * sizeof(float) * numberOfAcquiredChannels];
            receiveBufferHandle = GCHandle.Alloc(receiveBuffer, GCHandleType.Pinned);
        }

        ps = GetComponent<ParticleSystem>();

        for (int i = 0; i < 8; i++)
        {
            arrays.Add(new float[60]);
        }
    }
    private void OnDestroy()
    {
        if (acquisitionRunning)
            unicornDevice.StopAcquisition();
        unicornDevice.Dispose();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (acquisitionRunning) unicornDevice.StopAcquisition();
            else unicornDevice.StartAcquisition(false);
            acquisitionRunning = !acquisitionRunning;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animationRunning)
            {
                animationRunning = false;
                text.text += "\nPAUSIERT";
                Time.timeScale = 0f;
            }
            else
            {
                animationRunning = true;
                Time.timeScale = 1f;
            }

        }

        float[] values = new float[8];

        // particle system modules
        var noise = ps.noise;

        // read values from each EEG channel into a buffer

        // for non-live testing purposes, read line of data from prerecorded file
        if (useRecordedFile)
        {
            values = csvP.ReadLine(8);
        }

        if (acquisitionRunning) values = ReadUnicornData();
        


        for (int i = 0; i < 8; i++)
        {
            print(values[i]);
            // normalize values in some way, save them in the buffer channels (TODO: find good way to normalize)
            arrays[i][pos] = Math.Abs(values[i] * 1 / 2000000) + 3;
        }

        pos = (pos + 1) % arrays[0].Length;

        // if all buffers are full (every time 60 values have been read)
        if (pos == 0)
        {

            // find the max value out of all 8 buffers
            float arrayMax = 0;
            for (int i = 0; i < 8; i++)
            {
                if (arrayMax < arrays[i].Max())
                {
                    arrayMax = arrays[i].Max();
                }
            }

            // print normalized array max
            // print(arrayMax);
            // manipulate noise module strength
            noise.strength = arrayMax;

            // on-screen debug text
            text.text = "Höchster Wert auf allen Kanälen:\n" + arrayMax.ToString("F2");
            if (acquisitionRunning) text.text += "\nacquisition running";
        }
    }


    private float[] ReadUnicornData()
    {
        unicornDevice.GetData(FrameLength, receiveBufferHandle.AddrOfPinnedObject(), (uint)(receiveBuffer.Length / sizeof(float)));
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
