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
    class Star: SpaceObject
    {
        public Point Position
        {
            get
            {
                Point pos = new Point();
                pos.X = Canvas.GetLeft(spaceObject) + Radius;
                pos.Y = Canvas.GetTop(spaceObject) + Radius;
                return pos;
            }
            set
            {
                Canvas.SetLeft(spaceObject, value.X - Radius);
                Canvas.SetTop(spaceObject, value.Y - Radius);
            }
        }

        public List<Planet> planets = new List<Planet>();

        public Star(string name, double radius, Point pos): base(name, radius)
        {
            Position = pos;
        }

        public Star(string name, string texturePath, double radius, Point pos): base(name, texturePath, radius)
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
