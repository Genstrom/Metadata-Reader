using System;
using System.IO;

namespace MetadataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //The first eight bytes of a PNG datastream always contain the following (decimal) values:
            //137 80 78 71 13 10 26 10 => Hex: 89 50 4E 47 0D 0A 1A 0A

            //BMP = LSB = Little Endian

            //PNG files: banana, mushroom, poop = 640x616
            //BMP files: rpg, buck, meta


            //Console.WriteLine("Input filepath to be read: ");
            //string path = Console.ReadLine();

            if (args.Length != 0)
            {
                RunProgram(args[0]);
            }
            else
            {
                RunProgram(@"C:\Users\Tommy\Desktop\Labbar\Lab3\Pictures\banana.png");
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
