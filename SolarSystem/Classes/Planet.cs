﻿using System;
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
    class Planet: OrbitObject
    {
        public int SatellitesCount
        {
            get
            {
                return satellites.Count;
            }
        }

        public List<Satellite> satellites = new List<Satellite>();

        public Planet(string name, double radius, Star center, double orbit, string texturePath = ""): base(name, radius, center, orbit, texturePath)
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
    }
}
