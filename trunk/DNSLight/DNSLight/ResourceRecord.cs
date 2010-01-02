using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    [Serializable]
    public class ResourceRecord
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

        private readonly int _ttl;

        public int Ttl
        {
            get { return _ttl; }
        }

        private Record _record;

        public Record Record
        {
            get 
            {
                if (_record == null)
                    _record = new NullRecord();
                return _record; 
            }
        }

        internal ResourceRecord(SmartPointer pointer)
        {
            _domain = pointer.ReadDomain();
            _dnsType = (DnsType)pointer.ReadShort();
            _dnsClass = (DnsClass)pointer.ReadShort();
            _ttl = pointer.ReadInt();

            int recordLength = pointer.ReadShort();

            switch (_dnsType)
            {
                case DnsType.ANAME: _record = new ANameRecord(pointer); break;
                case DnsType.MX: _record = new MxRecord(pointer); break;
                case DnsType.NS: _record = new NsRecord(pointer); break;
                case DnsType.SOA: _record = new NsRecord(pointer); break;
                case DnsType.CNAME: _record = new CNameRecord(pointer); break;
                case DnsType.PTR: _record = new CNameRecord(pointer); break;
                default:
                    pointer += recordLength;
                    break;
            }
        }
    }

    [Serializable]
    public class Answer : ResourceRecord
    {
        internal Answer(SmartPointer pointer) : base(pointer) { }
    }

    [Serializable]
    public class NameServer : ResourceRecord
    {
        internal NameServer(SmartPointer pointer) : base(pointer) { }
    }

    [Serializable]
    public class AdditionalRecord : ResourceRecord
    {
        internal AdditionalRecord(SmartPointer pointer) : base(pointer) { }
    }
}
