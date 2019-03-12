using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelHilbert
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateImageTwo();
        }
        static void GenerateImageTwo()
        {
            var image = new Pixel[255, 384];
            var header = image.ReadImage("sample0.bmp");
            image = PixelWright.SphericGreyscale(image);
            image.WriteImage(header, "gen2.bmp");
        }
        static void GenerateImageOne()
        {
            var image = new Pixel[32, 32];
            var map = new Func<List<int>, List<int>>(IterateHilbert);
            image.HueCycle(map);
            image = PixelWright.SphericGreyscale(image);
            var header = new byte[54];
            header.Read("1024.bmp");
            image.WriteImage(header, "gen1.bmp");
        }
        static List<int> IterateHilbert(List<int> ds)
        {
            for (int i = 0; i < 2; i++)
            {
                var temp = ds.ToArray<int>();
                ds.Add((1 - i) % 4);
                foreach (var d in temp)
                {
                    ds.Add((1 - d) % 4);
                }
                ds.Add(i % 4);
                foreach (var d in temp)
                {
                    ds.Add((1 - d) % 4);
                }
                ds.Add((3 - i) % 4);
                foreach (var d in temp)
                {
                    ds.Add((d + 2) % 4);
                }
            }
            return ds;
        }
    }
}
