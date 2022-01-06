using System;
using System.IO;
using FS = Cosmos.System.FileSystem;

namespace Quantum
{
    class VFS
    {
        public static FS.CosmosVFS fs = new FS.CosmosVFS();

        public static void TryInit()
        {
            VFS.Init();
        }

        public static void Init()
        {
            FS.VFS.VFSManager.RegisterVFS(fs);
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print("Drive: " + d.Name);
                Kernel.print("Type: " + d.DriveFormat);
                if (d.DriveFormat != "FAT32")
                {
                    Kernel.print("Drive " + d.Name + " is not FAT32. Want to format it?");
                    if (Console.ReadKey(true).KeyChar == 'y')
                    {
                        Kernel.print("Formatting '" + d.Name + "'");
                        Format(d.Name);
                        Kernel.print("Success!");
                    }
                    else
                    {
                        Kernel.print("Skipped.");
                    }
                }
                Kernel.print("Filesystem prepared success!");
            }
        }

        public static void Format(string name, string type)
        {
            int index = -1;
            for (int i = 0; i < DriveInfo.GetDrives().Length; i++)
            {
                Kernel.print(DriveInfo.GetDrives()[i].Name);
                Kernel.print(name);
                if (DriveInfo.GetDrives()[i].Name[0] == name[0])
                {
                    index = i;
                }
            }

            if (index == -1)
            {
                Kernel.err("pdu: partition doesn't exist!");
                return;
            }

            var drive = fs.GetDisks()[index];
            for (int i = 0; i < drive.Partitions.Count; i++)
            {
                drive.FormatPartition(i, type, true);
            }
        }

        public static void SLB(int index, string label)
        {
            Kernel.print(index.ToString());
            Kernel.print(label);
            fs.SetFileSystemLabel(index.ToString() + @":\", label);
        }

        public static void Format(string v1)
        {
            Format(v1 + ":\\", "FAT32");
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

        public static void MBSize()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + (d.AvailableFreeSpace / 1024).ToString());
            }
        }

        public static void TMBSize()
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + ": " + (d.TotalFreeSpace / 1024).ToString());
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

        public static void Summary()
        {
            Kernel.print("Name    Label    Type    Size    TSize    MBSize    TMBSize");
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Kernel.print(d.Name + "    " + d.VolumeLabel + "    " + DriveT(d.DriveType) + "    " + d.AvailableFreeSpace + "    " + d.TotalSize + "    " + (d.AvailableFreeSpace / 1024) + "    " + (d.TotalFreeSpace / 1024));
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
                    if (file == "sysr") Kernel.magic(".sysr             SysFile");
                    else if (file == "sysw") Kernel.magic(".sysw             SysFile");
                    else if (file == "dtts") Kernel.magic(".dtts             SysFile");
                    else if (KernelRW.IsR(file)) Kernel.magic(file + "             ReadOnly");
                    else if (KernelRW.IsW(file)) Kernel.magic(file + "             WriteOnly");
                    else Kernel.print(file);
                }
            }
            catch (Exception e)
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
            }
            catch (Exception e)
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
                if (KernelRW.IsR(v) || KernelRW.IsW(v))
                {
                    Kernel.err("Permission deined");
                    return;
                }
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
                if (KernelRW.IsW(dir))
                {
                    Kernel.err("Permission denied!");
                    return;
                }
                Kernel.print(File.ReadAllText(Kernel.dir() + dir));
            }
            catch (Exception e)
            {
                Kernel.deGrep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to cat file: " + e.ToString());
                Console.ResetColor();
            }
        }

        public static void PRTCLR()
        {
            Format(Kernel.dir());
        }

        public static void Corrupt()
        {
            Kernel.print("'corrupt' is depricated because of it's useless and dangerous!");
            Kernel.print("it was a bad idea to implement it in first versions.");
        }

        public static void To(string file, string acc)
        {
            try
            {
                if (KernelRW.IsR(file))
                {
                    Kernel.err("Permission denied");
                    return;
                }
                File.WriteAllText(Kernel.dir() + file, acc);
            }
            catch (Exception e)
            {
                Kernel.deGrep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to 'to' file: " + e.ToString());
                Console.ResetColor();
            }
        }

        public static void SZOF(string path)
        {
            Kernel.print(File.ReadAllBytes(Kernel.dir() + path).Length.ToString());
        }

        public static bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                if (KernelRW.IsR(fileName))
                {
                    Kernel.err("Permission denied");
                    return false;
                }
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }
    }
}
