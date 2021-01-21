package Server.Game.Enemy;

public abstract class EnemyBot {
    private EnemyType type;
    private int health;
    private int damage;
    private float x;
    private float z;

    public EnemyBot() {
    }

    public EnemyBot(EnemyType type, int health, int damage, float x, float z) {
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.x = x;
        this.z = z;
    }

    public EnemyType getType() {
        return type;
    }

    public int getHealth() {
        return health;
    }

    public int getDamage() {
        return damage;
    }

    public float getX() {
        return x;
    }

    public void setX(float x) {
        this.x = x;
    }

    public float getZ() {
        return z;
    }

    public void setZ(float z) {
        this.z = z;
    }

    public abstract void attack();

    public void die() {

    }
}
