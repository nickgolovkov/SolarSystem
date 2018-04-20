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
using System.IO;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using SolarSystem.Classes;
using SolarSystem.Classes.UI;

namespace SolarSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Масштабы
        // Радиус звезд, планет и спутников 1 : 1 тыс. км
        // Орбиты планет 1 : 1 млн. км
        // Орбиты спутнико 1 : 100 тыс. км

        public MainWindow()
        {
            InitializeComponent();
            InitMoveTimer();
            InitRotateTimer();
        }

        private List<Star> stars = new List<Star>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Star sun = new Star("Sun", 695, new Point(ActualWidth / 2, ActualHeight / 2), "Textures/Sun.png");

            //Planet mercury = new Planet("Mercury", 2.4, sun, sun.Radius + 58, 880, "Textures/Mercury.png");
            //Planet venus = new Planet("Venus", 6.0, sun, sun.Radius + 108.2, 2250, "Textures/Venus.png");
            //Planet earth = new Planet("Earth", 6.3, sun, sun.Radius + 149.6, 3650, "Textures/Earth.png");
            //Satellite moon = new Satellite("Moon", 1.7, earth, earth.Radius + 3.8, 270, "Textures/Moon.png");
            //Planet mars = new Planet("Mars", 3.4, sun, sun.Radius + 227.9, 6870, "Textures/Mars.png");
            //Planet jupiter = new Planet("Jupiter", 69.9, sun, sun.Radius + 778.5, 120 * 365, "Textures/Jupiter.png");
            //PlanetWithRings saturn = new PlanetWithRings("Saturn", 58.2, 130, sun, sun.Radius + 1429, 290 * 365, "Textures/Saturn.png", "Textures/SaturnRings.png");
            //Planet uranus = new Planet("Uranus", 25.3, sun, sun.Radius + 2871, 840 * 365, "Textures/Uranus.png");
            //Planet neptune = new Planet("Neptune", 24.6, sun, sun.Radius + 4498, 1650 * 365, "Textures/Neptune.png");
            //sun.Show(canvasModel);
            //stars.Add(sun);

            //Star whiteDwarf = new Star("WhiteDwarf", 160, new Point(7000, 4000), "Textures/WhiteDwarf.png");
            //whiteDwarf.Show(canvasModel);
            //stars.Add(whiteDwarf);
        }

        // Движение камеры
        public DispatcherTimer timerMove;
        private void InitMoveTimer()
        {
            const int INTERVAL = 10;

            timerMove = new DispatcherTimer();
            timerMove.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            timerMove.Tick += new EventHandler(MoveTick);
            timerMove.Start();
        }
        private void MoveTick(object sender, EventArgs e)
        {
            const double MOVE_OFFSET = 60;
            const double MOVE_COEF = 0.4;

            if (Mouse.GetPosition(this).X < MOVE_OFFSET)
            {
                Canvas.SetLeft(canvasModel, Canvas.GetLeft(canvasModel) - ((Mouse.GetPosition(this).X - MOVE_OFFSET) * MOVE_COEF));
            }

            if (Mouse.GetPosition(this).Y < MOVE_OFFSET)
            {
                Canvas.SetTop(canvasModel, Canvas.GetTop(canvasModel) - ((Mouse.GetPosition(this).Y - MOVE_OFFSET) * MOVE_COEF));
            }

            if (Mouse.GetPosition(this).X > ActualWidth - (MOVE_OFFSET + 10))
            {
                Canvas.SetLeft(canvasModel, Canvas.GetLeft(canvasModel) + (ActualWidth - Mouse.GetPosition(this).X - (MOVE_OFFSET + 10)) * MOVE_COEF);
            }

            if (Mouse.GetPosition(this).Y > ActualHeight - (MOVE_OFFSET + 10))
            {
                Canvas.SetTop(canvasModel, Canvas.GetTop(canvasModel) + (ActualHeight - Mouse.GetPosition(this).Y - (MOVE_OFFSET + 10)) * MOVE_COEF);
            }
        }

        // Вращение объектов
        private DispatcherTimer timerRotate;
        private void InitRotateTimer()
        {
            const int INTERVAL = 10;

            timerRotate = new DispatcherTimer();
            timerRotate.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            timerRotate.Tick += new EventHandler(RotateTick);
            timerRotate.Start();
        }
        private void RotateTick(object sender, EventArgs e)
        {
            foreach (Star star in stars)
            {
                foreach (Planet planet in star.planets)
                {
                    planet.Rotate();
                    foreach (Satellite satellite in planet.satellites)
                    {
                        satellite.Rotate();
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool ctrlPressed = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);

            // Save
            if (e.Key == Key.S && ctrlPressed)
            {
                SaveWorld();
            }
            
            // Open
            if (e.Key == Key.O && ctrlPressed)
            {
                foreach (Star star in stars)
                {
                    star.Delete(canvasModel);
                }
                stars = OpenWorld();
            }

            // Pause/Play
            if ((e.Key == Key.P || e.Key == Key.Space) && ctrlPressed)
            {
                if (timerRotate.IsEnabled)
                {
                    timerRotate.Stop();
                }
                else
                {
                    timerRotate.Start();
                }
            }

            // Quit
            if (e.Key == Key.Q && ctrlPressed)
            {
                Application.Current.Shutdown();
            }
        }

        private void SaveWorld()
        {
            timerMove.Stop();

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "World";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Serializer.XmlSerialize(stars, dlg.FileName + ".xml");
                Serializer.BinarySerialize(stars, dlg.FileName + ".bin");
                Serializer.JsonSerialize(stars, dlg.FileName + ".json");
            }

            timerMove.Start();
        }

        private List<Star> OpenWorld()
        {
            timerMove.Stop();

            List<Star> stars = new List<Star>();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Space Object files (*.xml;*json;*bin)|*.xml;*json;*bin";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {

                try
                {
                    switch (System.IO.Path.GetExtension(dlg.FileName).ToLower())
                    {
                        case ".xml":
                            stars = Serializer.XmlDeserializeWorld(dlg.FileName, new Type[] { typeof(List<Star>) });
                            break;

                        case ".json":
                            stars = Serializer.JsonDeserializeWorld(dlg.FileName, typeof(List<Star>));
                            break;

                        case ".bin":
                            stars = Serializer.BinaryDeserializeWorld(dlg.FileName);
                            break;
                    }
                    if (stars == null)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                    return stars;
                }

                foreach (Star star in stars)
                {
                    star.Show(canvasModel);
                }
            }

            timerMove.Start();

            return stars;
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Масштабирование
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                double scaleDelta = e.Delta / (double)1000;

                ScaleTransform st = canvasModel.RenderTransform as ScaleTransform;

                if (st != null)
                {
                    if (st.ScaleX + scaleDelta > 0)
                    {
                        st.ScaleX += scaleDelta;
                        st.ScaleY += scaleDelta;
                    }
                }
                else
                {
                    canvasModel.RenderTransform = new ScaleTransform(1 + scaleDelta, 1 + scaleDelta, ActualWidth / 2, ActualHeight / 2);
                }
            }
        }
        
        public static void ClosePrevUI(Canvas canvas)
        {
            List<IClosableUI> elements = new List<IClosableUI>(canvas.Children.OfType<IClosableUI>());
            foreach (IClosableUI el in elements)
            {
                el.Close();
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            foreach(UIElement el in canvas.Children)
            {
                if (el.IsMouseOver)
                    return;
            }

            ClosePrevUI(sender as Canvas);
        }
    }
}
