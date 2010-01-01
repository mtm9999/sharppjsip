using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobLog
{
    public interface IScreenLog : ILog
    {
        void displayLine();
        void displayLine(int line);
        void outputEntireLog();
    }
}
