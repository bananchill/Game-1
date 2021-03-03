using System;
using System.Xml.Serialization;

namespace Assets.Scrypts
{
    [Serializable]
    public class Item
    {
        [XmlElement]
        public string name { get; set; }

        [XmlElement]
        public int damage { get; set; }

        [XmlElement]
        public int health { get; set; }

        public Item()
        {
        }

        public Item(string name, int damage, int health)
        {
            this.name = name;
            this.damage = damage;
            this.health = health;
        }

        public override string ToString()
        {
            return "Item = type:" + name + "; damage: " + damage + "; health " + health;
        }
    }
}