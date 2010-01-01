using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DNSLight
{
    class ANameRecord : Record
    {
        private IPAddress _ipAddress;

        public IPAddress ipAddress
        {
            get
            {
                return _ipAddress;
            }
        }

        internal ANameRecord(SmartPointer pointer)
        {
            byte b1 = pointer.ReadByte();
            byte b2 = pointer.ReadByte();
            byte b3 = pointer.ReadByte();
            byte b4 = pointer.ReadByte();

            _ipAddress = IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", b1, b2, b3, b4));
        }

        public override string ToString()
        {
            return _ipAddress.ToString();
        }
    }
}
