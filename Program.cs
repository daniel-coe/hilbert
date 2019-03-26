using System;
using System.Collections.Generic;
using System.Linq;
using static PixelHilbert.PixelWright;

namespace PixelHilbert
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateImageFive();
        }
        static void GenerateImageFive()
        {
            var image = new Pixel[255, 384];
            var header = image.ReadImage("sample0.bmp");
            image.WorkPixelwise(ExplodeSaturation);
            image.WriteImage(header, "gen8.bmp");
        }
        static void GenerateImageOne()
        {
            var image = new Pixel[32, 32];
            image.HueCycle(IterateHilbert);
            image.WorkPixelwise(SphericGreyscale);
            var header = new byte[54];
            header.Read("1024.bmp");
            image.WriteImage(header, "gen1.bmp");
        }
        static List<int> IterateHilbert(List<int> ds)
        {
            for (int i = 0; i < 2; i++)
            {
                var temp = ds.ToArray<int>();
                ds.Add(1 - i);
                foreach (var d in temp)
                {
                    ds.Add((5 - d) % 4);
                }
                ds.Add(i);
                foreach (var d in temp)
                {
                    ds.Add((5 - d) % 4);
                }
                ds.Add(3 - i);
                foreach (var d in temp)
                {
                    ds.Add((d + 2) % 4);
                }
            }
            return ds;
        }
    }
}
