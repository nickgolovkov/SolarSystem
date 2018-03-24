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
    abstract class SpaceObject
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
                spaceObject.Width = value * 2;
                spaceObject.Height = value * 2;
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

        protected Ellipse spaceObject = new Ellipse();

        public SpaceObject(string name, double radius)
        {
            Name = name;
            Radius = radius;
            Texture = LoadTexture("Textures/" + name + ".png");
        }

        public SpaceObject(string name, string texturePath, double radius)
        {
            Name = name;
            Radius = radius;
            Texture = LoadTexture(texturePath);
        }

        public virtual void Show(Canvas canvas)
        {
            if (!canvas.Children.Contains(spaceObject))
            {
                canvas.Children.Add(spaceObject);
            }
        }

        private BitmapImage LoadTexture(string path)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(path, UriKind.Relative);
            bm.EndInit();

            return bm;
        }
        
    }
}
