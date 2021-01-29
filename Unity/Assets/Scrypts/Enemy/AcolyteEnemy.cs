namespace Assets.Scrypts
{
    public class AcolyteEnemy : EnemyBot
    {
        public AcolyteEnemy(EnemyType type, int health, int damage, float x, float z) : base(type, health, damage, x, z) { }

        public override void attack() { }
    }
}
