using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaparoCommunicator;

namespace LaparoTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var comm = LaparoCommunicator.CommunicatorFactory.GetMock(@"C:/data/"))
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(comm.GetDataInQuaternion());
                    Console.WriteLine(comm.GetDataInEuler());
                }       
                Console.ReadKey();
            }
        }
    }
}
