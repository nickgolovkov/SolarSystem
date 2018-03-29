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

namespace SolarSystem.Classes
{
    public class Star : SpaceObject
    {
        public int PlanetsCount
        {
            get
            {
                return planets.Count;
            }
        }

        public List<Planet> planets = new List<Planet>(); 

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
    }
}
