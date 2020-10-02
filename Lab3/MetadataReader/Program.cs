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

            //PNG files: banana, mushroom, poop
            //BMP files: rpg, buck, meta


            //Console.WriteLine("Input filepath to be read: ");
            //string path = Console.ReadLine();

            var fileStream = new FileStream(@"C:\Users\Tommy\Desktop\Labbar\Lab3\Pictures\rpg.bmp", FileMode.Open);
            Enum Invalid = (Filetypes)2;

            byte[] data = ReadFileData(fileStream);

            var fileResult = Datachecker.FileChecker(data);
            var s = !Equals(fileResult, Invalid)
                ? $"This file is a {fileResult}"
                : $"This file is invalid!";

            Console.WriteLine($"{s}");
            
            BMPResolution(fileStream);


        }

        
        static byte[] ReadFileData(FileStream fileStream)
        {
            var data = new byte[8];

            fileStream.Read(data, 0, 8);

            return data;
        }

        static void BMPResolution(FileStream fileStream)
        {
            var data = new byte[8];

            fileStream.Seek(18, SeekOrigin.Begin);
            fileStream.Read(data, 0, 8);
            fileStream.Close();
            //Shifting by one byte to get the decimal value for width and height.
            int width = data[0]  + (data[1] << 8) + (data[2] << 16) + (data[3] << 24);
            int height = data[4] + (data[5] << 8) + (data[6] << 16) + (data[7] << 24);

            Console.WriteLine($"The resolution is: {width}x{height}.");

        }
    }
}
