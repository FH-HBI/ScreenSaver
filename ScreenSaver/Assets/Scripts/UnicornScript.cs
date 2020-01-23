using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Gtec.Unicorn;

public class UnicornScript : MonoBehaviour
{
    [SerializeField] Text text;

    private ParticleSystem ps = null;
    private Unicorn unicornDevice = null;

    uint FrameLength;
    byte[] receiveBuffer;
    GCHandle receiveBufferHandle;
    private bool animationRunning = true;

    public List<float[]> arrays = new List<float[]>();

    private int pos = 0;

    void Start()
    {
        unicornDevice = new Unicorn("UN-2019.02.86");
        print(unicornDevice.GetDeviceInformation().DeviceVersion);
        FrameLength = 1;
        uint numberOfAcquiredChannels = unicornDevice.GetNumberOfAcquiredChannels();
        print(numberOfAcquiredChannels);
        receiveBuffer = new byte[FrameLength * sizeof(float) * numberOfAcquiredChannels];
        receiveBufferHandle = GCHandle.Alloc(receiveBuffer, GCHandleType.Pinned);

        unicornDevice.StartAcquisition(false);

        ps = GetComponent<ParticleSystem>();

        for (int i = 0; i < 8; i++)
        {
            arrays.Add(new float[60]);
        }
    }
    private void OnDestroy()
    {
        unicornDevice.StopAcquisition();
    }

    void Update()
    {
        if (animationRunning)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                unicornDevice.StopAcquisition();
                animationRunning = false;
                text.text += "\nPAUSIERT";
                print("Animation pausiert");
                Time.timeScale = 0f;
            }

            float[] values;

            // particle system modules
            var noise = ps.noise;

            // read values from each EEG channel into a buffer:
            values = ReadUnicornData();


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

                // manipulate noise module strength
                noise.strength = arrayMax;

                // on-screen debug text
                text.text = "Höchster Wert auf allen Kanälen:\n" + arrayMax.ToString("F2");
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                unicornDevice.StartAcquisition(false);
                animationRunning = true;
                Time.timeScale = 1f;
                print("Animation fortgesetzt.");
            }
        }

    }

    private float[] ReadUnicornData()
    {
        float[] result = new float[17];
        unicornDevice.GetData(FrameLength, receiveBufferHandle.AddrOfPinnedObject(), (uint)(receiveBuffer.Length / sizeof(float)));
        for (int k = 0; k < 17; k++)
        {
            byte[] tmp = new byte[4];
            for (int j = 4 * k; j < 4 * k + 4; j++)
            {
                tmp[j % 4] = receiveBuffer[j];
            }
            result[k] = BitConverter.ToSingle(tmp, 0);

        }
        return result;
    }
}
