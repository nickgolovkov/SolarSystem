using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Classes
{
    public class Satellite : OrbitObject
    {
        public Satellite(string name, double radius, Planet center, double orbit, double period, string texturePath = ""): base(name, radius, center, orbit, period, texturePath)
        {
            center.satellites.Add(this);
        }
    }
}
