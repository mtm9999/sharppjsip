using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DNSLight
{
    public class WKSRecord : Record
    {
        private readonly IPAddress _address;
        private readonly int _protocol;
        private readonly byte[] _bitmap;

        internal WKSRecord(SmartPointer pointer)
        {
            _address = pointer.ReadIPAddress();
            _protocol = pointer.ReadByte();

            throw new NotImplementedException("Haven't finished this yet");
        }
    }
}
