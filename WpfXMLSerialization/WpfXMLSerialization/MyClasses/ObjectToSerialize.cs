using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfXMLSerialization.MyClasses
{
    [Serializable]
    public class ObjectToSerialize<T>
    {
        [XmlArray]
        public ObservableCollection<T> ObjectsToSerializeCollection { get; set; }
        public ObjectToSerialize()
        {
            ObjectsToSerializeCollection = new ObservableCollection<T>();
        }
    }
}
