using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;


namespace WpfXMLSerialization.MyClasses
{
    public class SerializationActions<T>
    {
        public ObservableCollection<T> objectsCollection;
        public SerializationActions(ObservableCollection<T> objectsCollection)
        {
            this.objectsCollection = objectsCollection;
        }
        public void SerializeObject(ObjectToSerialize<T> entity, string filePath)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObjectToSerialize<T>));
            FileStream fileStr = new FileStream(filePath, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }
        public ObservableCollection<T> DeserializeObject(string filePath)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObjectToSerialize<T>));
            FileStream file = new FileStream(filePath, FileMode.Open);
            //se pierde referinta la colectie prin reinitializarea ei cu un alt obiect
            //this.cars = (xmlser.Deserialize(file) as ObjectToSerialize).Cars;
            //din acest motiv repopulez colectia this.cars cu elementele colectiei obtinute prin deserializare
            var c = (xmlser.Deserialize(file) as ObjectToSerialize<T>).ObjectsToSerializeCollection;
            objectsCollection.Clear();
            foreach (var car in c)
            {
                objectsCollection.Add(car);
            }
            file.Dispose();
            return objectsCollection;
        }

    }
}