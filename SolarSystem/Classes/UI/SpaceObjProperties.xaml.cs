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
        Point pos;

        public SpaceObjProperties(SpaceObject spaceObj, Point pos, Canvas canvas)
        {
            InitializeComponent();

            this.spaceObj = spaceObj;
            this.pos = pos;

            txtblockName.Text = spaceObj.Name;
            txtboxRadius.Text = spaceObj.Radius.ToString();

            if (spaceObj is OrbitObject)
            {
                InitOrbitObjProperties(spaceObj as OrbitObject);
            }

            Show(canvas);
        }

        private void SpaceObjProperties_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.KeyDown += SpaceObjProperties_KeyDown;

            Canvas canvas = Parent as Canvas;

            if (pos.X + ActualWidth < SystemParameters.PrimaryScreenWidth)
            {
                Canvas.SetLeft(this, pos.X);
            }
            else
            {
                Canvas.SetRight(this, canvas.ActualWidth - pos.X);
            }

            if (pos.Y + ActualHeight < SystemParameters.PrimaryScreenHeight)
            {
                Canvas.SetTop(this, pos.Y);
            }
            else
            {
                Canvas.SetBottom(this, canvas.ActualHeight - pos.Y);
            }
        }

        private void InitOrbitObjProperties(OrbitObject orbitObj)
        {
            txtboxOrbit.Visibility = Visibility.Visible;
            txtboxOrbit.Text = orbitObj.Orbit.ToString();
            txtboxPeriod.Text = orbitObj.period.ToString();
        }
        
        private void Show(Canvas canvas)
        {
            ClosePrev(canvas);
            canvas.Children.Add(this);
        }

        private void txtboxRadius_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                spaceObj.Radius = Convert.ToDouble(txtboxRadius.Text);
            }
            catch { }
        }

        private void txtboxOrbit_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                (spaceObj as OrbitObject).Orbit = Convert.ToDouble(txtboxOrbit.Text);
            }
            catch { }
        }

        private void txtboxPeriod_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                (spaceObj as OrbitObject).period = Convert.ToDouble(txtboxPeriod.Text);
            }
            catch { }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SpaceObjProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                spaceObj.Delete(spaceObj.ParentCanvas);
                Close();
            }
        }

        private void Close()
        {
            Window window = Window.GetWindow(this);
            window.KeyDown -= SpaceObjProperties_KeyDown;

            Canvas parent = Parent as Canvas;
            parent.Children.Remove(this);
        }

        public static void ClosePrev(Canvas canvas)
        {
            foreach (UIElement el in canvas.Children)
            {
                if (el is SpaceObjProperties)
                {
                    (el as SpaceObjProperties).Close();
                    break;
                }
            }
        }
    }
}
