using System;
using FS = Cosmos.System.FileSystem;
using System.IO;

namespace Quantum
{
    class VFS
    {
        static FS.CosmosVFS fs = new FS.CosmosVFS();

        public static void TryInit()
        {
            Console.WriteLine("Do you want to enable filesystem?");
            Console.WriteLine("All non-FAT32 devices will be formatted!");
            Console.WriteLine("[y/n]");
            var key = Console.ReadKey(false);
            if (key.KeyChar == 'y')
            {
                VFS.Init();
            } else
            {
                Console.WriteLine("Skipped.");
            }
        }

        public static void Init()
        {
            FS.VFS.VFSManager.RegisterVFS(fs);
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("Type: {0}", d.DriveFormat);
                if (d.DriveFormat != "FAT32")
                {
                    Console.WriteLine("Drive {0} is not FAT32. Want to format it?", d.Name);
                    if (Console.ReadKey(true).KeyChar == 'y')
                    {
                        Console.WriteLine("Formatting '" + d.Name + "'");
                        fs.Format(d.Name, "FAT32", false);
                        Console.WriteLine("Success!");
                    } else
                    {
                        Console.WriteLine("Skipped.");
                    }
                }
                Console.WriteLine("Filesystem prepared success!");
            }
        }

        public static void Format(string v)
        {
            fs.Format(v, "FAT32", false);
        }

        public static void Names()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name.ToString());
            }
        }

        public static void Sizes()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + d.AvailableFreeSpace.ToString());
            }
        }

        public static void Labels()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + d.VolumeLabel.ToString());
            }
        }

        public static void Formats()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + d.DriveFormat.ToString());
            }
        }

        public static void Types()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + DriveT(d.DriveType));
            }
        }

        private static string DriveT(DriveType d)
        {
            switch (d)
            {
                case DriveType.Unknown:
                    {
                        return "Unknown";
                    }
                case DriveType.NoRootDirectory:
                    {
                        return "NoRootDirectory";
                    }
                case DriveType.Removable:
                    {
                        return "USB/Removable";
                    }
                case DriveType.Fixed:
                    {
                        return "Fixed";
                    }
                case DriveType.CDRom:
                    {
                        return "CDRom";
                    }
                case DriveType.Network:
                    {
                        return "Network";
                    }
                case DriveType.Ram:
                    {
                        return "RAM";
                    }
                default:
                    {
                        return "???";
                    }
            }
        }

        public static void TotalSize()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + d.TotalSize.ToString());
            }
        }

        public static void IsReady()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + d.IsReady.ToString());
            }
        }

        public static void CDR()
        {
            try
            {
                var directory_list = Directory.GetFiles(Kernel.dir());
                if (directory_list.Length == 0) return;
                foreach (var file in directory_list)
                {
                    Kernel.print(file);
                }
            } catch (Exception e)
            {
                Kernel.deGrep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unexpected 'cdr' error: " + e.ToString());
                Console.ResetColor();
            }
        }

        public static void Touch(string name)
        {
            try
            {
                File.Create(Kernel.dir() + name);
            } catch (Exception e)
            {
                Kernel.deGrep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to touch: " + e.ToString());
                Console.ResetColor();
            }
        }

        public static void RM(string v)
        {
            try
            {
                File.Delete(Kernel.dir() + v);
            }
            catch (Exception e)
            {
                Kernel.deGrep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to remove: " + e.ToString());
                Console.ResetColor();
            }
        }

        public static void Cat(string dir)
        {
            try
            {
                Kernel.print(File.ReadAllText(Kernel.dir() + dir));
            } catch (Exception e)
            {
                Kernel.deGrep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to cat file: " + e.ToString());
                Console.ResetColor();
            }
        }

        public static void PRTCLR()
        {
            fs.Format("0", "FAT32", false);
        }

        public static void Corrupt()
        {
            fs.Format("0", "NTFS", false);
        }

        public static void To(string file, string acc)
        {
            try
            {
                File.WriteAllText(file, acc);
            }
            catch (Exception e)
            {
                Kernel.deGrep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to 'to' file: " + e.ToString());
                Console.ResetColor();
            }
        }
    }
}
