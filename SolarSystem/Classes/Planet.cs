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
using System.Runtime.Serialization;
using System.Security.Permissions;
using SolarSystem.Classes.UI;

namespace SolarSystem.Classes
{
    [Serializable]
    public class Planet : OrbitObject, ICentral
    {
        public List<Satellite> satellites = new List<Satellite>();

        public Planet(): base()
        {
            spaceObject.MouseLeftButtonDown += ShowChildren;
        }

        protected Planet(SerializationInfo info, StreamingContext context): base(info, context)
        {
            for (int i = 0; i < info.GetInt32("SatellitesCount"); i++)
            {
                satellites.Add(info.GetValue("Satellite" + i.ToString(), typeof(Satellite)) as Satellite);
            }

            spaceObject.MouseLeftButtonDown += ShowChildren;
        }

        public Planet(string name, double radius, Star center, double orbit, double period, string texturePath): base(name, radius, center, orbit, period, texturePath)
        {
            center.planets.Add(this);

            spaceObject.MouseLeftButtonDown += ShowChildren;
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SatellitesCount", satellites.Count);
            for (int i = 0; i < satellites.Count; i++)
            {
                info.AddValue("Satellite" + i.ToString(), satellites[i]);
            }
        }

        public List<OrbitObject> GetOrbitObjects()
        {
            return new List<OrbitObject>(satellites);
        }

        public void SetCenters()
        {
            foreach (Satellite satellite in satellites)
            {
                satellite.Center = this;
            }
        }

        public Type GetOrbitObjectsType()
        {
            return satellites.GetType().GetGenericArguments().Single();
        }

        public void AddOrbitObject(OrbitObject orbitObject)
        {
            satellites.Add((Satellite)orbitObject);
            orbitObject.Center = this;
        }
        
        public void ShowChildren(object sender, MouseButtonEventArgs e)
        {
            Canvas parent = ParentCanvas.Parent as Canvas;
            SpaceObjChildren spaceObjChildren = new UI.SpaceObjChildren(this, Mouse.GetPosition(parent), parent);
        }
    }
}
