using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Quantum
{
    class KernelShell
    {
        public static string ShellPrompt()
        {
            Console.Write("|>");
            return Console.ReadLine();
        }
        public static void start()
        {
            while (true)
            {
                string prompt = ShellPrompt();
                string[] parts = prompt.Split(" ");
                switch (parts[0])
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
                            VFS.CDR();
                            break;
                        }
                    case "rm":
                        {
                            VFS.RM(parts[1]);
                            break;
                        }
                    case "touch":
                        {
                            VFS.Touch(parts[1]);
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
    }
}
