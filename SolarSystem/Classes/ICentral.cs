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
using SolarSystem.Classes.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SolarSystem.Classes
{
    public interface ICentral
    {
        List<OrbitObject> GetOrbitObjects();

        void SetCenters();

        Type GetOrbitObjectsType();

        void AddOrbitObject(OrbitObject orbitObject);

        void ShowChildren(object sender, MouseButtonEventArgs e);
    }
}
