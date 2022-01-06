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
            Kernel.print("Checking .dtts file");
            KernelDTTS.CheckFiles();
            Kernel.print("Setting DTTS mode!");
            KernelDTTS.Parse(File.ReadAllText(Kernel.dir() + "dtts"));
            Kernel.print("TTS Mode: " + File.ReadAllText(Kernel.dir() + "dtts"));
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
            File.WriteAllText("0:\\dtts", mode);
            Parse(mode);
        }

        public static void Parse(string text)
        {
            if (text == "") return;
            Utils.ConsoleImpl.SetWindowSize(int.Parse(text.Split('x')[0]), int.Parse(text.Split('x')[1]));
        }
    }
}
