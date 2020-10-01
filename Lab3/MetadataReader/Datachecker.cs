using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataReader
{
    public enum Filetypes
    {
        Png,
        Bmp,
        Invalid
    }
    public class Datachecker
    {
        private readonly byte[] _bmpSignature;
        private readonly byte[] _pngSignature;


        public Datachecker()
        {
            _pngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            _bmpSignature = new byte[] { 0x42, 0x4D };
        }


        public Enum FileChecker(byte[] data)
        {
            switch (data.Length)
            {
                case 8:
                    {
                        for (var i = 0; i < data.Length; i++)
                        {
                            if (data[i] != _pngSignature[i])
                                return (Filetypes)3;
                            return (Filetypes)1;
                        }

                        break;
                    }
                case 2:
                    for (var i = 0; i < data.Length; i++)
                    {
                        if (data[i] != _bmpSignature[i])

                            return (Filetypes)3;
                        return (Filetypes)2;
                    }

                    break;
            }

            return (Filetypes)3;
        }
    }
}

