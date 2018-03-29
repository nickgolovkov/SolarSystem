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
    public abstract class OrbitObject : SpaceObject
    {
        SpaceObject center;

        public virtual double Orbit
        {
            get
            {
                return orbitObject.Width / 2;
            }
            set
            {
                orbitObject.Width = value * 2;
                orbitObject.Height = orbitObject.Width;

                SetPosition();
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

        public double AngularVelocity
        {
            get
            {
                return Math.PI * 2 / period;
            }
            set
            {
                period = Math.PI * 2 / value;
            }
        }

        public double period;
        
        private double angle = 0;

        public OrbitObject(string name, double radius, SpaceObject center, double orbit, double period, string texturePath = ""): base(name, radius, texturePath)
        {
            this.period = period;
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

        public void Rotate()
        {
            angle += AngularVelocity;
            SetPosition();
        }

        protected virtual void SetPosition()
        {
            Canvas.SetLeft(orbitObject, center.Position.X - Orbit);
            Canvas.SetTop(orbitObject, center.Position.Y - Orbit);

            double x = Math.Cos(angle) * Orbit;
            double y = Math.Sin(angle) * Orbit;

            Canvas.SetLeft(spaceObject, center.Position.X + x - Radius);
            Canvas.SetTop(spaceObject, center.Position.Y + y - Radius);
        }
    }
}
