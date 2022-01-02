using System;
using System.IO;
using System.Text;

namespace Quantum
{
    class KernelGrep
    {
        public static void Grep(string[] parts)
        {
            Kernel.grep = true;
            int position = 1;
            string acc = String.Empty;
            string regex = String.Empty;
            for (;position < parts.Length; position++)
            {
                if (parts[position] == "|") break;
                acc += parts[position] + " ";
            }
            position++; // skip "|"

            for (;position < parts.Length; position++)
            {
                regex += parts[position];
            }
            KernelShell.Interpret(acc);
            string grepResult = Kernel.deGrep();
            foreach (string line in grepResult.Split("\n"))
            {
                if (line.Contains(regex))
                {
                    Kernel.print(line);
                }   
            }
        }
    }
}
