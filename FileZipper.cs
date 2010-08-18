using System;
using System.IO;
using System.IO.Compression;

namespace zip
{

    public class zip
    {

        public static void Main()
        {
            Console.WriteLine("Usage: <directory>");
            
            // User generated path
            string DIR_PATH = Console.ReadLine();

            DirectoryInfo di = new DirectoryInfo(DIR_PATH);
            foreach (FileInfo f in di.GetFiles())
            {
                Compress(f);
            }
            foreach (FileInfo f in di.GetFiles("*.zip"))
            {
                Decompress(f);

            }

        }

        public static void Compress(FileInfo f)
        {
            using (FileStream INFILE = f.OpenRead())
            {
                if ((File.GetAttributes(f.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & f.Extension != ".zip")
                {
                    // Create the compressed file.
                    using (FileStream ret =
                                File.Create(f.FullName + ".zip"))
                    {
                        using (GZipStream Compress =
                            new GZipStream(ret,
                            CompressionMode.Compress))
                        {
                            INFILE.CopyTo(Compress);

                            Console.WriteLine("Zipping up files...",
                                f.Name, f.Length.ToString(), ret.Length.ToString());
                        }
                    }
                }
            }
        }

        public static void Decompress(FileInfo f)
        {
            using (FileStream INFILE = f.OpenRead())
            {
                string a = f.FullName;
                string ORIG = a.Remove(a.Length -
                        f.Extension.Length);

                using (FileStream fuck = File.Create(ORIG))
                {
                    using (GZipStream Decompress = new GZipStream(INFILE,
                            CompressionMode.Decompress))
                    {
                        Decompress.CopyTo(fuck);

                        Console.WriteLine("Decompressed {0}", f.Name);

                    }
                }
            }
        }

    }
}
