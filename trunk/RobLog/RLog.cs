using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RobLog
{
    [Serializable]
    public class RLog : IFileLog, IScreenLog
    {
        #region Private Members
        ELogLevel level = ELogLevel.Info3;
        List<string> Llog = new List<string>();
        List<ELogLevel> loggedLevel = new List<ELogLevel>();
        //Dictionary<string, ELogLevel> Llog = new Dictionary<string, ELogLevel>();
        string filename = "log.txt";
        #endregion

        public ELogLevel logLevel
        {
            get { return level; }
            set { level = value; }
        }

        public RLog(string file)
        {
            filename = file;

            string[] log = RLogReader.ReadLog(file);

            foreach (string line in log)
            {
                Llog.Add(line);
                loggedLevel.Add(ELogLevel.Detrc6);
            }
        }

        public RLog()
        {
            string[] log = RLogReader.ReadLog(filename);

            foreach (string line in log)
            {
                Llog.Add(line);
                loggedLevel.Add(ELogLevel.Detrc6);
            }
        }

        public string[] logArray
        {
            get
            {
                return Llog.ToArray();
            }
        }

        public void log(object caller, string message, ELogLevel level)
        {
            DateTime now = DateTime.Now;
            string line = level.ToString() + ": (" + now.ToLongTimeString() + " " + now.ToLongDateString() + ") [" + caller.ToString() + "] ~" + message;
            Llog.Add(line);
            loggedLevel.Add(level);
            writeLogToFile();
            //displayLine();
        }

        public void log(object caller, string message)
        {
            log(caller, message, ELogLevel.Info3);
        }

        public void writeLogToFile(string file)
        {
            StreamWriter writer = new StreamWriter(file);
            foreach (string line in Llog)
            {
                writer.WriteLine(line);
            }
            writer.Close();
        }

        public void writeLogToFile()
        {
            (this as IFileLog).writeLogToFile(filename);
        }

        public void displayLine()
        {
            (this as IScreenLog).displayLine(Llog.Count - 1);
        }

        void IScreenLog.outputEntireLog()
        {
            foreach (string line in Llog)
            {
                Console.WriteLine(line);
            }
        }

        void IScreenLog.displayLine(int line)
        {
            if (line >= 0 && line < Llog.Count)
            {
                if (loggedLevel[line] <= logLevel)
                    Console.WriteLine(Llog[line]);
            }
        }
    }
}
