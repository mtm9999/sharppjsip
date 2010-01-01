using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace DNSLight
{
    public sealed class DnsResolver
    {
        const int _dnsPort = 53;
        const int _udpRetryAttempts = 2;
        static int _uniqueID;

        private DnsResolver()
        {
        }

        public static MxRecord[] MxLookup(string domain, IPAddress dnsServer)
        {
            if (domain == null) throw new ArgumentNullException("domain");
            if (dnsServer == null) throw new ArgumentNullException("dnsServer");

            Request request = new Request();

            request.AddQuestion(new Query(domain, DnsType.MX, DnsClass.IN));

            Response response = LookUp(request, dnsServer);

            if (response == null) return null;

            List<Record> resourceRecords = new List<Record>();

            foreach (Answer answer in response.Answers)
            {
                if (answer.Record is MxRecord)
                {
                    resourceRecords.Add(answer.Record);
                }
            }

            resourceRecords.Sort();

            MxRecord[] mx = resourceRecords.ToArray() as MxRecord[];

            return mx;
        }

        public static Response LookUp(Request request, IPAddress dnsServer)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (dnsServer == null) throw new ArgumentNullException("dnsServer");

            IPEndPoint server = new IPEndPoint(dnsServer, _dnsPort);

            byte[] requestMessage = request.GetMessage();

            byte[] responseMessage = UdpTransfer(server, requestMessage);

            return new Response(responseMessage);
        }

        private static byte[] UdpTransfer(IPEndPoint server, byte[] requestMessage)
        {
            int attempts = 0;

            while (attempts <= _udpRetryAttempts)
            {
                unchecked
                {
                    requestMessage[0] = (byte)(_uniqueID >> 8);
                    requestMessage[1] = (byte)(_uniqueID);
                }

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);

                socket.SendTo(requestMessage, requestMessage.Length, SocketFlags.None, server);

                byte[] responseMessage = new byte[512];

                try
                {
                    socket.Receive(responseMessage);

                    if (responseMessage[0] == requestMessage[0] && responseMessage[1] == requestMessage[1])
                        return responseMessage;
                }
                catch (SocketException)
                {
                    attempts++;
                }
                finally
                {
                    _uniqueID++;

                    socket.Close();
                }
            }

            throw new Exception("No response from server");
        }
    }
}
