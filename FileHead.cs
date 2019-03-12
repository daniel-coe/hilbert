using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PixelHilbert
{
    static class FileHead
    {
        public static void Read(this byte[] data, string path)
        {
            using (FileStream fStream = File.Open(@"C:\Users\Daniel Coe\Pictures\" + path, FileMode.Open))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (byte)fStream.ReadByte();
                }
            }
        }
        public static void WriteImage(this Pixel[,] pixels, byte[] header, string path)
        {
            using (FileStream fStream = File.Open(@"C:\Users\Daniel Coe\Pictures\" + path, FileMode.Create))
            {
                byte[] file = new byte[54 + 3 * pixels.Length];
                for (int i = 0; i < file.Length; i++)
                {
                    if (i < 54)
                    {
                        file[i] = header[i];
                    }
                    else
                    {
                        Pixel p = pixels[(i - 54) / 96, (i / 3 - 18) % 32];
                        switch (i % 3)
                        {
                            case 0:
                                file[i] = p.b;
                                break;
                            case 1:
                                file[i] = p.g;
                                break;
                            case 2:
                                file[i] = p.r;
                                break;
                        }
                    }
                }
                fStream.Write(file, 0, file.Length);
            }

        }
    }
}
