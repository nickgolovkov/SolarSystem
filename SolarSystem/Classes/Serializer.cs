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
using System.IO;
using System.Windows.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using SolarSystem.Classes;
using SolarSystem.Classes.UI;

namespace SolarSystem.Classes
{
    static class Serializer
    {
        public static void XmlSerialize(SpaceObject spaceObj, string path)
        {
            XmlSerializer xml = new XmlSerializer(spaceObj.GetType(), new Type[] { typeof(PlanetWithRings) });
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                xml.Serialize(stream, spaceObj);
            }
        }

        public static void XmlSerialize(List<Star> stars, string path)
        {
            XmlSerializer xml = new XmlSerializer(stars.GetType(), new Type[] { typeof(PlanetWithRings) });
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                xml.Serialize(stream, stars);
            }
        }

        public static SpaceObject XmlDeserialize(string path, IEnumerable<Type> types)
        {
            SpaceObject spaceObj = null;

            foreach (Type type in types)
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(type, new Type[] { typeof(PlanetWithRings) });
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        spaceObj = (SpaceObject)xml.Deserialize(stream);
                    }
                    break;
                }
                catch { }
            }

            SetCenters(spaceObj);

            return spaceObj;
        }
        
        public static List<Star> XmlDeserializeWorld(string path, IEnumerable<Type> types)
        {
            List<Star> stars = null;

            foreach (Type type in types)
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(type, new Type[] { typeof(PlanetWithRings) });
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        stars = (List<Star>)xml.Deserialize(stream);
                    }
                    break;
                }
                catch { }
            }

            foreach (Star star in stars)
            {
                SetCenters(star);
            }

            return stars;
        }


        public static void BinarySerialize(SpaceObject spaceObj, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, spaceObj);
            }
        }

        public static void BinarySerialize(List<Star> stars, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, stars);
            }
        }

        public static SpaceObject BinaryDeserialize(string path)
        {
            SpaceObject spaceObj;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                spaceObj = (SpaceObject)formatter.Deserialize(stream);
            }

            SetCenters(spaceObj);

            return spaceObj;
        }

        public static List<Star> BinaryDeserializeWorld(string path)
        {
            List<Star> stars;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                stars = (List<Star>)formatter.Deserialize(stream);
            }
            
            foreach (Star star in stars)
            {
                SetCenters(star);
            }

            return stars;
        }


        public static void JsonSerialize(SpaceObject spaceObj, string path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(spaceObj.GetType(), new Type[] { typeof(Planet), typeof(Satellite), typeof(PlanetWithRings), typeof(Point) });
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                jsonFormatter.WriteObject(stream, spaceObj);
            }
        }

        public static void JsonSerialize(List<Star> stars, string path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(stars.GetType(), new Type[] { typeof(Planet), typeof(Satellite), typeof(PlanetWithRings), typeof(Point) });
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                jsonFormatter.WriteObject(stream, stars);
            }
        }

        public static SpaceObject JsonDeserialize(string path, Type type)
        {
            SpaceObject spaceObj;

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(type, new Type[] { typeof(Planet), typeof(Satellite), typeof(PlanetWithRings), typeof(Point)});
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                spaceObj = (SpaceObject)jsonFormatter.ReadObject(stream);
            }
            
            SetCenters(spaceObj);

            return spaceObj;
        }

        public static List<Star> JsonDeserializeWorld(string path, Type type)
        {
            List<Star> stars;

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(type, new Type[] { typeof(Planet), typeof(Satellite), typeof(PlanetWithRings), typeof(Point) });
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                stars = (List<Star>)jsonFormatter.ReadObject(stream);
            }

            foreach (Star star in stars)
            {
                SetCenters(star);
            }

            return stars;
        }


        private static void SetCenters(SpaceObject spaceObj)
        {
            if (spaceObj is Star)
            {
                OrbitObject.SetCenters(spaceObj as Star);
            }
            if (spaceObj is Planet)
            {
                OrbitObject.SetCenters(spaceObj as Planet);
            }
        }
    }
}
