using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scrypts
{
    [Serializable]
    public class Chest
    {
        [XmlElement("chestType")]
        public ChestType type { get; set; }
        [XmlArray("listItem"), XmlArrayItem("Item")]
        public List<Item> listItem { get; set; }
        [XmlElement]
        public float x { get; set; }
        [XmlElement]
        public float z { get; set; }

        public Chest() { }

        public Chest(ChestType type, List<Item> listItem, float x, float z)
        {
            this.type = type;
            this.listItem = listItem;
            this.x = x;
            this.z = z;
        }
    }
}
