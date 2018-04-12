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
    /// Логика взаимодействия для SpaceObjChildrenItem.xaml
    /// </summary>
    public partial class SpaceObjChildrenItem : UserControl
    {
        public SpaceObject spaceObj;

        public SpaceObjChildrenItem(OrbitObject spaceObj)
        {
            InitializeComponent();

            this.spaceObj = spaceObj;
            txtblockName.Text = spaceObj.Name;
            elSpaceObj.Fill = new ImageBrush(spaceObj.Texture);
        }
    }
}
