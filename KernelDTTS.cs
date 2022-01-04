using Cosmos.System.Graphics;
using System;
using System.IO;
using static Cosmos.HAL.VGADriver;

namespace Quantum
{
    class KernelDTTS
    {
        public static void Init()
        {
            Console.WriteLine("Checking .dtts file");
            KernelDTTS.CheckFiles();
            Console.WriteLine("Setting DTTS mode!");
            KernelDTTS.Parse(File.ReadAllText(Kernel.dir() + "dtts"));
            Console.WriteLine("TTS Mode: " + File.ReadAllText(Kernel.dir() + "dtts"));
            KernelRW.SetR("dtts");
        }

        public static void CheckFiles()
        {
            if (!File.Exists(Kernel.dir() + "dtts"))
            {
                File.Create(Kernel.dir() + "dtts");
            }
        }

        public static void Set(string mode)
        {
            if (mode != "help" && mode != "modes" && mode != "40x25" && mode != "40x50" && mode != "80x25" && mode != "80x50" && mode != "90x25" && mode != "90x50" && mode != "")
            {
                Kernel.err("dtts: unknown tts mode or command");
                return;
            } else
            {
                File.WriteAllText("0:\\dtts", mode);
                Parse(mode);
            }
        }

        public static void Parse(string text)
        {
            Kernel.clear();
            switch (text.Trim())
            {
                case "":
                    {
                        break;
                    }
                case "help":
                    {
                        Kernel.print("       This software is part of quantum-project.");
                        Kernel.print("      DTTS - Default-Temrinal-Text-Size util. 1.0.0");
                        Kernel.print("====================================================");
                        Kernel.print("      Sets choosen terminal size after boot!");
                        Kernel.print("usage: ?");
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
        }
    }
}
