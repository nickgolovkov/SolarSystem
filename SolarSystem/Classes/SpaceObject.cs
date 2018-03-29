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
    public abstract class SpaceObject
    {
        public string Name
        {
            get
            {
                return spaceObject.Name;
            }
            set
            {
                spaceObject.Name = value;
                
            }
        }

        public double Radius
        {
            get
            {
                return spaceObject.Width / 2;
            }
            set
            {
                Point pos = Position;

                spaceObject.Width = value * 2;
                spaceObject.Height = value * 2;

                Position = pos;
            }
        }

        public virtual Point Position
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

        private ImageSource Texture
        {
            get
            {
                BrushConverter brushConverter = new BrushConverter();
                return (ImageSource)brushConverter.ConvertTo(spaceObject.Fill, typeof(ImageSource)); ;
            }
            set
            {
                spaceObject.Fill = new ImageBrush(value);
            }
        }

        protected Ellipse spaceObject = new Ellipse()
        {
            Cursor = Cursors.Hand
        };

        public SpaceObject(string name, double radius, string texturePath = "")
        {
            Name = name;
            Radius = radius;
            if (texturePath == "")
            {
                Texture = LoadTexture("Textures/" + name + ".png");
            }
            else
            {
                Texture = LoadTexture(texturePath);
            }

            spaceObject.MouseRightButtonDown += ShowProperties;
        }

        protected void ShowProperties(object sender, MouseButtonEventArgs e)
        {
            Canvas parent = (spaceObject.Parent as Canvas).Parent as Canvas;
            UI.SpaceObjProperties spaceObjProperties = new UI.SpaceObjProperties(this, Mouse.GetPosition(parent), parent);
        }

        public virtual void Show(Canvas canvas)
        {
            if (!canvas.Children.Contains(spaceObject))
            {
                canvas.Children.Add(spaceObject);
            }
        }

        protected BitmapImage LoadTexture(string path)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(path, UriKind.Relative);
            bm.EndInit();

            return bm;
        }
        
    }
}
