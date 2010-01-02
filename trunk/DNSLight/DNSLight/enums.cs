using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    public enum DnsType
    {
        None = 0,
        ANAME = 1,
        NS = 2,
        CNAME = 5,
        SOA = 6,
        MX = 15,
        RT = 21
    }

    public enum DnsClass
    {
        None = 0,
        IN = 1,
        CS = 2,
        CH = 3,
        HS = 4
    }

    public enum ReturnCode
    {
        Success = 0,
        FormatError = 1,
        ServerFailure = 2,
        NameError = 3,
        NotImplemented = 4,
        Refused = 5,
        Other = 6
    }

    public enum Opcode
    {
        StandardQuery = 0,
        InverseQuery = 1,
        StatusRequest = 2,
        Reserved3,
        Reserved4,
        Reserved5,
        Reserved6,
        Reserved7,
        Reserved8,
        Reserved9,
        Reserved10,
        Reserved11,
        Reserved12,
        Reserved13,
        Reserved14,
        Reserved15,
    }
}
