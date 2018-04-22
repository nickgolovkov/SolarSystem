using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace SolarSystem.Classes.Compression
{
    public class Zip : ICompression, IDisposable
    {
        private Stream input;
        public string inputFile;

        public Zip(string inputFile)
        {
            input = File.OpenRead(inputFile);
            this.inputFile = inputFile;
        }

        public void Compress(string outputFile)
        {
            using (FileStream output = File.Create(outputFile))
            {
                using (ZipArchive zip = new ZipArchive(output, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry entry = zip.CreateEntry(Path.GetFileName(inputFile));
                    using (Stream stream = entry.Open())
                    {
                        input.CopyTo(stream);
                    }
                }
            }
        }

        public void Decompress(string outputFile)
        {
            ZipFile.ExtractToDirectory(inputFile, Path.GetDirectoryName(outputFile));
        }

        public void Dispose()
        {
            input.Close();
        }
    }
}
