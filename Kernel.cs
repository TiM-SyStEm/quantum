using System;
using System.Threading;
using Sys = Cosmos.System;

namespace Quantum
{
    public class Kernel : Sys.Kernel
    {

        public static string acc = String.Empty;
        public static bool grep = false;
        public static string version = "142022";
        public static double bootTime = 0;
        public static Drawing.Console AConsole;

        public static string dir()
        {
            return KernelShell.dir + ":\\";
        }

        protected override void BeforeRun()
        {
            Utils.Global.Init(null);
            Kernel.print("Global initialization success!");
            int old = Cosmos.HAL.RTC.Second;
            changeColor(ConsoleColor.Green);
            VFS.TryInit();
            KernelDTTS.Init();
            KernelCurl.Init();
            try
            {
                KernelRW.Init();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to register RW permissions. Please reboot system or repair it");
                Console.WriteLine(e.ToString());
            }
            Kernel.print("Quantum boot was success. Entering kernel shell");
            Console.Beep();
            bootTime = Cosmos.HAL.RTC.Second - old;
            bootTime = Math.Abs(bootTime);
            Drawing.Logo.Show();
            resetColor();
        }

        protected override void Run()
        {
            try
            {
                KernelShell.start();
            }
            catch (Exception e)
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
            Kernel.AConsole.Write(text.ToCharArray());
        }

        public static void print(string text)
        {
            if (grep)
            {
                acc += text + "\n";
                return;
            }
            Utils.ConsoleImpl.WriteLine(text);
        }

        public static void print()
        {
            print("");
        }

        public static void changeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            AConsole.Foreground = color;
        }

        public static void resetColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            AConsole.Foreground = ConsoleColor.White;
        }

        internal static void Stop(string aName, string aDescription, string lastsknowaddress, string ctxinterrupt)
        {
            Kernel.clear();
            Kernel.err("** CPU Exception **");
            Kernel.err(aName);
            Kernel.err(aDescription);
            Kernel.err("Last known address: " + lastsknowaddress);
            Kernel.err("CTXINT: " + ctxinterrupt);
        }

        public static void clear()
        {
            if (grep)
            {
                acc = String.Empty;
                return;
            }
            Utils.ConsoleImpl.Clear();
        }

        public static string deGrep()
        {
            grep = false;
            string a = acc;
            acc = String.Empty;
            return a;
        }

        public static void err(string text)
        {
            Utils.ConsoleImpl.set_ForegroundColor(ConsoleColor.Red);
            Kernel.print(text);
            Utils.ConsoleImpl.ResetColor();
        }

        public static void magic(string v)
        {
            changeColor(ConsoleColor.Magenta);
            Kernel.print(v);
            Utils.ConsoleImpl.ResetColor();
        }
    }
}
