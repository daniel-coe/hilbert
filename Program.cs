using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PixelHilbert
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0; int y = 0; int i = 0;
            Pixel[,] image = new Pixel[32, 32];
            byte[] header = new byte[54];
            while (i < 1024)
            {
                x = i / 32;
                y = i % 32;
                image[x, y] = new Pixel { r = (byte)i, g = (byte)i, b = (byte)i };
                i++;
            }
            using (FileStream fStream = File.Open(@"C:\Users\Daniel Coe\Pictures\1024.bmp", FileMode.Open))
            {
                for (int j = 0; j < header.Length; j++)
                {
                    header[j] = (byte)fStream.ReadByte();
                }
            }
            using (FileStream fStream = File.Open(@"C:\Users\Daniel Coe\Pictures\gen0.bmp", FileMode.Create))
            {
                byte[] file = new byte[header.Length + 3 * i];
                for (int j = 0; j < file.Length; j++)
                {
                    if (j < 54)
                    {
                        file[j] = header[j];
                    }
                    else
                    {
                        Pixel p = image[(j - 54) / 96, (j / 3 - 18) % 32];
                        switch (j % 3)
                        {
                            case 0:
                                file[j] = p.b;
                                break;
                            case 1:
                                file[j] = p.g;
                                break;
                            case 2:
                                file[j] = p.r;
                                break;
                        }
                    }
                }
                fStream.Write(file, 0, file.Length);
            }
        }
    }
    struct Pixel
    {
        public byte r;
        public byte g;
        public byte b;
    }
}
