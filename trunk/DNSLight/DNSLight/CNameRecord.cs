using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    class CNameRecord : Record
    {
        private readonly string _domain;

        public string Domain
        {
            get { return _domain; }
        } 

        internal CNameRecord(SmartPointer pointer)
        {
            _domain = pointer.ReadDomain();
        }

        public override string ToString()
        {
            return _domain;
        }
    }
}
