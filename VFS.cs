using System;
using System.Collections.Generic;
using System.Text;
using FS = Cosmos.System.FileSystem;

namespace Quantum
{
    class VFS
    {
        static FS.CosmosVFS fs = new FS.CosmosVFS();
        public static void Init()
        {
            Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
        }
    }
}
