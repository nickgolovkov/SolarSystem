using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using SolarSystem.Classes.Compression;

namespace Zip
{
    public class Zip : ICompressor
    {
        public string Name { get; } = "Zip";

        public string Format { get; } = ".zip";

        public string inputFile;

        public Zip() { }

        public void Compress(string inputFile, string outputFile)
        {
            using (FileStream input = File.OpenRead(inputFile))
            using (FileStream output = File.Create(outputFile))
            using (ZipArchive zip = new ZipArchive(output, ZipArchiveMode.Create))
            {
                ZipArchiveEntry entry = zip.CreateEntry(Path.GetFileName(inputFile));
                using (Stream stream = entry.Open())
                {
                    input.CopyTo(stream);
                }
            }
        }

        public void Decompress(string inputFile, string outputFile)
        {
            ZipFile.ExtractToDirectory(inputFile, Path.GetDirectoryName(outputFile));
        }
    }
}