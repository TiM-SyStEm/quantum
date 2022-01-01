using System;
using System.Collections.Generic;
using System.Text;
using FS = Cosmos.System.FileSystem;
using System.IO;

namespace Quantum
{
    class VFS
    {
        static FS.CosmosVFS fs = new FS.CosmosVFS();
        public static void Init()
        {
            Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("Type: {0}", d.DriveFormat);
                if (d.DriveFormat != "FAT32")
                {
                    Console.WriteLine("Formatting '" + d.Name + "'");
                    fs.Format(d.Name, "FAT32", false);
                    Console.WriteLine("Success!");
                }
                Console.WriteLine("Filesystem prepared success!");
            }
        }

        public static void CDR()
        {
            var directory_list = Directory.GetFiles(Kernel.dir());
            foreach (var file in directory_list)
            {
                Console.WriteLine(file);
            }
        }

        public static void Touch(string name)
        {
            try
            {
                File.Create(Kernel.dir() + name);
            } catch (Exception e)
            {
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
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to remove: " + e.ToString());
                Console.ResetColor();
            }
        }

        public static void Mkdir(string dir)
        {
            try
            {
                Directory.CreateDirectory(Kernel.dir());
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to create dir: " + e.ToString());
                Console.ResetColor();
            }
        }
    }
}
