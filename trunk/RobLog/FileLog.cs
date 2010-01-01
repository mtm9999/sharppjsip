using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobLog
{
    public interface IFileLog : ILog
    {
        void writeLogToFile(string file);
    }
}
