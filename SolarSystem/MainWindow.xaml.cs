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
using System.Windows.Threading;
using SolarSystem.Classes;

namespace SolarSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Масштабы
        // Звезды 1 : 1 тыс. км
        // Планеты и спутники 1 : 1 тыс. км
        // Орбиты планет 1 : 1 млн. км
        // Орбиты спутнико 1 : 100 тыс. км 
        // Время

        public MainWindow()
        {
            InitializeComponent();
            InitMoveTimer();
        }

        private Star sun;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sun = new Star("Sun", 695, new Point(ActualWidth / 2, ActualHeight / 2));

            Planet mercury = new Planet("Mercury", 2.4, sun, sun.Radius + 58, 880);
            Planet venus = new Planet("Venus", 6.0, sun, sun.Radius + 108.2, 2250);
            Planet earth = new Planet("Earth", 6.3, sun, sun.Radius + 149.6, 3650);
            Satellite moon = new Satellite("Moon", 1.7, earth, earth.Radius + 3.8, 270);
            Planet mars = new Planet("Mars", 3.4, sun, sun.Radius + 227.9, 6870);
            Planet jupiter = new Planet("Jupiter", 69.9, sun, sun.Radius + 778.5, 120 * 365);
            PlanetWithRings saturn = new PlanetWithRings("Saturn", 58.2, 130, sun, sun.Radius + 1429, 290 * 365);
            Planet uranus = new Planet("Uranus", 25.3, sun, sun.Radius + 2871, 840 * 365);
            Planet neptune = new Planet("Neptune", 24.6, sun, sun.Radius + 4498, 1650 * 365);

            sun.Show(canvasModel);
            InitRotateTimer();
        }

        private const double MOVE_OFFSET = 60;
        private const double MOVE_COEF = 0.4;

        // Движение камеры
        private DispatcherTimer timerMove;
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
            foreach (Planet planet in sun.planets)
            {
                planet.Rotate();
                foreach (Satellite satellite in planet.satellites)
                {
                    satellite.Rotate();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Остановка вращения планет
            if (e.Key == Key.S)
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

            // Выход
            if (e.Key == Key.Q)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                double scaleDelta = e.Delta / (double)1000;

                ScaleTransform st = canvasModel.RenderTransform as ScaleTransform;

                if (st != null)
                {
                    st.ScaleX += scaleDelta;
                    st.ScaleY += scaleDelta;
                }
                else
                {
                    canvasModel.RenderTransform = new ScaleTransform(1 + scaleDelta, 1 + scaleDelta, ActualWidth / 2, ActualHeight / 2);
                }
            }
        }
    }
}
