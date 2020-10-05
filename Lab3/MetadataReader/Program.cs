using System;
using System.IO;
using System.Threading;

namespace MetadataReader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                PathChecker(args[0]);
            }
            else
            {
                Console.WriteLine("Input filepath to be read: ");
                var path = Console.ReadLine();
                Console.WriteLine();
                PathChecker(path);
            }
        }

        private static void PathChecker(string path)
        {
            try
            {
                RunProgram(path);
            }

            catch (Exception)
            {
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Please insert a proper path from your computer: ");
                path = Console.ReadLine();
                PathChecker(path);
            }
        }

        private static void RunProgram(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found!");
            }
            var fileStream = new FileStream(path, FileMode.Open);

            var data = DataChecker.ReadFileHeader(fileStream);

            var fileType = DataChecker.FileChecker(data);
            var fileResultString = fileType != Filetypes.Invalid
                ? $"This file is a {fileType}.\n"
                : "This file is invalid!";

            Console.WriteLine($"{fileResultString}");

            var resolution = DataChecker.GetResolution(fileStream, fileType);
            Console.WriteLine($"The resolution is {resolution}\n");
            Console.WriteLine(DataChecker.GetImageInfo(fileStream));
            Console.ReadKey();
        }
    }
}