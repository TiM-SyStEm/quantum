using System;
using Sys = Cosmos.System;

namespace Quantum
{
    class KernelShell
    {

        public static string dir = "0";
        private static string ShellPrompt()
        {
            Console.Write(dir == "0" ? "root~" : "root~" + dir + "~");
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
                case "redirect":
                    {
                        KernelGrep.redirect(parts);
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
                case "pdu":
                    {
                        KernelPDU.interact(parts);
                        break;
                    }
                case "corrupt":
                    {
                        VFS.Corrupt();
                        break;
                    }
                case "sel":
                    {
                        dir = parts[1];
                        break;
                    }
                case "hello":
                    {
                        Kernel.print("      This software is part of quantum-project!");
                        Kernel.print("           Hello - greets you everyday.");
                        Kernel.print("===================================================");
                        Kernel.print("Hello, World!");
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
