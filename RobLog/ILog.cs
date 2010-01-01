using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobLog
{
    public interface ILog
    {
        ELogLevel logLevel
        {
            get;
            set;
        }

        string[] logArray
        {
            get;
        }

        /// <summary>
        /// Writes message to the log at the default log level (Info)
        /// </summary>
        /// <param name="caller">the caller</param>
        /// <param name="message">the message</param>
        void log(object caller, string message);
        void log(object caller, string message, ELogLevel level);
    }
}
