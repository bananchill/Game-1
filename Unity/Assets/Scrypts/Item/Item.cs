using System;
using System.Xml.Serialization;

namespace Assets.Scrypts
{
    [Serializable]
    public class Item
    {
        [XmlElement("itemType")]
        public ItemType type { get; set; }
        [XmlElement]
        public int damage { get; set; }
        [XmlElement]
        public int armor { get; set; }
        [XmlElement]
        public int health { get; set; }

        public Item()
        {
        }

        public Item(ItemType type, int damage, int armor, int health)
        {
            this.type = type;
            this.damage = damage;
            this.armor = armor;
            this.health = health;
        }

        public override string ToString()
        {
            return "Item = type:" + type + "; damage: " + damage + "; armor: " + armor + "; health " + health;
        }
    }
}