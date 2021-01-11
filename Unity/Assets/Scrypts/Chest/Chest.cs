using System.Collections.Generic;

namespace Assets.Scrypts
{
    public class Chest
    {
        private float x;
        private float z;
        private ChestType type;
        private List<Item> listItem;

        public Chest() { }

        public Chest(float x, float z, ChestType type, List<Item> listItem)
        {
            this.x = x;
            this.z = z;
            this.type = type;
            this.listItem = listItem;
        }

        public float X()
        {
            return x;
        }

        public float Z()
        {
            return z;
        }

        public ChestType Type()
        {
            return type;
        }

        public List<Item> ListItem()
        {
            return listItem;
        }
    }
}
