﻿using Cosmos.HAL;
using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.TCP.FTP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System;
using System.Text;

namespace Quantum
{

    class HTTPListener
    {

        public static void Execute(string arg, string name)
        {
            try
            {
                var dnsClient = new DnsClient();
                var tcpClient = new TcpClient(80);

                dnsClient.Connect(DNSConfig.Server(0));
                dnsClient.SendAsk(arg);
                Address address = dnsClient.Receive();
                dnsClient.Close();

                tcpClient.Connect(address, 80);

                string httpget = "GET / HTTP/1.1\r\n" +
                                 "User-Agent: Wget (CosmosOS)\r\n" +
                                 "Accept: */*\r\n" +
                                 "Accept-Encoding: identity\r\n" +
                                 "Host: " + arg + "\r\n" +
                                 "Connection: Keep-Alive\r\n\r\n";

                tcpClient.Send(Encoding.ASCII.GetBytes(httpget));

                var ep = new EndPoint(Address.Zero, 0);
                byte[] data = tcpClient.Receive(ref ep);
                VFS.Touch(Kernel.dir() + name);
                VFS.ByteArrayToFile(Kernel.dir() + name, data);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Kernel.print(ex.ToString());
                Console.ResetColor();
            }

            Kernel.print("curl: success");
        }
    }

    class Ping
    {
        public static void Execute(string domain)
        {
            int PacketSent = 0;
            int PacketReceived = 0;
            int PacketLost = 0;
            int PercentLoss;

            Address source;
            Address destination = Address.Parse(domain);

            if (destination != null)
            {
                source = IPConfig.FindNetwork(destination);
            }
            else //Make a DNS request if it's not an IP
            {
                var xClient = new DnsClient();
                xClient.Connect(DNSConfig.Server(0));
                xClient.SendAsk(domain);
                destination = xClient.Receive();
                xClient.Close();

                if (destination == null)
                {
                    Kernel.err("curl: failed to get DNS respone");
                    return;
                }

                source = IPConfig.FindNetwork(destination);
            }

            try
            {
                Console.WriteLine("Sending ping to " + destination.ToString());

                var xClient = new ICMPClient();
                xClient.Connect(destination);

                for (int i = 0; i < 4; i++)
                {
                    xClient.SendEcho();

                    PacketSent++;

                    var endpoint = new EndPoint(Address.Zero, 0);

                    int second = xClient.Receive(ref endpoint, 4000);

                    if (second == -1)
                    {
                        Console.WriteLine("Destination host unreachable.");
                        PacketLost++;
                    }
                    else
                    {
                        if (second < 1)
                        {
                            Console.WriteLine("Reply received from " + endpoint.Address.ToString() + " time < 1s");
                        }
                        else if (second >= 1)
                        {
                            Console.WriteLine("Reply received from " + endpoint.Address.ToString() + " time " + second + "s");
                        }

                        PacketReceived++;
                    }
                }

                xClient.Close();
            }
            catch
            {
                Kernel.err("curl: ping process error");
                return;
            }

            PercentLoss = 25 * PacketLost;

            Kernel.print();
            Kernel.print("Ping statistics for " + destination.ToString() + ":");
            Kernel.print("    Packets: Sent = " + PacketSent + ", Received = " + PacketReceived + ", Lost = " + PacketLost + " (" + PercentLoss + "% loss)");
        }
    }
    
    class DNS
    {
        public static void Execute(string arg)
        {
            var xClient = new DnsClient();
            string domainname;

            xClient.Connect(DNSConfig.Server(0));
            Console.WriteLine("DNS used : " + DNSConfig.Server(0).ToString());
            xClient.SendAsk(arg);
            domainname = arg;

            Address address = xClient.Receive();

            xClient.Close();

            if (address == null)
            {
                Kernel.err("curl: bad domain");
                return;
            }
            else
            {
                Console.WriteLine(domainname + " is " + address.ToString());
            }
        }
    }

    class ICP
    {
        public static void Empty()
        {
            if (NetworkStack.ConfigEmpty())
            {
                Console.WriteLine("No network configuration detected! Use ipconfig /help");
            }
            foreach (NetworkDevice device in NetworkConfig.Keys)
            {
                switch (device.CardType)
                {
                    case CardType.Ethernet:
                        Console.Write("Ethernet Card : " + device.NameID + " - " + device.Name);
                        break;
                    case CardType.Wireless:
                        Console.Write("Wireless Card : " + device.NameID + " - " + device.Name);
                        break;
                }
                if (NetworkConfig.CurrentConfig.Key == device)
                {
                    Console.WriteLine(" (current)");
                }
                else
                {
                    Console.WriteLine();
                }

                Console.WriteLine("MAC Address          : " + device.MACAddress.ToString());
                Console.WriteLine("IP Address           : " + NetworkConfig.Get(device).IPAddress.ToString());
                Console.WriteLine("Subnet mask          : " + NetworkConfig.Get(device).SubnetMask.ToString());
                Console.WriteLine("Default Gateway      : " + NetworkConfig.Get(device).DefaultGateway.ToString());
                Console.WriteLine("DNS Nameservers      : ");
                foreach (Address dnsnameserver in DNSConfig.DNSNameservers)
                {
                    Console.WriteLine("                       " + dnsnameserver.ToString());
                }
            }
        }
    }

    class FTP
    {
        public static void Execute()
        {

            Kernel.print("Soon");     
        }
    }

    class KernelCurl
    {
        public static void Init()
        {
            Console.WriteLine("NetworkStack: maybe success.");
            using (var xClient = new DHCPClient())
            {
                xClient.SendDiscoverPacket();
            }
            Console.WriteLine("DHCPCLient: sendDiscoverPacket");
            Console.WriteLine("curl: Success.");
        }
        public static void start(string[] parts)
        {
            string opinion = parts[1];
            switch (opinion)
            {
                case "ip":
                    {
                        Kernel.print(NetworkConfig.CurrentConfig.Value.IPAddress.ToString());
                        break;
                    }
                case "url":
                    {
                        HTTPListener.Execute(parts[2], parts[3]);
                        break;
                    }
                case "ping":
                    {
                        Ping.Execute(parts[2]);
                        break;
                    }
                case "dns":
                    {
                        DNS.Execute(parts[2]);
                        break;
                    }
                case "ftp":
                    { 
                        Kernel.print("Port: 21");
                        FTP.Execute();
                        break;
                    }
                case "ipconfig":
                    {
                        if (parts.Length == 2)
                        {
                            ICP.Empty();
                        }
                        break;
                    }
                    
                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Kernel.print("curl: unknown command: " + opinion);
                        Console.ResetColor();
                        break;
                    }
            }
        }
    }
}
