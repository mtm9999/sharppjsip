using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    class SoaRecord : Record
    {
        private readonly string _primaryNameServer;

        public string PrimaryNameServer
        {
            get { return _primaryNameServer; }
        }

        private readonly string _responsibleMailAddress;

        public string ResponsibleMailAddress
        {
            get { return _responsibleMailAddress; }
        }

        private readonly int _serial;

        public int Serial
        {
            get { return _serial; }
        }

        private readonly int _refresh;

        public int Refresh
        {
            get { return _refresh; }
        }

        private readonly int _retry;

        public int Retry
        {
            get { return _retry; }
        }

        private readonly int _expire;

        public int Expire
        {
            get { return _expire; }
        }

        private readonly int _defaultTTL;

        public int DefaultTTL
        {
            get { return _defaultTTL; }
        }

        internal SoaRecord(SmartPointer pointer)
        {
            _primaryNameServer = pointer.ReadDomain();
            _responsibleMailAddress = pointer.ReadDomain();
            _serial = pointer.ReadInt();
            _refresh = pointer.ReadInt();
            _retry = pointer.ReadInt();
            _expire = pointer.ReadInt();
            _defaultTTL = pointer.ReadInt();
        }

        public override string ToString()
        {
            return string.Format("primary name server = {0}\nresponsible mail addr = {1}\nserial  = {2}\nrefresh = {3}\nretry   = {4}\nexpire  = {5}\ndefault TTL = {6}",
                _primaryNameServer,
                _responsibleMailAddress,
                _serial.ToString(),
                _refresh.ToString(),
                _retry.ToString(),
                _expire.ToString(),
                _defaultTTL.ToString());
        }
    }
}
