using System;
using System.IO;

namespace MetadataReader
{
    class Program
    {
        static void Main(string[] args)
        { 
            if (args.Length != 0)
            {
                RunProgram(args[0]);
            }
            else
            {
                Console.WriteLine("Input filepath to be read: ");
                string path = Console.ReadLine();
                RunProgram(path);
            }
        }

        static void RunProgram(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found!");
            }
            var fileStream = new FileStream(path, FileMode.Open);

            byte[] data = Datachecker.ReadFileData(fileStream);

            var fileResult = Datachecker.FileChecker(data);
            var s = !Equals(fileResult, Filetypes.Invalid)
                ? $"This file is a {fileResult}."
                : $"This file is invalid!";

            Console.WriteLine($"{s}");

            Datachecker.GetResolution(fileStream, fileResult);
            Datachecker.GetChunkInfo(fileStream);
        }
    }
}
