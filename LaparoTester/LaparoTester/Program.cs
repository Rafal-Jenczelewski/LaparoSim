using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                using (var comm = LaparoCommunicator.CommunicatorFactory.GetMock(Path.Combine(Directory.GetCurrentDirectory(), "/data")))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(comm.GetDataInQuaternion());
                        Console.WriteLine(comm.GetDataInEuler());
                    }
                    Console.WriteLine("End.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Brak pliku z danymi, program kończy działanie.");
            }
            Console.ReadKey();
        }
    }
}
