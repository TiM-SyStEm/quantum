using System;

namespace Quantum
{
    class KernelPDU
    {
        public static void interact(string[] parts) 
        {
            string opinion = parts[1];

            switch (opinion)
            {
                case "names":
                    {
                        VFS.Names();
                        break;
                    }
                case "sizes":
                    {
                        VFS.Sizes();
                        break;
                    }
                case "labels":
                    {
                        VFS.Labels();
                        break;
                    }
                case "types":
                    {
                        VFS.Types();
                        break;
                    }
                case "formats":
                    {
                        VFS.Formats();
                        break;
                    }
                case "tsizes":
                    {
                        VFS.TotalSize();
                        break;
                    }
                case "mbsizes":
                    {
                        VFS.MBSize();
                        break;
                    }
                case "format":
                    {
                        VFS.Format(parts[2]);
                        break;
                    }
                case "ir":
                    {
                        VFS.IsReady();
                        break;
                    }
                case "help":
                    {
                        Kernel.print("           PDU - Portable Disk Util. 1.0.0");
                        Kernel.print("      This software is part of Quantum-Project.");
                        Kernel.print("=========================================================");
                        Kernel.print("names - prints names of disks");
                        Kernel.print("sizes - prints sizes of disks");
                        Kernel.print("labels - prints labels of disks");
                        Kernel.print("types - prints types of disks (CDRom, Removable...)");
                        Kernel.print("formats - prints formats of disks(FAT32...)");
                        Kernel.print("tsizes - prints total sizes of disks");
                        Kernel.print("ir - is disks ready?");
                        Kernel.print("format ID - formats disk with ID");
                        Kernel.print("=========================================================");
                        break;
                    }
                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Kernel.print("pdu: unknown arg: " + opinion);
                        Console.ResetColor();
                        break;
                    }
            }
        }
    }
}
