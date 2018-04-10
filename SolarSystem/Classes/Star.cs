using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SolarSystem.Classes
{
    [Serializable]
    public class Star : SpaceObject
    {
        public List<Planet> planets = new List<Planet>();

        public Star(): base() { }

        protected Star(SerializationInfo info, StreamingContext context): base(info, context)
        {
            for (int i = 0; i < info.GetInt32("PlanetsCount"); i++)
            {
                planets.Add(info.GetValue("Planet" + i.ToString(), typeof(Planet)) as Planet);
            }
        }

        public Star(string name, double radius, Point pos, string texturePath = ""): base(name, radius, texturePath)
        {
            Position = pos;
        }

        public override void Show(Canvas canvas)
        {
            base.Show(canvas);
            foreach (Planet planet in planets)
            {
                planet.Show(canvas);
            }
        }

        public override void Delete(Canvas canvas)
        {
            List<Planet> temp = new List<Planet>(planets);
            foreach (Planet planet in temp)
            {
                planet.Delete(canvas);
            }
            base.Delete(canvas);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("PlanetsCount", planets.Count);
            for (int i = 0; i < planets.Count; i++)
            {
                info.AddValue("Planet" + i.ToString(), planets[i]);
            }
        }
    }
}
