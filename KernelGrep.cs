using System;
using System.IO;

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
            for (; position < parts.Length; position++)
            {
                if (parts[position] == "|") break;
                acc += parts[position] + " ";
            }
            position++; // skip "|"

            for (; position < parts.Length; position++)
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

        public static void redirect(string[] parts)
        {
            Kernel.grep = true;
            int position = 1;
            string filename = String.Empty;
            string acc = String.Empty;
            for (; position < parts.Length; position++)
            {
                if (parts[position] == "|") break;
                acc += parts[position] + " ";
            }
            position++; // skip "|"
            filename = parts[position];
            KernelShell.Interpret(acc);
            string output = Kernel.deGrep();
            try
            {
                File.WriteAllText(Kernel.dir() + filename, output);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Kernel.print("Unable to redirect input: " + e.ToString());
                Console.ResetColor();
            }
        }
    }
}
