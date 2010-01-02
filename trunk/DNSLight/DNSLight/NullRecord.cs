using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    class NullRecord : Record
    {
        public override string ToString()
        {
            return "Null Record";
        }
    }
}
