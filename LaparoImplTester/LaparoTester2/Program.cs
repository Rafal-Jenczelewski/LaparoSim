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
            try
            {
                using (var comm = CommunicatorFactory.GetCommunicator())
                {
                    while (true)
                    {
                        Console.WriteLine(comm.GetDataInQuaternion());
                        if (Console.ReadKey().Key == ConsoleKey.Q)
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Podano zły port, lub port nie mógł zostać otwarty. Wyjątek:\n" + e.Message);
            }
            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}
