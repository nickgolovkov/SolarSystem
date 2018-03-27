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
    class PlanetWithRings: Planet
    {
        public override double Orbit
        {
            get
            {
                return base.Orbit;
            }
            set
            {
                base.Orbit = value;
                SetRings();
            }
        }

        public double RingsRadius
        {
            get
            {
                return ringsObject.Width / 2;
            }
            set
            {
                ringsObject.Width = value * 2;
                ringsObject.Height = value * 2;
            }
        }

        private ImageSource RingsTexture
        {
            get
            {
                BrushConverter brushConverter = new BrushConverter();
                return (ImageSource)brushConverter.ConvertTo(ringsObject.Fill, typeof(ImageSource)); ;
            }
            set
            {
                ringsObject.Fill = new ImageBrush(value);
            }
        }

        private Ellipse ringsObject = new Ellipse();

        public PlanetWithRings(string name, double radius, double ringsRadius, Star center, double orbit, string texturePath = "", string ringsTexturePath = ""): base(name, radius, center, orbit, texturePath)
        {
            RingsRadius = ringsRadius;

            if (ringsTexturePath == "")
            {
                RingsTexture = LoadTexture("Textures/" + name + "Rings.png");
            }
            else
            {
                RingsTexture = LoadTexture(texturePath);
            }

            SetRings();
        }

        public override void Show(Canvas canvas)
        {
            base.Show(canvas);
            canvas.Children.Add(ringsObject);
        }

        private void SetRings()
        {
            Canvas.SetLeft(ringsObject, Canvas.GetLeft(spaceObject) + Radius - RingsRadius);
            Canvas.SetTop(ringsObject, Canvas.GetTop(spaceObject) + Radius - RingsRadius);
        }
    }
}
