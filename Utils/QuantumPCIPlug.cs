using System;
using IL2CPU.API.Attribs;
using Cosmos.HAL;
using Cosmos.Core;
using Cosmos.HAL.Network;
using Cosmos.HAL.Drivers;
using Quantum.Drawing;

namespace Quantum.Utils
{
    [Plug(Target = typeof(Cosmos.HAL.Global))]
    public static class QuantumPCIPlug
    {

        static public void Init(TextScreenBase textScreen, bool InitScrollWheel, bool InitPS2, bool InitNetwork, bool IDEInit)
        {
            var _SVGAIIDevice = PCI.GetDevice(VendorID.VMWare, DeviceID.SVGAIIAdapter);

            if (_SVGAIIDevice != null && PCI.Exists(_SVGAIIDevice) && VBE.IsAvailable() == false)
            {
                Kernel.AConsole = new GraphicalConsole();
            } else 
                Kernel.AConsole = VBEAvailable() ? new GraphicalConsole() : new VGAConsole(textScreen);
            Kernel.clear();
            Kernel.print("PCI Work Started...");
            PCI.Setup();
            Kernel.print("PCI Work Finished!");

            Kernel.print("[ Loading... ]");
            Kernel.print("Starting Cosmos kernel...");

            Kernel.print("ACPI Work Started...");
            ACPI.Start();
            Kernel.print("ACPI Work Finished");

            Cosmos.HAL.Network.NetworkInit.Init();
        }
        private static bool VBEAvailable()
        {
            if (BGAExists())
            {
                return true;
            }
            else if (PCI.Exists(VendorID.VirtualBox, DeviceID.VBVGA))
            {
                return true;
            }
            else if (PCI.Exists(VendorID.Bochs, DeviceID.BGA))
            {
                return true;
            }
            else if (VBE.IsAvailable())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks whether the Bochs Graphics Adapter exists (not limited to Bochs)
        /// </summary>
        /// <returns></returns>
        private static bool BGAExists()
        {
            return VBEDriver.ISAModeAvailable();
        }
    }
}
