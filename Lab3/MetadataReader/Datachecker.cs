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
        private static readonly byte[] _bmpSignature = new byte[] { 0x42, 0x4D };
        private static readonly byte[] _pngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        public static Filetypes FileChecker(byte[] data)
        {
            switch (data[0])
            {
                case 0x89:
                    {
                        for (var i = 0; i < _pngSignature.Length; i++)
                        {
                            if (data[i] != _pngSignature[i])
                                return Filetypes.Invalid;
                        }
                        return Filetypes.PNG;
                    }
                case 0x42:
                    for (var i = 0; i < _bmpSignature.Length; i++)
                    {
                        if (data[i] != _bmpSignature[i])
                            return Filetypes.Invalid;
                    }
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

                default:
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

            var file = new byte[fileStream.Length];
            var sizeArray = new byte[4];
            var typeArray = new byte[4];
            var type = "";
            var size = 0;
            var sizeNow = 8;

            fileStream.Seek(8, SeekOrigin.Begin);
            fileStream.Read(sizeArray, 0, 4);
            fileStream.Seek(12, SeekOrigin.Begin);
            fileStream.Read(typeArray, 0, 4);

            size = +12 + sizeArray[0] + sizeArray[1] + sizeArray[2] + sizeArray[3];
            //sizeNow = size;
            type = Encoding.ASCII.GetString(typeArray);

            Console.WriteLine($"Chunk type/name: {type}, chunksize: {size} bytes.");

            for (int i = 0; i < fileStream.Length; i++)
            {
                fileStream.Seek(sizeNow + size, SeekOrigin.Begin);
                fileStream.Read(sizeArray, 0, 4);
                fileStream.Seek(sizeNow + size + 4, SeekOrigin.Begin);
                fileStream.Read(typeArray, 0, 4);
                sizeNow = size;

                size = +12 + sizeArray[0] + sizeArray[1] + sizeArray[2] + sizeArray[3];
                type = Encoding.ASCII.GetString(typeArray);

                Console.WriteLine($"Chunk type/name: {type}, chunksize: {size} bytes.");


            }


        }
    }
}

