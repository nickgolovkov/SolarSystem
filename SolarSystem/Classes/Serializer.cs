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

        public static SpaceObject XmlDeserialize(string path, Type type)
        {
            SpaceObject spaceObj;

            XmlSerializer xml = new XmlSerializer(type, new Type[] { typeof(PlanetWithRings) });
            using (FileStream myFileStream = new FileStream(path, FileMode.Open))
            {
                spaceObj = (Star)xml.Deserialize(myFileStream);
            }

            SetCenters(spaceObj);

            return spaceObj;
        }

        public static void BinarySerialize(SpaceObject spaceObj, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, spaceObj);
            }
        }

        public static SpaceObject BinaryDeserialize(string path)
        {
            SpaceObject spaceObj;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                spaceObj = formatter.Deserialize(stream) as SpaceObject;
            }

            SetCenters(spaceObj);

            return spaceObj;
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
