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
using Microsoft.Win32;
using System.IO;

namespace SolarSystem.Classes.UI
{
    /// <summary>
    /// Логика взаимодействия для SpaceObjChildren.xaml
    /// </summary>
    public partial class SpaceObjChildren : UserControl, IClosableUI
    {
        public ICentral centralObject;
        Point pos;

        public SpaceObjChildren(ICentral centralObject, Point pos, Canvas canvas)
        {
            InitializeComponent();

            this.pos = pos;
            this.centralObject = centralObject;

            txtblockName.Text = (centralObject as SpaceObject).Name + " " + centralObject.GetOrbitObjectsType().Name.ToLower() + "s:";

            ShowChildrenList();
            Show(canvas);
        }

        private void ShowChildrenList()
        {
            listboxOrbitObjects.Items.Clear();
            List<OrbitObject> orbitObj = centralObject.GetOrbitObjects();
            orbitObj.Sort((x, y) => x.Orbit.CompareTo(y.Orbit));

            foreach (OrbitObject orbitObject in orbitObj)
            {
                listboxOrbitObjects.Items.Add(new SpaceObjChildrenItem(orbitObject));
            }
        }

        private void Show(Canvas canvas)
        {
            MainWindow.ClosePrevUI(canvas);
            canvas.Children.Add(this);
        }

        private void SpaceObjChildren_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.KeyDown += SpaceObjChildren_KeyDown;

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Close()
        {
            Window window = Window.GetWindow(this);
            window.KeyDown -= SpaceObjChildren_KeyDown;

            Canvas parent = Parent as Canvas;
            parent.Children.Remove(this);
        }

        private void SpaceObjChildren_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listboxOrbitObjects.SelectedItem != null)
                {
                    SpaceObject spaceObj = (listboxOrbitObjects.SelectedItem as SpaceObjChildrenItem).spaceObj;
                    spaceObj.Delete(spaceObj.ParentCanvas);

                    listboxOrbitObjects.Items.Remove(listboxOrbitObjects.SelectedItem);
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.timerMove.Stop();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Space Object files (*.xml;*json;*bin)|*.xml;*json;*bin";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    SpaceObject spaceObj = null;
                    Type type = centralObject.GetOrbitObjectsType();

                    switch (System.IO.Path.GetExtension(dlg.FileName).ToLower())
                    {
                        case ".xml":
                            if (type == typeof(Planet))
                            {
                                spaceObj = Serializer.XmlDeserialize(dlg.FileName, new Type[] { type, typeof(PlanetWithRings)});
                            }
                            else
                            {
                                spaceObj = Serializer.XmlDeserialize(dlg.FileName, new Type[] { type });
                            }
                            break;

                        case ".json":
                            // Не определяет тип десериализуемого
                            spaceObj = Serializer.JsonDeserialize(dlg.FileName, type);
                            break;

                        case ".bin":
                            spaceObj = Serializer.BinaryDeserialize(dlg.FileName);
                            break;
                    }
                    if (spaceObj == null)
                    {
                        throw new Exception();
                    }
                    centralObject.AddOrbitObject((OrbitObject)spaceObj);
                    spaceObj.Show(((SpaceObject)centralObject).ParentCanvas);
                    ShowChildrenList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }

            mainWindow.timerMove.Start();
        }
    }
}
