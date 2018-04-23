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
                for (long i = Int64.MinValue; i < Int64.MaxValue; i++)
                        ;// Console.WriteLine("iter" + i + j);
            }
            Console.ReadKey();
        }
    }
}
