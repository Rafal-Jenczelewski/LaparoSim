using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApp2.Utils
{
    class Logger
    {
        private static string path = "D:/laparo_logs.txt";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Log(string s)
        {
            using (var writer = new StreamWriter(path, true))
            {
                //writer.W(s);
            }
        }
    }
}
