using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SolarSystem.Classes
{
    [Serializable]
    public class Satellite : OrbitObject
    {
        public Satellite() : base() { }

        protected Satellite(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public Satellite(string name, double radius, Planet center, double orbit, double period, string texturePath) : base(name, radius, center, orbit, period, texturePath)
        {
            center.satellites.Add(this);
        }

        protected override void DeleteFromCenterList()
        {
            Planet planet = Center as Planet;
            planet.satellites.Remove(this);
        }
    }
}
