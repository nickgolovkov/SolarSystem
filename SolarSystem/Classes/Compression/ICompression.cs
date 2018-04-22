using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SolarSystem.Classes.Compression
{
    interface ICompression
    {
        void Compress(string outputFile);

        void Decompress(string outputFile);
    }
}
