using Cosmos.HAL;
using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
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
                Kernel.print("Sending ping to " + destination.ToString());

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
                        Kernel.print("Destination host unreachable.");
                        PacketLost++;
                    }
                    else
                    {
                        if (second < 1)
                        {
                            Kernel.print("Reply received from " + endpoint.Address.ToString() + " time < 1s");
                        }
                        else if (second >= 1)
                        {
                            Kernel.print("Reply received from " + endpoint.Address.ToString() + " time " + second + "s");
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
            Kernel.print("DNS used : " + DNSConfig.Server(0).ToString());
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
                Kernel.print(domainname + " is " + address.ToString());
            }
        }
    }

    class ICP
    {
        public static void Empty()
        {
            if (NetworkStack.ConfigEmpty())
            {
                Kernel.print("No network configuration detected! Use ipconfig /help");
            }
            foreach (NetworkDevice device in NetworkConfig.Keys)
            {
                switch (device.CardType)
                {
                    case CardType.Ethernet:
                        Kernel.print("Ethernet Card : " + device.NameID + " - " + device.Name);
                        break;
                    case CardType.Wireless:
                        Kernel.print("Wireless Card : " + device.NameID + " - " + device.Name);
                        break;
                }
                if (NetworkConfig.CurrentConfig.Key == device)
                {
                    Kernel.print(" (current)");
                }
                else
                {
                    Kernel.print();
                }

                Kernel.print("MAC Address          : " + device.MACAddress.ToString());
                Kernel.print("IP Address           : " + NetworkConfig.Get(device).IPAddress.ToString());
                Kernel.print("Subnet mask          : " + NetworkConfig.Get(device).SubnetMask.ToString());
                Kernel.print("Default Gateway      : " + NetworkConfig.Get(device).DefaultGateway.ToString());
                Kernel.print("DNS Nameservers      : ");
                foreach (Address dnsnameserver in DNSConfig.DNSNameservers)
                {
                    Kernel.print("                       " + dnsnameserver.ToString());
                }
            }
        }

        public static void Execute(string[] arguments)
        {
            if (arguments[2] == "release")
            {
                var xClient = new DHCPClient();
                xClient.SendReleasePacket();
                xClient.Close();
            }
            else if (arguments[2] == "ask")
            {
                var xClient = new DHCPClient();
                if (xClient.SendDiscoverPacket() != -1)
                {
                    xClient.Close();
                    Kernel.print("Configuration applied! Your local IPv4 Address is " + NetworkConfig.CurrentConfig.Value.IPAddress.ToString() + ".");
                }
                else
                {
                    xClient.Close();
                    Kernel.err("curl: DHCP discover failed. can't apply dynamic IPv4 addres");
                    return;
                }
            }
            else if (arguments[2] == "listnic")
            {
                foreach (var device in NetworkDevice.Devices)
                {
                    switch (device.CardType)
                    {
                        case CardType.Ethernet:
                            Kernel.print("Ethernet Card - " + device.NameID + " - " + device.Name + " (" + device.MACAddress + ")");
                            break;
                        case CardType.Wireless:
                            Kernel.print("Wireless Card - " + device.NameID + " - " + device.Name + " (" + device.MACAddress + ")");
                            break;
                    }
                }
            }
            else if (arguments[2] == "set")
            {
                if ((arguments.Length == 4) || (arguments.Length == 5)) // ipconfig /set eth0 192.168.1.2/24 {gw}
                {
                    string[] adrnetwork = arguments[2].Split('/');
                    Address ip = Address.Parse(adrnetwork[0]);
                    NetworkDevice nic = NetworkDevice.GetDeviceByName(arguments[1]);
                    Address gw = null;
                    if (arguments.Length == 5)
                    {
                        gw = Address.Parse(arguments[3]);
                    }

                    int cidr;
                    try
                    {
                        cidr = int.Parse(adrnetwork[1]);
                    }
                    catch (Exception ex)
                    {
                        Kernel.err("curl: " + ex.ToString());
                        return;
                    }
                    Address subnet = Address.CIDRToAddress(cidr);

                    if (nic == null)
                    {
                        Kernel.err("curl: coulnd't find network device");
                        return;
                    }

                    if (ip != null && subnet != null && gw != null)
                    {
                        IPConfig.Enable(nic, ip, subnet, gw);
                        Kernel.magic("Config OK!");
                    }
                    else if (ip != null && subnet != null)
                    {
                        IPConfig.Enable(nic, ip, subnet, ip);
                        Kernel.magic("Config OK!");
                    }
                    else
                    {
                        Kernel.err("curl: can't parse IP address");
                        return;
                    }
                }
                else
                {
                    Kernel.err("curl: usage: ipconfig set {device} {ipv4|cidr} {gateaway|null}");
                    return;
                }
            }
            else if (arguments[2] == "nameserver")
            {
                if (arguments[3] == "-add")
                {
                    DNSConfig.Add(Address.Parse(arguments[2]));
                    Kernel.print(arguments[2] + " has been added to nameservers.");
                }
                else if (arguments[3] == "-rem")
                {
                    DNSConfig.Remove(Address.Parse(arguments[2]));
                    Kernel.print(arguments[2] + " has been removed from nameservers list.");
                }
                else
                {
                    Kernel.err("curl: usage: ipconfig nameserver {add|remove} {ip}");
                    return;
                }
            }
            else
            {
                Kernel.err("curl: bad usage");
                return;
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
            Kernel.print("NetworkStack: maybe success.");
            using (var xClient = new DHCPClient())
            {
                Kernel.print("Waiting...");
                Kernel.print("Success!");
            }
            Kernel.print("DHCPCLient: sendDiscoverPacket");
            Kernel.print("curl: Success.");
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
                        Kernel.err("curl: unknown command: " + opinion);
                        break;
                    }
            }
        }
    }
}
