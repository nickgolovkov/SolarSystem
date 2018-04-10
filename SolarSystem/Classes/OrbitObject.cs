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
        protected SpaceObject Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
                SetPosition();
            }
        }
        private SpaceObject center;

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

        public OrbitObject(): base() { }

        public OrbitObject(string name, double radius, SpaceObject center, double orbit, double period, string texturePath = ""): base(name, radius, texturePath)
        {
            this.period = period;
            this.Center = center;
            Orbit = orbit;

            if (this is Satellite)
            {
                orbitObject.StrokeThickness = 0.3;
            }

            Canvas.SetZIndex(orbitObject, -1);
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
            if (Center == null)
            {
                return;
            }

            Canvas.SetLeft(orbitObject, Center.Position.X - Orbit);
            Canvas.SetTop(orbitObject, Center.Position.Y - Orbit);

            double x = Math.Cos(angle) * Orbit;
            double y = Math.Sin(angle) * Orbit;

            Canvas.SetLeft(spaceObject, Center.Position.X + x - Radius);
            Canvas.SetTop(spaceObject, Center.Position.Y + y - Radius);
        }

        public override void Delete(Canvas canvas)
        {
            DeleteFromCenterList();

            canvas.Children.Remove(orbitObject);
            base.Delete(canvas);
        }

        protected abstract void DeleteFromCenterList();

        public static void SetCenters(Star star)
        {
            foreach (Planet planet in star.planets)
            {
                planet.Center = star;
                SetCenters(planet);
            }
        }

        public static void SetCenters(Planet planet)
        {
            foreach (Satellite satellite in planet.satellites)
            {
                satellite.Center = planet;
            }
        }
    }
}
