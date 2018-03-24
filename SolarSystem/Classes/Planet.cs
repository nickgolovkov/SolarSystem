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
    class Planet: SpaceObject
    {
        public Star star;

        public double Orbit
        {
            get
            {
                return Canvas.GetLeft(spaceObject) + Radius - star.Position.X;
            }
            set
            {
                Canvas.SetLeft(spaceObject, star.Position.X + value - Radius);
                Canvas.SetTop(spaceObject, star.Position.Y - Radius);
            }
        }

        public Planet(string name, double radius, Star star, double orbit): base(name, radius)
        {
            this.star = star;
            Orbit = orbit;
            star.planets.Add(this);
        }

        public Planet(string name, string texturePath, double radius, Star star, double orbit): base(name, texturePath, radius)
        {
            this.star = star;
            Orbit = orbit;
            star.planets.Add(this);
        }
    }
}
