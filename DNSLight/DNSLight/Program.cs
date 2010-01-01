using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DNSLight
{
    class Program
    {
        static void Main()
        {
            //tests the DNS System
            Console.Write("Enter the DNS to query: ");
            string dns = Console.ReadLine();

            IPAddress server = IPAddress.Parse(dns);
            Console.WriteLine("Type quit to exit");

            while (true)
            {
                Console.Write(">");
                string domain = Console.ReadLine();

                if (domain.ToLower() == "quit") break;
                if (domain.ToLower() == "change")
                {
                    Console.Write("Enter new DNS server: ");
                    dns = Console.ReadLine();
                    server = IPAddress.Parse(dns);
                }

                Console.WriteLine("Querying DNS records for domain: " + domain);

                Query(server, domain, DnsType.ANAME);
                Query(server, domain, DnsType.MX);
                Query(server, domain, DnsType.NS);
                Query(server, domain, DnsType.SOA);
            }
        }

        private static void Query(IPAddress address, string domain, DnsType type)
        {
            Request request = new Request();

            request.AddQuestion(new Query(domain, type, DnsClass.IN));

            Response response = DnsResolver.LookUp(request, address);

            if (response == null)
            {
                Console.WriteLine("No Answer");
                return;
            }

            Console.WriteLine("---------------------------------------------");

            if (response.AuthoritativeAnswer)
            {
                Console.WriteLine("Authoritative Answer");
            }
            else
            {
                Console.WriteLine("Non-Authoritative Answer");
            }

            foreach (Answer answer in response.Answers)
            {
                Console.WriteLine("{0} ({1}) : {2}", answer.DnsType.ToString(), answer.Domain, answer.Record.ToString());
            }

            foreach (NameServer name in response.NameServers)
            {
                if (name.Record == null)
                    Console.WriteLine("{0} ({1})", name.DnsType.ToString(), name.Domain);
                else
                    Console.WriteLine("{0} ({1}) : {2}", name.DnsType.ToString(), name.Domain, name.Record.ToString());
            }

            foreach (AdditionalRecord record in response.AdditionalRecords)
            {
                Console.WriteLine("{0} ({1}) : {2}", record.DnsType.ToString(), record.Domain, record.Record.ToString());
            }
        }
    }
}
