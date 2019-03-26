using System;
using System.Collections.Generic;
using static System.Math;

namespace PixelHilbert
{
    static class PixelWright
    {
        public static Func<Point, int, Point> move = (p, n) => n % 2 == 0 ? new Point { x = p.x + (n == 0 ? 1 : -1), y = p.y } : new Point { x = p.x, y = p.y + (n == 1 ? 1 : -1) };
        public static void HueCycle(this Pixel[,] img, Func<List<int>,List<int>> map)
        {
            Point coord = new Point { x = 0, y = 0 };
            List<int> direx = new List<int>();
            int i = 0;
            byte red = 255; byte green = 0; byte blue = 0;
            while (i < img.Length)
            {
                img[coord.y, coord.x] = new Pixel { r = red, g = green, b = blue };
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
        public static void WorkPixelwise(this Pixel[,] img, Func<Pixel,Pixel> way)
        {
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    img[i,j] = way(img[i, j]);
                }
            }
        }
        public static Pixel SphericGreyscale(Pixel p)
        {
            byte v = (byte)Sqrt((Pow(p.r, 2) + Pow(p.g, 2) + Pow(p.b, 2)) / 3);
            return new Pixel { r = v, g = v, b = v };
        }
        public static Pixel FlattenValue(Pixel p)
        {
            double ratio = 255 / Sqrt((Pow(p.r, 2) + Pow(p.g, 2) + Pow(p.b, 2)));
            p.r = (byte)(ratio * p.r);
            p.g = (byte)(ratio * p.g);
            p.b = (byte)(ratio * p.b);
            return p;
        }
        //freaking sucks
        public static Pixel ExplodeSaturation(Pixel p)
        {
            byte vsq = (byte)((Pow(p.r, 2) + Pow(p.g, 2) + Pow(p.b, 2)) / 3);
            double k; double l; double m;
            var q = new byte[3]; int i;
            if (p.r > p.g)
            {
                if (p.g > p.b)
                {
                    //blue min
                    k = p.r;
                    l = p.g;
                    m = p.b;
                    i = 0;
                }
                else
                {
                    //green min
                    k = p.b;
                    l = p.r;
                    m = p.g;
                    i = 1;
                }
            }
            else
            {
                if (p.r > p.b)
                {
                    //blue min
                    k = p.r;
                    l = p.g;
                    m = p.b;
                    i = 0;
                }
                else
                {
                    //red min
                    k = p.g;
                    l = p.b;
                    m = p.r;
                    i = 2;
                }
            }
            k = Sqrt((Pow(k, 2) - Pow(m, 2)) / (1 - Pow(m, 2) / vsq));
            l = Sqrt((Pow(l, 2) - Pow(m, 2)) / (1 - Pow(m, 2) / vsq));
            q[0] = (byte)(k > 255 ? 255 : k);
            q[1] = (byte)(l > 255 ? 255 : l);
            q[2] = 0;
            return new Pixel { r = q[(3 - i) % 3], g = q[(4 - i) % 3], b = q[(2 - i) % 3] };
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
