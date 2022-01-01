using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys = Cosmos.System;

namespace Quantum
{
    public class Kernel : Sys.Kernel
    {

        public static string dir()
        {
            return KernelShell.dir == "" ? @"0:\" : @"0:\" + KernelShell.dir;
        }
        protected override void BeforeRun()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            VFS.Init();
            Console.WriteLine("Quantum boot was success. Entering kernel shell");
            Console.ResetColor();
        }

        protected override void Run()
        {
            KernelShell.start();
        }
    }
}
