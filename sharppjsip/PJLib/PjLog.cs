using System;
using System.IO;
using System.Text;
using System.Threading;

namespace PJLib
{
    public class PjLog
    {
        private PjLogLevel pLogLevel;
        private string filename;

        public PjLogLevel LogLevel
        {
            get { return pLogLevel; }
            set { pLogLevel = value; }
        }

        private PjLogDecoration pLogDecoration;

        public PjLogDecoration LogDecoration
        {
            get { return pLogDecoration; }
            set { pLogDecoration = value; }
        }

        public PjLog(string file) : this()
        {
            this.filename = file;
        }

        public PjLog()
        {
            pLogDecoration = PjLogDecoration.HasTime | PjLogDecoration.HasMicroSec | PjLogDecoration.HasSender | PjLogDecoration.HasNewLine;
            filename = "pj.log";
        }

        const PjLogLevel defaultLogLevel = PjLogLevel.Info;

        public void log(object caller, PjLogLevel level, string message)
        {
            DateTime now = DateTime.Now;
            StringBuilder finalMessage = new StringBuilder();
            if ((pLogDecoration & PjLogDecoration.HasLevelText) != 0)
            {
                finalMessage.Append(level.ToString() + ": ");
            }

            if ((pLogDecoration & PjLogDecoration.HasDayName) != 0) 
            {
                finalMessage.Append(now.DayOfWeek.ToString() + ", ");
            }

            if ((pLogDecoration & PjLogDecoration.HasYear) != 0)
            {
                finalMessage.Append(now.Year + " ");
            }

            if ((pLogDecoration & PjLogDecoration.HasMonth) != 0)
            {
                finalMessage.Append(now.Month.ToString() + " ");
            }

            if ((pLogDecoration & PjLogDecoration.HasDayOfMonth) != 0)
            {
                finalMessage.Append(now.Day.ToString() + " ");
            }

            if ((pLogDecoration & PjLogDecoration.HasTime) != 0)
            {
                finalMessage.Append(now.ToLongTimeString());
            }

            if ((pLogDecoration & PjLogDecoration.HasMicroSec) != 0)
            {
                finalMessage.Append("." + now.Millisecond + " ");
            }

            if ((pLogDecoration & PjLogDecoration.HasSender) != 0)
            {
                if (caller != null)
                    finalMessage.Append("[" + caller.ToString() + "] ");
            }

            if ((pLogDecoration & PjLogDecoration.HasThreadID) != 0)
            {
                finalMessage.Append("<" + Thread.CurrentThread.ManagedThreadId.ToString() + "> ");
            }

            finalMessage.Append(" ~ " + message);

            if ((pLogDecoration & PjLogDecoration.HasCR) != 0)
            {
                finalMessage.Append("\r");
            }

            if ((pLogDecoration & PjLogDecoration.HasNewLine) != 0)
            {
                finalMessage.Append("\n");
            }

            write(finalMessage.ToString(), level);
        }

        public void log(object caller, string message)
        {
            log(caller, defaultLogLevel, message);
        }

        private static bool pIsSuspended = false;

        public static bool IsSuspended
        {
            get { return PjLog.pIsSuspended; }
        }

        public static void suspendLog()
        {
            pIsSuspended = true;
        }

        public static void resumeLog()
        {
            pIsSuspended = false;
        }

        private void write(string logged, PjLogLevel level)
        {
            if (level <= LogLevel)
            {
                Console.Write(logged);
            }

            if (!IsSuspended)
            {
                //output to file
                StreamWriter writer = new StreamWriter(filename, true);
                writer.Write(logged);
                writer.Close();
            }
        }
    }
}
