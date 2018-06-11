using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaparoCommunicator;

namespace LaparoMockTester
{
    class Program
    {
        static private String pathToPortFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data");

        static void Main(string[] args)
        {
            while (!File.Exists(pathToPortFile + "/quaternion.tsv") || !File.Exists(pathToPortFile + "/euler.tsv"))
            {
                Console.WriteLine("W poniższym folderze muszą istnieć plik 'euler.tsv' oraz 'quaternion.tsv' aby program działał prawidłowo:");
                Console.WriteLine(pathToPortFile);
                Console.WriteLine("Stwórz te pliki, a następnie naciśnij dowolny klawisz.");
                Console.ReadKey(true);
            }

            Console.WriteLine("Naciśnij 'q', aby zakończyć program, dowolny inny klawisz, aby pobrać kolejną wartość.");

            try
            {
                using (var comm = LaparoCommunicator.CommunicatorFactory.GetMock(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data")))
                {
                    while(true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Quat: " + comm.GetDataInQuaternion());
                        Console.WriteLine("Euler: " + comm.GetDataInEuler());
                        if (Console.ReadKey(true).Key == ConsoleKey.Q)
                            break;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Brak pliku z danymi, program kończy działanie. Wyjątek:\n" + e.Message);
            }
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }
}
