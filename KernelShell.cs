using Cosmos.System.Graphics;
using System;
using static Cosmos.HAL.PCIDevice;
using static Cosmos.HAL.VGADriver;
using Dat = Cosmos.HAL.RTC;
using Sys = Cosmos.System;

namespace Quantum
{
    class KernelShell
    {

        public static string dir = "0";
        public static Utils.KernelMemoryMonitor Monitor = new Utils.KernelMemoryMonitor();
        private static string ShellPrompt()
        {
            Kernel.printk(dir == "0" ? "root~" : "root~" + dir + "~");
            return Utils.ConsoleImpl.ReadLine();
        }

        private static string ExtendedPrompt()
        {
            Kernel.printk(">");
            string prompt = Utils.ConsoleImpl.ReadLine();
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
                        Kernel.print(Date());
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
                            case "90x60":
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
                case "tech":
                    {
                        Monitor.Monitor();
                        Kernel.print("Computer name: " + Kernel.version);
                        Kernel.print("Date and time: " + Date());
                        Kernel.print("Boot time: " + Kernel.bootTime);
                        Kernel.print("Amount of RAM: " + Cosmos.Core.CPU.GetAmountOfRAM());
                        Kernel.print("Free RAM: " + Monitor.FreeMemory);
                        Kernel.print("CPU brand string: " + Cosmos.Core.CPU.GetCPUBrandString());
                        break;
                    }
                case "lspci":
                    {
                        int count = 0;
                        foreach (Cosmos.HAL.PCIDevice device in Cosmos.HAL.PCI.Devices)
                        {
                            Kernel.print(D2(device.bus) + ":" + D2(device.slot) + ":" + D2(device.function) + " - " + "0x" + D4(DecToHex(device.VendorID)) + ":0x" + D4(DecToHex(device.DeviceID)) + " : " + DeviceClass.GetTypeString(device) + ": " + DeviceClass.GetDeviceString(device));
                            count++;
                        }
                        Kernel.print("Total " + count + " devices");
                        break;
                    }
                case "force":
                    {
                        Kernel.AConsole = parts[1] == "graphical" ? new Drawing.GraphicalConsole() : new Drawing.VGAConsole(null);
                        break;
                    }

                default:
                    {
                        Kernel.err("Unknown command: " + prompt);
                        break;
                    }
            }
        }

        public static string D4(string text)
        {
            if (text.Length < 4)
            {
                switch (text.Length)
                {
                    case 3:
                        return "0" + text;
                    case 2:
                        return "00" + text;
                    case 1:
                        return "000" + text;
                    default:
                        return text;
                }
            }
            else
            {
                return text;
            }
        }

        public static string DecToHex(int x)
        {
            string result = "";

            while (x != 0)
            {
                if ((x % 16) < 10)
                    result = x % 16 + result;
                else
                {
                    string temp = "";

                    switch (x % 16)
                    {
                        case 10: temp = "A"; break;
                        case 11: temp = "B"; break;
                        case 12: temp = "C"; break;
                        case 13: temp = "D"; break;
                        case 14: temp = "E"; break;
                        case 15: temp = "F"; break;
                    }

                    result = temp + result;
                }

                x /= 16;
            }

            return result;
        }

        public static string D2(uint number)
        {
            if (number < 10)
            {
                return "0" + number;
            }
            else
            {
                return number.ToString();
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

        public static string Date()
        {
            return "20" + Dat.Year + "   Month " + Dat.Month + " Day of the month:" + Dat.DayOfTheMonth + "   " + Dat.Hour + ":" + (Dat.Second.ToString().Length == 2 ? Dat.Second : "0" + Dat.Second);
        }
    }
}
