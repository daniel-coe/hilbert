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
                        int w = pixels.GetLength(1); int m = (i - 54) / 3 / w; int n = (i / 3 - 18 + w) % w;
                        switch (i % 3)
                        {
                            case 0:
                                file[i] = pixels[m,n].b;
                                break;
                            case 1:
                                file[i] = pixels[m,n].g;
                                break;
                            case 2:
                                file[i] = pixels[m,n].r;
                                break;
                        }
                    }
                }
                fStream.Write(file, 0, file.Length);
            }
        }
        public static byte[] ReadImage(this Pixel[,] pixels, string path)
        {
            var header = new byte[54];
            using (FileStream fStream = File.Open(@"C:\Users\Daniel Coe\Pictures\" + path, FileMode.Open))
            {
                for (int i = 0; i < header.Length; i++)
                {
                    header[i] = (byte)fStream.ReadByte();
                }
                for (int i = 0; i < 3 * pixels.Length; i++)
                {
                    int w = pixels.GetLength(1); int m = i / 3 / w; int n = (i / 3 + w) % w;
                    switch (i % 3)
                    {
                        case 0:
                            pixels[m, n].b = (byte)fStream.ReadByte();
                            break;
                        case 1:
                            pixels[m, n].g = (byte)fStream.ReadByte();
                            break;
                        case 2:
                            pixels[m, n].r = (byte)fStream.ReadByte();
                            break;
                    }
                }
            }
            return header;
        }
    }
}
