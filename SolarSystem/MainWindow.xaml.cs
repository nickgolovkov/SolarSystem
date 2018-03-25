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

                sun.Show(cnvSpace);
            }

            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(this).X > ActualWidth - 20)
            {
                Canvas.SetLeft(cnvSpace, Canvas.GetLeft(cnvSpace) - 20);
            }
        }
    }
}
