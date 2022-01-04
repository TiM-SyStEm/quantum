using System;
using System.IO;


namespace Quantum
{
    class KernelRW
    {
        public static void Init()
        {
            Kernel.print("Checking .sysr, .sysw files");
            KernelRW.CheckFiles();
            KernelRW.SetR("sysr");
            KernelRW.SetR("sysw");
            Kernel.print("RW permissions was restored success!");
        }

        public static void SetR(string file)
        {
            if (File.ReadAllText(Kernel.dir() + "sysr").Contains(file)) return;
            File.AppendAllText(Kernel.dir() + "sysr", file + "\n");
        }

        public static void SetW(string file)
        {
            if (File.ReadAllText(Kernel.dir() + "sysw").Contains(file)) return;
            File.AppendAllText(Kernel.dir() + "sysw", file + "\n");
        }

        public static bool IsR(string file)
        {
            return File.ReadAllText(Kernel.dir() + "sysr").Contains(file);
        }

        public static bool IsW(string file)
        {
            return File.ReadAllText(Kernel.dir() + "sysw").Contains(file);
        }

        public static void CheckFiles()
        {
            if (!File.Exists(Kernel.dir() + "sysr"))
            {
                VFS.Touch("sysr");
            }

            if (!File.Exists(Kernel.dir() + "sysw"))
            {
                VFS.Touch("sysw");
            }
        }
    }
}
