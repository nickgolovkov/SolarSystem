using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace SolarSystem.Classes.Compression
{
    public class GZip: ICompression, IDisposable
    {
        private FileStream input;

        public GZip(string inputFile)
        {
            input = File.OpenRead(inputFile);
        }

        public void Compress(string outputFile)
        {
            using (FileStream output = File.Create(outputFile))
            {
                using (GZipStream gzip = new GZipStream(output, CompressionMode.Compress))
                {
                    input.CopyTo(gzip);
                }
            }
        }

        public void Decompress(string outputFile)
        {
            using (FileStream output = File.Create(outputFile))
            {
                using (GZipStream gzip = new GZipStream(input, CompressionMode.Decompress))
                {
                    gzip.CopyTo(output);
                }
            }
        }

        public void Dispose()
        {
            input.Close();
        }
    }
}
