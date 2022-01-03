using System;
using Sys = Cosmos.System;

namespace Quantum
{
    public class Kernel : Sys.Kernel
    {

        public static string acc = String.Empty;
        public static bool grep = false;

        public static string dir()
        {
            return KernelShell.dir + ":\\";
        }
        protected override void BeforeRun()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            VFS.TryInit();
            KernelCurl.Init();
            Console.WriteLine("Quantum boot was success. Entering kernel shell");
            Console.ResetColor();
            Console.Beep();
        }

        protected override void Run()
        {
            try
            {
                KernelShell.start();
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Caught error: " + e.ToString());
                Console.ResetColor();
                Run();
            }
        }

        public static void printk(string text)
        {
            if (grep)
            {
                acc += text;
                return;
            }
            Console.Write(text);
        }

        public static void print(string text)
        {
            if (grep)
            {
                acc += text + "\n";
                return;
            }
            Console.WriteLine(text);
        }

        public static void print()
        {
            print("");
        }

        public static void clear()
        {
           if (grep)
            {
                acc = String.Empty;
                return;
            }
            Console.Clear();
        }

        public static string deGrep()
        {
            grep = false;
            string a = acc;
            acc = String.Empty;
            return a;
        }
    }
}
