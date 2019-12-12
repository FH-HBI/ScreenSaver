using System;
using System.Runtime.InteropServices;
using Gtec.Unicorn;

namespace UnicornNetAcquisitionExample
{
    class Program
    {

        string helper = "";
      
        static void Main(string[] args)
        {
            Console.WriteLine("API Version: " + Unicorn.GetApiVersion());

            Unicorn _device = new Unicorn("UN-2019.02.86");
            uint FrameLength = 1;
            uint numberOfAcquiredChannels = _device.GetNumberOfAcquiredChannels();
            byte[] receiveBuffer = new byte[FrameLength * sizeof(float) * numberOfAcquiredChannels];
            GCHandle receiveBufferHandle = GCHandle.Alloc(receiveBuffer, GCHandleType.Pinned);
            _device.StartAcquisition(false);

            //while(true)
            for (int i = 0; i < 100; i++)
            {
                _device.GetData(FrameLength, receiveBufferHandle.AddrOfPinnedObject(), (uint)(receiveBuffer.Length / sizeof(float)));
               for(int k = 0; k < 17; k++)
                {
                    byte[] tmp = new byte[4];
                    for( int j = 4 * k + 0; j < 4*k + 4; j++)
                    {
                        tmp[j % 4] = receiveBuffer[j];
                    }
                    float result = BitConverter.ToSingle(tmp, 0);
                    Console.Write( result + " " );


                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();

            _device.StopAcquisition();

            Console.ReadLine();
        }
    }
}