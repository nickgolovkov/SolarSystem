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

        public MainWindow()
        {
            InitializeComponent();
            InitTimer();
        }

        private const double MOVE_OFFSET = 60;
        private const double MOVE_COEF = 0.4;
        private DispatcherTimer timerMove;

        // Движение камеры
        private void InitTimer()
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Star sun = new Star("Sun", 695, new Point(ActualWidth / 2, ActualHeight / 2));

                Planet mercury = new Planet("Mercury", 2.4, sun, 58);
                Planet venus = new Planet("Venus", 6.0, sun, 108.2);
                Planet earth = new Planet("Earth", 6.3, sun, 149.6);
                Satellite moon = new Satellite("Moon", 1.7, earth, 3.8);
                Planet mars = new Planet("Mars", 3.4, sun, 227.9);
                Planet jupiter = new Planet("Jupiter", 69.9, sun, 778.5);
                PlanetWithRings saturn = new PlanetWithRings("Saturn", 58.2, 130, sun, 1429);
                Planet uranus = new Planet("Uranus", 25.3, sun, 2871);
                Planet neptune = new Planet("Neptune", 24.6, sun, 4498);

                sun.Show(canvasModel);
            }

            if (e.Key == Key.Escape)
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
