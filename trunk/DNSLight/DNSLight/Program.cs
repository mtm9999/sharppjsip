using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DNSLight
{
    class Program
    {
        static string NullIP = "";

        static void Main()
        {
            //tests the DNS System
            Console.Write("Do you want to use default DNS: ");
            string dns = "";
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Y)
                dns = "208.67.222.222";
            else if (key == ConsoleKey.N)
            {
                Console.WriteLine();
                Console.Write("Enter the DNS to query: ");
                dns = Console.ReadLine();
            }
            else if (key == ConsoleKey.Enter)
            {
                return;
            }
            Console.WriteLine();

            IPAddress server;

            try
            {
                server = IPAddress.Parse(dns);
            }
            catch
            {
                return;
            }

            Console.WriteLine("Type quit to exit, change to enter a new DNS server");

            NullIP = QueryNullIP(server);

            while (true)
            {
                Console.Write(">");
                string domain = Console.ReadLine();

                if (domain.ToLower() == "quit") break;
                if (domain.ToLower() == "change")
                {
                    Console.Write("Enter new DNS server(Enter to cancel or 'default'): ");
                    dns = Console.ReadLine();
                    if (dns.ToLower() == "default") dns = "208.67.222.222";
                    if (dns != "")
                    {
                        server = IPAddress.Parse(dns);
                        NullIP = QueryNullIP(server);
                    }
                    continue;
                }
                if (domain.ToLower() == "clear") Console.Clear();

                Console.WriteLine("Querying DNS records for domain: " + domain);
                try
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ANAME Query");
                    if (Query(server, domain, DnsType.ANAME))
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("MX Query");
                        Query(server, domain, DnsType.MX);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("NS Query");
                        Query(server, domain, DnsType.NS);

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("SOA Query");
                        Query(server, domain, DnsType.SOA);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Some error: " + ex.Message);
                }
            }
        }

        private static string QueryNullIP(IPAddress address)
        {
            Request request = new Request();
            request.AddQuestion(new Query("ashfiuewjkfb.com", DnsType.ANAME, DnsClass.IN));
            Response resp = DnsResolver.LookUp(request, address);

            foreach (Answer answer in resp.Answers)
            {
                if (answer.DnsType == DnsType.ANAME)
                    return answer.Record.ToString();
            }

            return "0.0.0.0";
        }

        private static bool Query(IPAddress address, string domain, DnsType type)
        {
            Request request = new Request();

            request.AddQuestion(new Query(domain, type, DnsClass.IN));

            Response response = DnsResolver.LookUp(request, address);

            if (response == null)
            {
                Console.WriteLine("No Answer");
                return false;
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
                if (answer.DnsType == DnsType.ANAME && answer.Record.ToString() == NullIP)
                {
                    Console.WriteLine("Domain does not exist");
                    return false;
                }
                else
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

            return true;
        }
    }
}
