using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utils
{
    class Logger
    {
        private static string path = "laparo_logs.txt";

        public static void Log(string s)
        {
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLineAsync(s);
            }
        }
    }
}
