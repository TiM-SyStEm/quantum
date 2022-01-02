using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;

namespace Quantum
{
    class KernelShell
    {

        public static string dir = "";
        public static string previous = "";
        private static string ShellPrompt()
        {
            Console.Write(dir == "" ? "root~" : "root~" + dir + "~");
            return Console.ReadLine();
        }

        private static string ExtendedPrompt()
        {
            Console.Write(">");
            string prompt = Console.ReadLine();
            return prompt.EndsWith("\\") ? prompt + ExtendedPrompt() : prompt;
        }

        public static void Interpret(string prompt)
        {
            string[] parts = prompt.Split(" ");
            switch (parts[0])
            {
                case "echo":
                    {
                        for (int i = 1; i < parts.Length; i++)
                        {
                            Kernel.printk(parts[i] + " ");
                        }
                        Kernel.print();
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
                case "prtclr":
                    {
                        VFS.PRTCLR();
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
                case "reboot":
                    {
                        Sys.Power.Reboot();
                        break;
                    }
                case "clear":
                    {
                        Kernel.clear();
                        break;
                    }
                    
                case "cat":
                    {
                        VFS.Cat(parts[1]);
                        break;
                    }
                case "grep":
                    {
                        KernelGrep.Grep(parts);
                        break;
                    }
                case "to":
                    {
                        string acc = String.Empty;
                        for (int i = 2; i < parts.Length; i++)
                        {
                            acc += parts[i] + " ";
                        }
                        VFS.To(parts[1], acc);
                        break;
                    }

                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Kernel.print("Unknown command: " + prompt);
                        Console.ResetColor();
                        break;
                    }
            }
        }
        public static void start()
        {
            while (true)
            {
                string prompt = ShellPrompt();
                if (prompt.EndsWith("\\"))
                {
                    prompt += ExtendedPrompt();
                }
                prompt = prompt.Replace("\\", "");
                Interpret(prompt);
            }
        }
    }
}
