using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Classes
{
    class Satellite: OrbitObject
    {
        public Satellite(string name, double radius, Planet center, double orbit, string texturePath = "") : base(name, radius, center, orbit, texturePath)
        {
            center.satellites.Add(this);
        }
    }
}
