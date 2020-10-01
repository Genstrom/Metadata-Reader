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


            byte[] data = ReadFileData(fileStream);

            var pngResult = PNGChecker(data) ? "File is a png." : "File is not a png.";
            var bmpResult = BMPChecker(data) ? "File is a bmp." : "File is not a bmp.";
            BMPResolution(fileStream);
            Console.WriteLine($"{pngResult}\n{bmpResult}");


        }

        static bool PNGChecker(byte[] data)
        {
            var pngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            for (int i = 0; i < 8; i++)
            {
                if (data[i] != pngSignature[i])
                {
                    return false;
                }
            }

            return true;
        }

        static bool BMPChecker(byte[] data)
        {
            var BMPSignature = new byte[] { 0x42, 0x4D };

            for (int i = 0; i < 2; i++)
            {
                if (data[i] != BMPSignature[i])
                {
                    return false;
                }
            }

            return true;
        }

        static byte[] ReadFileData(FileStream fileStream)
        {
            var data = new byte[8];

            fileStream.Read(data, 0, 8);
            //fileStream.Close();

            return data;
        }

        static void BMPResolution(FileStream fileStream)
        {
            var data = new byte[8];
            var width = "";
            var height = "";

            fileStream.Seek(18, SeekOrigin.Begin);
            fileStream.Read(data, 0, 8);
            fileStream.Close();

            //Separates the dimensions to a width and height byte array.
            for (int i = 3; i > -1; i--)
            {
                width += data[i].ToString("X2");
                height += data[i + 4].ToString("X2");
            }

            int x = Convert.ToInt32(width, 16);
            int y = Convert.ToInt32(height, 16);

            Console.WriteLine($"The resolution is: {x}x{y}.");

        }


    }
}
