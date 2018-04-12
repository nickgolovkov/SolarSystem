using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Classes
{
    static class Randomizer
    {
        static private Random random = new Random();

        static public double GetRandomAngle()
        {
            return random.NextDouble() * Math.PI * 2;
        }
    }
}
