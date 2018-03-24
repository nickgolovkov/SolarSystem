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
                double y = ActualHeight / 2;

                Star sun = new Star("Sun", 200, new Point(300, y));
                Planet mercury = new Planet("Mercury", 20, sun, 400);
                Planet venus = new Planet("Venus", 30, sun, 600);
                Planet earth = new Planet("Earth", 35, sun, 900);
                
                sun.Show(canvas);
            }

            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
