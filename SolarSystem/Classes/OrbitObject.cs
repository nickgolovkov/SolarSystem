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
    abstract class OrbitObject : SpaceObject
    {
        SpaceObject center;

        public double Orbit
        {
            get
            {
                return (Canvas.GetLeft(spaceObject) + Radius - center.Position.X);
            }
            set
            {
                Canvas.SetLeft(spaceObject, center.Position.X + value - Radius);
                Canvas.SetTop(spaceObject, center.Position.Y - Radius);
            }
        }

        public override Point Position
        {
            get => base.Position;
        }

        public OrbitObject(string name, double radius, SpaceObject center, double orbit, string texturePath = ""): base(name, radius, texturePath)
        {
            this.center = center;
            Orbit = orbit;
        }
    }
}
