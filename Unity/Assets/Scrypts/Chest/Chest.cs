using System.Collections.Generic;

namespace Assets.Scrypts
{
    public class Chest
    {
        private ChestType type;
        private List<Item> listItem;
        private float x;
        private float z;

        public Chest() { }

        public Chest(ChestType type, List<Item> listItem, float x, float z)
        {
            this.type = type;
            this.listItem = listItem;
            this.x = x;
            this.z = z;
        }

        public ChestType Type()
        {
            return type;
        }

        public List<Item> ListItem()
        {
            return listItem;
        }

        public float X()
        {
            return x;
        }

        public float Z()
        {
            return z;
        }
    }
}
