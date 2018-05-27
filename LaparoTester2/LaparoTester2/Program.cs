using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LaparoCommunicator;

namespace LaparoTester2
{
    class Program
    {
        static void Main()
        {
            using (var comm = CommunicatorFactory.GetCommunicator())
            {
                for (long i = 0; i < 100; i++)
                {
                    Console.WriteLine(comm.GetDataInQuaternion());
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}
