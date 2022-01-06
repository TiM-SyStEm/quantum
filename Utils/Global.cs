using Cosmos.HAL;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Utils
{
    [Plug(Target = typeof(Cosmos.System.Global))]
    public static class Global
    {
        public static void Init(TextScreenBase textScreen, bool InitScroolWheel = true, bool InitPS2 = true, bool InitNetwork = true, bool IDEInit = true)
        {
            Cosmos.System.Global.mDebugger.Send("Creating Console");

            Quantum.Utils.QuantumPCIPlug.Init(textScreen, InitScroolWheel, InitPS2, InitNetwork, IDEInit);

            Kernel.print("HW Init");

            Cosmos.System.Network.NetworkStack.Init();
            Cosmos.System.Global.mDebugger.Send("Network Stack Init");

            

            Cosmos.System.Global.NumLock = false;
            Cosmos.System.Global.CapsLock = false;
            Cosmos.System.Global.ScrollLock = false;
        }
    }
}
