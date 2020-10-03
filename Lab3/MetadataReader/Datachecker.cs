using System;
using System.IO;
using System.Text;

namespace MetadataReader
{
    public enum Filetypes
    {
        PNG,
        BMP,
        Invalid
    }

    public static class Datachecker
    {
        private static readonly byte[] BmpSignature = {0x42, 0x4D};
        private static readonly byte[] PngSignature = {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A};

        public static Filetypes FileChecker(byte[] data)
        {
            switch (data[0])
            {
                case 0x89:
                {
                    for (var i = 0; i < PngSignature.Length; i++)
                        if (data[i] != PngSignature[i])
                            return Filetypes.Invalid;
                    return Filetypes.PNG;
                }
                case 0x42:
                    for (var i = 0; i < BmpSignature.Length; i++)
                        if (data[i] != BmpSignature[i])
                            return Filetypes.Invalid;
                    return Filetypes.BMP;

                default:
                    return Filetypes.Invalid;
            }
        }

        public static void GetResolution(FileStream fileStream, Filetypes filetype)
        {
            var data = new byte[8];
            var width = 0;
            var height = 0;

            switch (filetype)
            {
                case Filetypes.BMP:
                    fileStream.Seek(18, SeekOrigin.Begin);
                    fileStream.Read(data, 0, 8);

                    //Shifting by one byte to get the decimal value for width and height.
                    width = data[0] + (data[1] << 8) + (data[2] << 16) + (data[3] << 24);
                    height = data[4] + (data[5] << 8) + (data[6] << 16) + (data[7] << 24);

                    Console.WriteLine($"The resolution is: {width}x{height}.");
                    break;
                case Filetypes.PNG:
                    fileStream.Seek(16, SeekOrigin.Begin);
                    fileStream.Read(data, 0, 8);

                    //Shifting by one byte to get the decimal value for width and height.
                    width = data[3] + (data[2] << 8) + (data[1] << 16) + (data[0] << 24);
                    height = data[7] + (data[6] << 8) + (data[5] << 16) + (data[4] << 24);

                    Console.WriteLine($"The resolution is: {width}x{height}.");
                    break;
            }
        }

        public static byte[] ReadFileData(FileStream fileStream)
        {
            var data = new byte[8];

            fileStream.Read(data, 0, 8);

            return data;
        }

        public static void GetChunkInfo(FileStream fileStream)
        {
            var sizeArray = new byte[4];
            var typeArray = new byte[4];
            var size = 0;
            var offset = 8;


            while (offset + size < fileStream.Length)
            {
                fileStream.Seek(offset + size, SeekOrigin.Begin);
                fileStream.Read(sizeArray, 0, 4);
                fileStream.Read(typeArray, 0, 4);
                offset += size;

                size = 12 + sizeArray[3] + (sizeArray[2] << 8) + (sizeArray[1] << 16) + (sizeArray[0] << 24);
                var typeString = Encoding.ASCII.GetString(typeArray);

                Console.WriteLine($"Chunk type/name: {typeString}, chunksize: {size} bytes.");
            }
        }
    }
}