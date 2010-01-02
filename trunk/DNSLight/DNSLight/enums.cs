using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    public enum DnsType
    {
        None = 0, 
        ANAME = 1, ///host address
        NS = 2, ///authoritative name server
        MD = 3, /// Mail destination (obsolete)
        MF = 4, //mail forward (obsolete)
        CNAME = 5, //alias
        SOA = 6, //start of authority
        MB = 7, //mailbox domain name (ex)
        MG = 8, //a mail group member (ex)
        MR = 9, //a mail rename domain name (ex)
        NULL = 10, //a null RR (ex)
        WKS = 11, //a well known service
        PTR = 12, //a domain name pointer
        HINFO = 13, //host info
        MINFO = 14, //mail list desc
        MX = 15, //mail exchange
        TXT = 16, //txt strings
        RT = 21 
    }

    public enum DnsClass
    {
        None = 0,
        IN = 1, //the internet
        CS = 2, //csnet (obsolete)
        CH = 3, //choas class
        HS = 4 //hesiod
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
