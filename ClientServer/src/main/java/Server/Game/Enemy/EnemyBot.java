package Server.Game.Enemy;

public abstract class EnemyBot {
    private EnemyType type;
    private int health;
    private int damage;
    private float x;
    private float y;

    public EnemyBot() {
    }

    public EnemyBot(EnemyType type, int health, int damage, float x, float y) {
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.x = x;
        this.y = y;
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

    public float getY() {
        return y;
    }

    public void setY(float y) {
        this.y = y;
    }

    public abstract void attack();

    public void die() {

    }
}
