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
            VFS.Init();
            Console.WriteLine("Quantum boot was success. Entering kernel shell");
        }

        protected override void Run()
        {
            while (true)
            {
                string prompt = ShellPrompt();
                string[] parts = prompt.Split(" ");
                switch(parts[0])
                {
                    case "echo":
                    {
                        for (int i = 1; i < parts.Length; i++)
                        {
                            Console.Write(parts[i] + " ");
                        }
                        Console.WriteLine();
                        break;
                    }

                    case "cdr":
                    {
                        break;
                    }
                    case "shutdown":
                    {
                        Sys.Power.Shutdown();
                        break;
                    }
                }
            }
        }

        public string ShellPrompt()
        {
            Console.Write("|>");
            return Console.ReadLine();
        }
    }
}
