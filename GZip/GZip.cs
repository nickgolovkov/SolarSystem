using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using SolarSystem.Classes.Compression;

namespace GZip
{
    public class GZip: ICompressor
    {
        public string Name { get; } = "GZip";

        public string Format { get; } = ".gz";

        public GZip() { }

        public void Compress(string inputFile, string outputFile)
        {
            using (FileStream input = File.OpenRead(inputFile))
            using (FileStream output = File.Create(outputFile))
            using (GZipStream gzip = new GZipStream(output, CompressionMode.Compress))
            {
                input.CopyTo(gzip);
            }
        }

        public void Decompress(string inputFile, string outputFile)
        {
            using (FileStream input = File.OpenRead(inputFile))
            using (FileStream output = File.Create(outputFile))
            using (GZipStream gzip = new GZipStream(input, CompressionMode.Decompress))
            {
                gzip.CopyTo(output);
            }
        }
    }
}
