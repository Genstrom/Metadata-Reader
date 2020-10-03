using System;
using System.IO;

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
                Console.WriteLine("Please insert a proper path from your computer");
                path = Console.ReadLine();
                PathChecker(path);
            }
        }

        private static void RunProgram(string path)
        {
            if (!File.Exists(path)) Console.WriteLine("File not found!");
            var fileStream = new FileStream(path, FileMode.Open);

            var data = DataChecker.ReadFileData(fileStream);

            var fileResult = DataChecker.FileChecker(data);
            var fileResultString = !Equals(fileResult, Filetypes.Invalid)
                ? $"This file is a {fileResult}."
                : "This file is invalid!";


            Console.WriteLine($"{fileResultString}");

            DataChecker.GetResolution(fileStream, fileResult);
            DataChecker.GetChunkInfo(fileStream);
        }
    }
}