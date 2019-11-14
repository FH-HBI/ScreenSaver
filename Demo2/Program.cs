using System;
using Gtec.Unicorn;

namespace UnicornNetAcquisitionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("API Version: " + Unicorn.GetApiVersion());
            Console.ReadLine();
        }
    }
}