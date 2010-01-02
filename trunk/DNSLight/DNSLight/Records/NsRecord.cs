using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    public class NsRecord : Record
    {
        private readonly string _domainName;

        public string DomainName
        {
            get { return _domainName; }
        }

        internal NsRecord(SmartPointer pointer)
        {
            _domainName = pointer.ReadDomain();
        }

        public override string ToString()
        {
            return _domainName;
        }
    }
}
