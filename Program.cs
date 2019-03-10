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
            Random rand = new Random();
            Point coord = new Point { x = 0, y = 0 };
            List<int> direx = null;
            int i = 0;
            byte red = 255; byte green = 0; byte blue = 0;
            Pixel[,] image = new Pixel[32, 32];
            byte[] header = new byte[54];
            Func<Point, int, Point> move = (p, n) => n % 2 == 0 ? new Point { x = p.x + n == 0 ? 1 : -1, y = p.y } : new Point { x = p.x, y = p.y + n == 1 ? 1 : -1 };
            while (i < 1024)
            {
                image[coord.x, coord.y] = new Pixel { r = red, g = green, b = blue };
                if (i==direx.Capacity)
                {

                }
                switch (i / 255 % 6)
                {
                    case 0:
                        green++;
                        break;
                    case 1:
                        red--;
                        break;
                    case 2:
                        blue++;
                        break;
                    case 3:
                        green--;
                        break;
                    case 4:
                        red++;
                        break;
                    case 5:
                        blue--;
                        break;
                }
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
    struct Point
    {
        public int x;
        public int y;
    }
}
