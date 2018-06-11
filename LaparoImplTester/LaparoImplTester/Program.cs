using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using LaparoCommunicator;   

namespace LaparoImplTester
{
    class Program
    {
        static private String pathToPortFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "portName.txt");

        static void Main()
        {
            while(!File.Exists(pathToPortFile))
            {
                Console.WriteLine("Poniższy plik musi istnieć, aby program zadziałał prawidłowo:");
                Console.WriteLine(pathToPortFile);
                Console.WriteLine("Stwórz ten plik, a następnie naciśnij dowolny klawisz.");
                Console.ReadKey(true);
            }

            Console.WriteLine("Naciśnij 'q', aby zakończyć program, dowolny inny klawisz, aby pobrać kolejną wartość.");

            try
            {
                using (var comm = CommunicatorFactory.GetCommunicator())
                {
                    while (true)
                    {
                        Console.WriteLine(comm.GetDataInQuaternion());
                        if (Console.ReadKey(true).Key == ConsoleKey.Q)
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Podano zły port, lub port nie mógł zostać otwarty. Wyjątek:\n" + e.Message);
            }
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }
}
