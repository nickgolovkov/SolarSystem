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
    public class PlanetWithRings : Planet
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
                SetRingsPosition();
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

        public string SerializeRingsTexture
        {
            get
            {
                ImageSourceConverter converter = new ImageSourceConverter();
                return converter.ConvertToString(RingsTexture);
            }
            set
            {
                ImageSourceConverter converter = new ImageSourceConverter();
                RingsTexture = converter.ConvertFromString(value) as ImageSource;
            }
        }
        
        private ImageSource RingsTexture
        {
            get
            {
                ImageBrush imgBrush = ringsObject.Fill as ImageBrush;
                return imgBrush.ImageSource;
            }
            set
            {
                ringsObject.Fill = new ImageBrush(value);
            }
        }
        
        private Ellipse ringsObject = new Ellipse()
        {
            Cursor = Cursors.Hand
        };

        public PlanetWithRings(): base()
        {
            ringsObject.MouseRightButtonDown += ShowProperties;
        }

        protected PlanetWithRings(SerializationInfo info, StreamingContext context): base(info, context)
        {
            RingsRadius = info.GetDouble("RingsRadius");
            SerializeRingsTexture = info.GetString("SerializeRingsTexture");

            ringsObject.MouseRightButtonDown += ShowProperties;
        }

        public PlanetWithRings(string name, double radius, double ringsRadius, Star center, double orbit, double period, string texturePath = "", string ringsTexturePath = ""): base(name, radius, center, orbit, period, texturePath)
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

            SetRingsPosition();

            ringsObject.MouseRightButtonDown += ShowProperties;
        }

        public override void Show(Canvas canvas)
        {
            base.Show(canvas);
            canvas.Children.Add(ringsObject);
        }

        private void SetRingsPosition()
        {
            Canvas.SetLeft(ringsObject, Canvas.GetLeft(spaceObject) + Radius - RingsRadius);
            Canvas.SetTop(ringsObject, Canvas.GetTop(spaceObject) + Radius - RingsRadius);
        }

        protected override void SetPosition()
        {
            base.SetPosition();
            SetRingsPosition();
        }

        public override void Delete(Canvas canvas)
        {
            canvas.Children.Remove(ringsObject);
            base.Delete(canvas);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("RingsRadius", RingsRadius);
            info.AddValue("SerializeRingsTexture", SerializeRingsTexture);
        }
    }
}
