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

        public virtual double Orbit
        {
            get
            {
                return (Canvas.GetLeft(spaceObject) + Radius - center.Position.X - center.Radius);
            }
            set
            {
                Canvas.SetLeft(spaceObject, center.Position.X + center.Radius + value - Radius);
                Canvas.SetTop(spaceObject, center.Position.Y - Radius);

                orbitObject.Width = (Orbit + center.Radius) * 2;
                orbitObject.Height = orbitObject.Width;
                Canvas.SetLeft(orbitObject, center.Position.X - center.Radius - Orbit);
                Canvas.SetTop(orbitObject, center.Position.Y - center.Radius - Orbit);
            }
        }

        public override Point Position
        {
            get => base.Position;
        }

        private Ellipse orbitObject = new Ellipse()
        {
            Stroke = new SolidColorBrush(Colors.White),
            Opacity = 0.5,
            StrokeThickness = 0.6
        };


        public OrbitObject(string name, double radius, SpaceObject center, double orbit, string texturePath = ""): base(name, radius, texturePath)
        {
            this.center = center;
            Orbit = orbit;

            if (this is Satellite)
            {
                orbitObject.StrokeThickness = 0.3;
            }
        }

        public override void Show(Canvas canvas)
        {
            canvas.Children.Add(orbitObject);
            base.Show(canvas);
        }
    }
}
