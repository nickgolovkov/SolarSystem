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

namespace SolarSystem.Classes
{
    public class Planet : OrbitObject
    {
        public List<Satellite> satellites = new List<Satellite>();

        public Planet(): base() { }

        public Planet(string name, double radius, Star center, double orbit, double period, string texturePath = ""): base(name, radius, center, orbit, period, texturePath)
        {
            center.planets.Add(this);
        }

        public override void Show(Canvas canvas)
        {
            base.Show(canvas);
            foreach (Satellite satellite in satellites)
            {
                satellite.Show(canvas);
            }
        }

        public override void Delete(Canvas canvas)
        {
            List<Satellite> temp = new List<Satellite>(satellites);
            foreach (Satellite satellite in temp)
            {
                satellite.Delete(canvas);
            }
            base.Delete(canvas);
        }

        protected override void DeleteFromCenterList()
        {
            Star star = Center as Star;
            star.planets.Remove(this);
        }
    }
}
