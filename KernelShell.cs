using Cosmos.System.Graphics;
using System;
using System.IO.Compression;
using static Cosmos.HAL.VGADriver;
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
            switch (parts[0].ToLower())
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
                        Interpret("reboot");
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
                        Kernel.print("===================================================");
                        Kernel.print("      This software is part of quantum-project!");
                        Kernel.print("           Hello - greets you everyday.");
                        Kernel.print("===================================================");
                        Kernel.print("Hello, World!");
                        break;
                    }
                case "curl":
                    {
                        KernelCurl.start(parts);
                        break;
                    }
                case "date":
                    {
                        Kernel.print(new DateTime().ToString());
                        break;
                    }
                case "zipper":
                    {
                        KernelPacker.interact(parts);
                        break;
                    }
                case "sizeof":
                    {
                        VFS.SZOF(parts[1]);
                        break;
                    }
                case "permissions":
                    {
                        switch (parts[1])
                        {
                            case "r":
                                {
                                    KernelRW.SetR(parts[2]);
                                    break;
                                }
                            case "w":
                                {
                                    KernelRW.SetW(parts[2]);
                                    break;
                                }
                            case "de":
                                {
                                    switch (parts[2])
                                    {
                                        case "r":
                                            {
                                                KernelRW.DeR(parts[3]);
                                                break;
                                            }
                                        case "w":
                                            {
                                                KernelRW.DeW(parts[3]);
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "help":
                                {
                                    Kernel.print("        This software is part of quantum-project.");
                                    Kernel.print("      Permissions - file's permission manager. 1.0.0");
                                    Kernel.print("===============================================================");
                                    Kernel.print("r FILENAME - make FILENAME readonly");
                                    Kernel.print("w FILENAME - make FILENAME writeonly");
                                    Kernel.print("de r FILENAME - make FILENAME not readonly");
                                    Kernel.print("de w FILENAME - make FILENAME not writeonly");
                                    Kernel.print("===============================================================");
                                    break;
                                }
                            default:
                                {
                                    Kernel.err("permissions: unknown command");
                                    break;
                                }
                        }
                        break;
                    }
                case "tts":
                    {
                        switch (parts[1])
                        {
                            case "help":
                                {
                                    Kernel.print("       This software is part of quantum-project.");
                                    Kernel.print("        TTS - Temrinal-Text-Size util. 1.0.0");
                                    Kernel.print("====================================================");
                                    Kernel.print("help - see all text modes");
                                    Kernel.print("usage: tts 80x25");
                                    Kernel.print("====================================================");
                                    break;
                                }
                            case "modes":
                                {
                                    Kernel.print("40x25\n40x50\n80x25\n80x50\n90x30\n90x50");
                                    break;
                                }
                            case "40x25":
                                {
                                    VGAScreen.SetTextMode(TextSize.Size40x25);
                                    break;
                                }
                            case "40x50":
                                {
                                    VGAScreen.SetTextMode(TextSize.Size40x50);
                                    break;
                                }
                            case "80x25":
                                {
                                    VGAScreen.SetTextMode(TextSize.Size80x25);
                                    break;
                                }
                            case "80x50":
                                {
                                    VGAScreen.SetTextMode(TextSize.Size80x50);
                                    break;
                                }
                            case "90x30":
                                {
                                    VGAScreen.SetTextMode(TextSize.Size90x30);
                                    break;
                                }
                            case "90x50":
                                {
                                    VGAScreen.SetTextMode(TextSize.Size90x60);
                                    break;
                                }
                        }
                        break;
                    }
                case "dtts":
                    {
                        KernelDTTS.Set(parts[1]);
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
