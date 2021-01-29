namespace Assets.Scrypts
{
    public abstract class EnemyBot
    {
        public EnemyType type { get; set; }
        public int health { get; set; }
        public int damage { get; set; }
        public float x { get; set; }
        public float z { get; set; }

        public EnemyBot()
        {
        }

        public EnemyBot(EnemyType type, int health, int damage, float x, float z)
        {
            this.type = type;
            this.health = health;
            this.damage = damage;
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return "Enemy = type:" + type + "; damage: " + damage + "; health " + health;
        }

        public abstract void attack();

        public void die()
        {

        }
    }
}