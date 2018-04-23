using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utils
{
    class Logger
    {
        private static string path = "C:/laparo_logs.txt";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Log(string s)
        {
            using (var writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(s);
            }
        }
    }
}
