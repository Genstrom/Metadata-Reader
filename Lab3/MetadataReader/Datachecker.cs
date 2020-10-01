using System;
using System.Collections.Generic;
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
    }
}

