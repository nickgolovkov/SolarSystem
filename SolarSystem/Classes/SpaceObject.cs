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
using SolarSystem.Classes.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SolarSystem.Classes
{
    [Serializable]
    public abstract class SpaceObject : ISerializable
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
        
        public virtual double Radius
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

        public Canvas ParentCanvas
        {
            get
            {
                return spaceObject.Parent as Canvas;
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
        
        public string SerializeTexture
        {
            get
            {
                ImageSourceConverter converter = new ImageSourceConverter();
                return converter.ConvertToString(Texture);
            }
            set
            {
                ImageSourceConverter converter = new ImageSourceConverter();
                Texture = converter.ConvertFromString(value) as ImageSource;
            }
        }

        private ImageSource Texture
        {
            get
            {
                ImageBrush imgBrush = spaceObject.Fill as ImageBrush;
                return imgBrush.ImageSource;
            }
            set
            {
                spaceObject.Fill = new ImageBrush(value);
            }
        }
        
        protected Ellipse spaceObject = new Ellipse()
        {
            Cursor = Cursors.Hand,
        };

        public SpaceObject()
        {
            spaceObject.MouseRightButtonDown += ShowProperties;
        }

        protected SpaceObject(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Radius = info.GetDouble("Radius");
            Position = (Point)info.GetValue("Position", typeof(Point));
            SerializeTexture = info.GetString("SerializeTexture");
            
            spaceObject.MouseRightButtonDown += ShowProperties;
        }
        
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
            Canvas parent = ParentCanvas.Parent as Canvas;
            SpaceObjProperties spaceObjProperties = new UI.SpaceObjProperties(this, Mouse.GetPosition(parent), parent);
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

        public virtual void Delete(Canvas canvas)
        {
            canvas.Children.Remove(spaceObject);
        }
        
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Radius", Radius);
            info.AddValue("Position", Position);
            info.AddValue("SerializeTexture", SerializeTexture);
        }
    }
}
