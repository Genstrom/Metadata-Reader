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
                RunProgram(args[0]);
            }
            else
            {
                Console.WriteLine("Input filepath to be read: ");
                var path = Console.ReadLine();
                RunFile(path);
            }
        }

        private static void RunFile(string path)
        {
            try
            {
                RunProgram(path);
            }

            catch (Exception)
            {
                Console.WriteLine("Please insert a proper path from your computer");
                path = Console.ReadLine();
                RunFile(path);
            }
        }

        private static void RunProgram(string path)
        {
            if (!File.Exists(path)) Console.WriteLine("File not found!");
            var fileStream = new FileStream(path, FileMode.Open);

            var data = Datachecker.ReadFileData(fileStream);

            var fileResult = Datachecker.FileChecker(data);
            var s = !Equals(fileResult, Filetypes.Invalid)
                ? $"This file is a {fileResult}."
                : "This file is invalid!";


            Console.WriteLine($"{s}");

            Datachecker.GetResolution(fileStream, fileResult);
            Datachecker.GetChunkInfo(fileStream);
        }
    }
}