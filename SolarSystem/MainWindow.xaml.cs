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
        public MainWindow()
        {
            InitializeComponent();
            InitTimers();
        }

        private const double MOVE_OFFSET = 60;
        private const double MOVE_COEF = 4;
        private DispatcherTimer leftMove, topMove, rightMove, bottomMove;

        // Движение камеры
        private void InitTimers()
        {
            const int INTERVAL = 10;

            leftMove = new DispatcherTimer();
            leftMove.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            leftMove.Tick += new EventHandler(LeftMoveTick);

            topMove = new DispatcherTimer();
            topMove.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            topMove.Tick += new EventHandler(TopMoveTick);

            rightMove = new DispatcherTimer();
            rightMove.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            rightMove.Tick += new EventHandler(RightMoveTick);

            bottomMove = new DispatcherTimer();
            bottomMove.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
            bottomMove.Tick += new EventHandler(BottomMoveTick);
        }
        private void LeftMoveTick(object sender, EventArgs e)
        {
            Canvas.SetLeft(canvasModel, Canvas.GetLeft(canvasModel) - ((Mouse.GetPosition(this).X - MOVE_OFFSET) / MOVE_COEF));
        }
        private void TopMoveTick(object sender, EventArgs e)
        {
            Canvas.SetTop(canvasModel, Canvas.GetTop(canvasModel) - ((Mouse.GetPosition(this).Y - MOVE_OFFSET) / MOVE_COEF));
        }
        private void RightMoveTick(object sender, EventArgs e)
        {
            Canvas.SetLeft(canvasModel, Canvas.GetLeft(canvasModel) + (ActualWidth - Mouse.GetPosition(this).X - (MOVE_OFFSET + 10)) / MOVE_COEF);
        }
        private void BottomMoveTick(object sender, EventArgs e)
        {
            Canvas.SetTop(canvasModel, Canvas.GetTop(canvasModel) + (ActualHeight - Mouse.GetPosition(this).Y - (MOVE_OFFSET + 10)) / MOVE_COEF);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Star sun = new Star("Sun", 140, new Point(0, ActualHeight / 2));

                Planet mercury = new Planet("Mercury", 24, sun, 580);
                Planet venus = new Planet("Venus", 60, sun, 1080);
                Planet earth = new Planet("Earth", 63, sun, 1496);
                Satellite moon = new Satellite("Moon", 5, earth, 384);

                sun.Show(canvasModel);
            }

            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(this).X < MOVE_OFFSET)
            {
                leftMove.Start();
            }
            else
            {
                leftMove.Stop();
            }

            if (e.GetPosition(this).Y < MOVE_OFFSET)
            {
                topMove.Start();
            }
            else
            {
                topMove.Stop();
            }

            if (e.GetPosition(this).X > ActualWidth - (MOVE_OFFSET + 10))
            {
                rightMove.Start();
            }
            else
            {
                rightMove.Stop();
            }

            if (e.GetPosition(this).Y > ActualHeight - (MOVE_OFFSET + 10))
            {
                bottomMove.Start();
            }
            else
            {
                bottomMove.Stop();
            }
        }
    }
}
