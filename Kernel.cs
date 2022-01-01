using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys = Cosmos.System;

namespace Quantum
{
    public class Kernel : Sys.Kernel
    {
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
