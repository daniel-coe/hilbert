using System;
using System.Collections.Generic;
using static System.Math;

namespace PixelHilbert
{
    static class PixelWright
    {
        public static Func<Point, int, Point> move = (p, n) => n % 2 == 0 ? new Point { x = p.x + (n == 0 ? 1 : -1), y = p.y } : new Point { x = p.x, y = p.y + (n == 1 ? 1 : -1) };
        public static void HueCycle(this Pixel[,] image, Func<List<int>,List<int>> map)
        {
            Point coord = new Point { x = 0, y = 0 };
            List<int> direx = new List<int>();
            int i = 0;
            byte red = 255; byte green = 0; byte blue = 0;
            while (i < image.Length)
            {
                image[coord.y, coord.x] = new Pixel { r = red, g = green, b = blue };
                if (i == direx.Count)
                {
                    direx = map(direx);
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
                coord = move(coord, direx[i]);
                i++;
            }
        }
        public static Pixel[,] SphericGreyscale(Pixel[,] img)
        {
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    byte v = (byte)Sqrt((Pow(img[i, j].r, 2) + Pow(img[i, j].g, 2) + Pow(img[i, j].b, 2)) / 3);
                    img[i, j] = new Pixel { r = v, g = v, b = v };
                }
            }
            return img;
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
