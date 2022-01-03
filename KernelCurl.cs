using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP.FTP;
using Cosmos.System.Network.IPv4.UDP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
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
            Console.WriteLine("CURL: Success.");
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
