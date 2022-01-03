using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quantum
{
    class KernelPacker
    {

        public static void interact(string[] parts)
        {
            string opinion = parts[1];
            switch (opinion.ToLower())
            {
                case "pack":
                    {
                        List<string> files = new List<string>();
                        for (int i = 2; i < parts.Length; i++)
                        {
                            files.Add(parts[i]);
                        }
                        KernelPacker.pack(parts[2], files.ToArray());
                        break;
                    }

                case "extract":
                    {
                        KernelPacker.Unpack(parts[2]);
                        break;
                    }
                case "files":
                    {
                        KernelPacker.Files(parts[2]);
                        break;
                    }
                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Kernel.print("zipper: unknown command: " + opinion);
                        Console.ResetColor();
                        break;
                    }
            }
        }

        public static void Unpack(string path)
        {
            Dictionary<string, string> content = parseContent(File.ReadAllText(Kernel.dir() + path));
            foreach (var pair in content)
            {
                VFS.Touch(pair.Key);
                VFS.To(pair.Key, pair.Value);
                Kernel.print("zipper: ok: " + pair.Key);
            }
            Kernel.print("Success!");
        }

        public static void Files(string path)
        {
            List<string> files = new List<string>();
            string package = File.ReadAllText(Kernel.dir() + path);
            string[] lines = package.Split('\n');
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length < 2) continue;
                if (parts[0] == "ARCHIVE_STAMP") files.Add(parts[1]);
            }
            Kernel.print("Name     Size");
            Dictionary<string, string> content = parseContent(package);
            Kernel.print(ToPrettyString<string, string>(content));
            foreach (string file in files)
            {
                Kernel.print(file + "     " + content[file].Length);
            }
        }

        public static Dictionary<string, string> parseContent(string package)
        {
            string[] lines = package.Split('\n');
            Dictionary<string, string> content = new Dictionary<string, string>();
            string acc = String.Empty;
            string header = String.Empty;
            bool writing = false;
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 1 || parts.Length == 0)
                {
                    acc += line + "\n";
                }
                if (parts[0] == "ARCHIVE_STAMP" && header == "")
                {
                    header = parts[1];
                    writing = true;
                }
                else if (parts[0] == "END_ARCHIVE_STAMP" && parts[1] == header) {
                    content[header] = acc;
                    acc = String.Empty;
                    header = String.Empty;
                    writing = false;
                } else {
                    acc += line + "\n";
                }
            }
            return content;
        }

        public static string ToPrettyString<K, V>(Dictionary<K, V> dict)
        {
            var str = new StringBuilder();
            str.Append("{");
            foreach (var pair in dict)
            {
                str.Append(String.Format(" {0}={1} ", pair.Key, pair.Value));
            }
            str.Append("}");
            return str.ToString();
        }

        public static void pack(string archive, string[] files)
        {
            try
            {
                VFS.Touch(archive);
                string acc = String.Empty;
                foreach (var file in files)
                {
                    if (file == archive) continue;
                    string ftext = File.ReadAllText(Kernel.dir() + file);
                    acc += "ARCHIVE_STAMP " + file;
                    acc += "\n";
                    acc += ftext + "\n";
                    acc += "END_ARCHIVE_STAMP " + file + "\n";
                }
                VFS.To(archive, acc);
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Kernel.print("zipper: caught exception: " + e.ToString());
                Console.ResetColor();
            }
        }
    }
}
