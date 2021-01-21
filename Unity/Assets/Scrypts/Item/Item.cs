namespace Assets.Scrypts
{
    public class Item
    {
        private ItemType type;
        private int damage;
        private int armor;
        private int health;

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

        public ItemType Type()
        {
            return type;
        }

        public int Damage()
        {
            return damage;
        }

        public int Armor()
        {
            return armor;
        }

        public int Health()
        {
            return health;
        }
    }
}