using System;
using System.IO;

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
                case "tmbsizes":
                    {
                        VFS.TMBSize();
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
                case "summary":
                    {
                        VFS.Summary();
                        break;
                    }
                case "slb":
                    {
                        int position = 2;
                        int index = int.Parse(parts[position++]);
                        string label = String.Empty;
                        for (; position < parts.Length; position++)
                        {
                            label += parts[position] + " ";
                        }
                        VFS.SLB(index, label);
                        break;
                    }
                case "newpart":
                    {
                        try
                        {
                            int size = int.Parse(parts[2]);
                            int currentDisk = int.Parse(KernelShell.dir);
                            string path = Kernel.dir();
                            for (int i = 0; i < VFS.fs.GetDisks().Count; i++)
                            {
                                if (currentDisk == i)
                                {
                                    VFS.fs.GetDisks()[i].CreatePartition(size);
                                    break;
                                }
                            }
                        } catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Kernel.print("pdu: exception: " + e.ToString());
                            Console.ResetColor();
                        }
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
                        Kernel.print("mbsizes - prints mb size of disks");
                        Kernel.print("tmbsizes - prints total mb size of disks");
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
