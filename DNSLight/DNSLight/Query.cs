using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RobLog;

namespace DNSLight
{
    [Serializable]
    public class Query
    {
        private readonly string _domain;

        public string Domain
        {
            get { return _domain; }
        }

        private readonly DnsType _dnsType;

        public DnsType DnsType
        {
            get { return _dnsType; }
        }

        private readonly DnsClass _dnsClass;

        public DnsClass DnsClass
        {
            get { return _dnsClass; }
        }

        public Query(string domain, DnsType type, DnsClass dnsClass)
        {
            RLog log = new RLog();
            log.log("Query.cs", "Setting up query: " + domain, ELogLevel.Info3);
            if (domain == null) throw new ArgumentNullException("domain");

            if (domain.Length == 0 || domain.Length > 255 || !Regex.IsMatch(domain, @"^[a-z|A-Z|0-9|-|_]{1,63}(\.[a-z|A-Z|0-9|-|_]{1,63})+$"))
            {
                log.log("Query.cs", "Domain is of incorrect type", ELogLevel.Fatal0);
                throw new ArgumentException("Domain is of incorrect type");
            }

            if (!Enum.IsDefined(typeof(DnsType), type) || type == DnsType.None)
            {
                log.log("Query.cs", "DNS TYPE is out of range", ELogLevel.Fatal0);
                throw new ArgumentException();
            }

            if (!Enum.IsDefined(typeof(DnsClass), dnsClass) || dnsClass == DnsClass.None)
            {
                log.log("Query.cs", "DNS CLASS is out of range", ELogLevel.Fatal0);
                throw new ArgumentException();
            }

            _domain = domain;
            _dnsType = type;
            _dnsClass = dnsClass;
        }

        internal Query(SmartPointer pointer)
        {
            _domain = pointer.ReadDomain();
            _dnsType = (DnsType)pointer.ReadShort();
            _dnsClass = (DnsClass)pointer.ReadShort();
        }
    }
}
