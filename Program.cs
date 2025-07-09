using System;
using System.IO;
using System.Threading.Tasks;
using MySortLib;

namespace MySortLib
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            int n;
            int processors;
            int fileCount;
            string type = "int";

            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("  [.exe] <inputfile>");
                Console.WriteLine("  [.exe] --generate [n] [processors] [fileCount] [--i|--f]");
                Console.WriteLine("  [.exe] --run [n] [processors] [fileCount] [--i|--f]");
                return;
            }

            if (args.Length > 0 && args[0] == "--generate")
            {
                //--generate [n] [processors] [fileCount] [--i|--f]
                n = args.Length > 1 ? int.Parse(args[1]) : 8;
                processors = args.Length > 2 ? int.Parse(args[2]) : 1;
                fileCount = args.Length > 3 ? int.Parse(args[3]) : 1;
                if(args.Length > 4)
                {
                    if (args[4] == "--i") type = "int";
                    else if (args[4] == "--f") type = "float";
                }

                var generator = new TextGenerator(n, processors, fileCount, type);
                generator.GenerateTextFile();
                Console.WriteLine("Generated new files.");
                return;
            }

            if (args.Length > 0 && args[0] == "--run")
            {
                //--generate [n] [processors] [fileCount] [--i|--f]
                n = args.Length > 1 ? int.Parse(args[1]) : 8;
                processors = args.Length > 2 ? int.Parse(args[2]) : 1;
                fileCount = args.Length > 3 ? int.Parse(args[3]) : 1;
                if (args.Length > 4)
                {
                    if (args[4] == "--i") type = "int";
                    else if (args[4] == "--f") type = "float";
                }

                var generator = new TextGenerator(n, processors, fileCount, type);
                generator.GenerateTextFile();
                Console.WriteLine("Generated new files.");
                for(int i = 0; i < fileCount; i++)
                {
                    string fileName = $"input{i + 1}.txt";
                    Console.WriteLine($"Running sort on {fileName}...");
                    RunSort(fileName, n, processors);
                }
                return;
            }
            if (!string.IsNullOrEmpty(args[0]))
            {
                string fileName = args[0];
                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"File {fileName} does not exist.");
                    return;
                }
                string[] lines = File.ReadAllLines(args[0]);
                string[] inputline = lines[0].Split(' ');
                n = int.Parse(inputline[0]);
                processors = int.Parse(inputline[1]);
                RunSort(fileName, n, processors);
                return;
            }
        }

        public static void RunSort(string fileName, int n, int processors)
        {
            string[] lines = File.ReadAllLines(fileName);
            float[][] inputs = new float[2 * n][];
            float[][] data = new float[2 * n][];
            for (int i = 0; i < 2 * n; i++)
            {
                inputs[i] = new float[2];
                data[i] = new float[2];
            }
            for (int i = 0; i < n; i++)
            {
                float a = 0; float b = 0;
                string[] inputline = lines[i + 1].Split(' ');
                a = float.Parse(inputline[0]);
                b = float.Parse(inputline[1]);
                if (a == b)
                {
                    Console.WriteLine("Caution: a and b cannot be equal.");
                }
                if (a > b)
                {
                    inputs[2 * i][0] = b;
                    inputs[2 * i + 1][0] = a;
                    inputs[2 * i][1] = 1;
                    inputs[2 * i + 1][1] = -1;
                }
                else
                {
                    inputs[2 * i][0] = a;
                    inputs[2 * i + 1][0] = b;
                    inputs[2 * i][1] = 1;
                    inputs[2 * i + 1][1] = -1;
                }
            }
            data = OddEvenMergeSort.NSort(inputs);
            for (int i = 0; i < 2 * n; i++)
            {
                Console.WriteLine($"{data[i][0]}");
            }
        }

        /*public static void ParallelPrefix(float[][] inputs)
        {

        }*/
    }
}