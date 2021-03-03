using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scrypts
{
    [Serializable]
    public class Chest
    {
        [XmlArray("listItem"), XmlArrayItem("Item")]
        public List<Item> listItem { get; set; }
        [XmlElement]
        public float x { get; set; }
        [XmlElement]
        public float z { get; set; }

        public Chest() { }

        public Chest(List<Item> listItem, float x, float z)
        {
            this.listItem = listItem;
            this.x = x;
            this.z = z;
        }
    }
}
