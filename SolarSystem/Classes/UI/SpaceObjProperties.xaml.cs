using System;
using Microsoft.Win32;
using System.IO;
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
    public partial class SpaceObjProperties : UserControl, IClosableUI
    {
        public SpaceObject spaceObj;
        Point pos;

        public SpaceObjProperties(SpaceObject spaceObj, Point pos, Canvas canvas)
        {
            InitializeComponent();

            this.spaceObj = spaceObj;
            this.pos = pos;

            txtboxName.Text = spaceObj.Name;
            txtboxRadius.Text = spaceObj.Radius.ToString();

            if (spaceObj is OrbitObject)
            {
                InitOrbitObjProperties(spaceObj as OrbitObject);
            }

            Show(canvas);
        }

        private void SpaceObjProperties_Loaded(object sender, RoutedEventArgs e)
        {
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
            MainWindow.ClosePrevUI(canvas);
            canvas.Children.Add(this);
        }

        private void txtboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                spaceObj.Name = txtboxName.Text;
            }
            catch { }
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Close()
        {
            Canvas parent = Parent as Canvas;
            parent.Children.Remove(this);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)chboxBin.IsChecked && !(bool)chboxJson.IsChecked && !(bool)chboxXml.IsChecked)
            {
                return;
            }
            
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.timerMove.Stop();

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = spaceObj.Name;

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                if ((bool)chboxBin.IsChecked)
                {
                    Serializer.BinarySerialize(spaceObj, dlg.FileName + ".bin");
                }
                if ((bool)chboxJson.IsChecked)
                {
                    Serializer.JsonSerialize(spaceObj, dlg.FileName + ".json");
                }
                if ((bool)chboxXml.IsChecked)
                {
                    Serializer.XmlSerialize(spaceObj, dlg.FileName + ".xml");
                }
            }
            
            mainWindow.timerMove.Start();
        }

        private void btnOpenTexture_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.timerMove.Stop();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    spaceObj.Texture = SpaceObject.LoadTexture(dlg.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            mainWindow.timerMove.Start();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            spaceObj.Delete(spaceObj.ParentCanvas);
            Close();
        }
    }
}
