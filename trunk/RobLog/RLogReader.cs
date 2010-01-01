using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RobLog
{
    public class RLogReader
    {
        public static string[] ReadLog(string file)
        {
            if (File.Exists(file))
            {
                StreamReader reader = new StreamReader(file);
                List<string> log = new List<string>();
                while (!reader.EndOfStream)
                    log.Add(reader.ReadLine());
                reader.Close();
                return log.ToArray();
            }
            return new string[0];
        }

        public static void displayLog(string file)
        {
            string[] log = ReadLog(file);
            Console.WriteLine("Displaying log: " + file);
            foreach(string line in log)
                Console.WriteLine(line);
        }
    }
}
