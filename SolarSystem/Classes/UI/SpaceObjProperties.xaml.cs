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

namespace SolarSystem.Classes.UI
{
    /// <summary>
    /// Логика взаимодействия для SpaceObjProperties.xaml
    /// </summary>
    public partial class SpaceObjProperties : UserControl
    {
        SpaceObject spaceObj;

        public SpaceObjProperties(SpaceObject spaceObj, Point pos, Canvas canvas)
        {
            InitializeComponent();

            this.spaceObj = spaceObj;

            txtblockName.Text = spaceObj.Name;
            txtboxRadius.Text = spaceObj.Radius.ToString();

            Show(pos, canvas);
        }

        private void Show(Point pos, Canvas canvas)
        {
            foreach (UIElement el in canvas.Children)
            {
                if (el is SpaceObjProperties)
                {
                    canvas.Children.Remove(el);
                    break;
                }
            }

            canvas.Children.Add(this);

            double WIDTH = 320;
            double HEIGHT = 140;

            if (pos.X + WIDTH < SystemParameters.PrimaryScreenWidth)
            {
                Canvas.SetLeft(this, pos.X);
            }
            else
            {
                Canvas.SetRight(this, canvas.ActualWidth - pos.X);
            }

            if (pos.Y + HEIGHT < SystemParameters.PrimaryScreenHeight)
            {
                Canvas.SetTop(this, pos.Y);
            }
            else
            {
                Canvas.SetBottom(this, canvas.ActualHeight - pos.Y);
            }

        }

        private void txtboxRadius_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                spaceObj.Radius = Convert.ToDouble(txtboxRadius.Text);
            }
            catch { }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Canvas parent = Parent as Canvas;
            parent.Children.Remove(this);
        }
    }
}
