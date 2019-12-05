using System;
using System.Runtime.InteropServices;
using Gtec.Unicorn;

namespace UnicornNetAcquisitionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("API Version: " + Unicorn.GetApiVersion());

            Unicorn _device = new Unicorn("UN-2019.02.86");
            uint FrameLength = 1;
            uint numberOfAcquiredChannels = _device.GetNumberOfAcquiredChannels();
            byte[] receiveBuffer = new byte[FrameLength * sizeof(float) * numberOfAcquiredChannels];
            GCHandle receiveBufferHandle = GCHandle.Alloc(receiveBuffer, GCHandleType.Pinned);
            _device.StartAcquisition(false);

            while (true)
            {
                _device.GetData(FrameLength, receiveBufferHandle.AddrOfPinnedObject(), (uint)(receiveBuffer.Length / sizeof(float)));


            }

            Console.ReadLine();
        }
    }
}