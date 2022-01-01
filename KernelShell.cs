using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Quantum
{
    class KernelShell
    {

        public static string dir = "";
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
                case "reboot":
                    {
                        Sys.Power.Reboot();
                        break;
                    }
                case "clear":
                    {
                        Console.Clear();
                        break;
                    }
                case "mkdir":
                    {
                        VFS.Mkdir(parts[1]);
                        break;
                    }
                case "cd":
                    {
                        KernelShell.dir = parts[1];
                        break;
                    }

                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unknown command: " + prompt);
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
