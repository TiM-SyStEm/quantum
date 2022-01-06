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


        public static void DeR(string path)
        {
            if (KernelRW.SystemFile(path))
            {
                Kernel.err("Permission denied");
                return;
            }
            string acc = String.Empty;
            string[] current = File.ReadAllLines("0:\\sysr");
            foreach (string line in current)
            {
                if (path == line) continue;
                acc += line + "\n";
            }
            File.WriteAllText("0:\\sysr", acc);
        }

        private static bool SystemFile(string path)
        {
            return path == "sysr" || path == "sysw" || path == "dtts";
        }

        public static void DeW(string path)
        {
            if (KernelRW.SystemFile(path))
            {
                Kernel.err("Permission denied");
                return;
            }
            string acc = String.Empty;
            string[] current = File.ReadAllLines("0:\\sysw");
            foreach (string line in current)
            {
                if (path == line) continue;
                acc += line + "\n";
            }
            File.WriteAllText("0:\\sysw", acc);
        }

        public static void SetR(string file)
        {
            if (Contains("0:\\sysr", file)) return;
            File.AppendAllText("0:\\sysr", file + "\n");
        }

        public static void SetW(string file)
        {
            if (Contains("0:\\sysr", file)) return;
            File.AppendAllText("0:\\sysw", file + "\n");
        }

        private static bool Contains(string v, string file)
        {
            string[] lines = File.ReadAllLines(v);
            foreach (string line in lines)
            {
                if (line == file) return true;
            }
            return false;
        }

        public static bool IsR(string file)
        {
            string[] files = File.ReadAllLines("0:\\sysr");
            foreach (string line in files)
            {
                if (line == file) return true;
            }
            return false;
        }

        public static bool IsW(string file)
        {
            string[] files = File.ReadAllLines("0:\\sysw");
            foreach (string line in files)
            {
                if (line == file) return true;
            }
            return false;
        }

        public static void CheckFiles()
        {
            if (!File.Exists("0:\\sysr"))
            {
                VFS.Touch("sysr");
            }

            if (!File.Exists("0:\\sysw"))
            {
                VFS.Touch("sysw");
            }
        }
    }
}
