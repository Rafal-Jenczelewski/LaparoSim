using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaparoCommunicator;

namespace LaparoTester2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var comm = CommunicatorFactory.GetCommunicator())
            {
                for (long i = -1000000; i < 2000; i++)
                    Console.WriteLine(comm.GetDataInQuaternion());
            }
            Console.ReadKey();
        }
    }
}
