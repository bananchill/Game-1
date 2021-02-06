package Server.Game.Enemy;

import Server.Client.Client;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

@JacksonXmlRootElement(localName = "EnemyBot")
public class EnemyBot {

    @JacksonXmlProperty(localName = "enemyType")
    private EnemyType type;
    @JacksonXmlProperty(localName = "health")
    private int health;
    @JacksonXmlProperty(localName = "damage")
    private int damage;
    @JacksonXmlProperty(localName = "x")
    private float x;
    @JacksonXmlProperty(localName = "z")
    private float z;
    @JsonIgnore
    private boolean isLive;

    public EnemyBot() {
    }

    public EnemyBot(EnemyType type, int health, int damage, float x, float z) {
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.x = x;
        this.z = z;
        this.isLive = true;
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

    public void attack(Client client) {
        client.setHealth(client.getHealth() - getDamage());
    }

    public void die() {
        isLive = false;
    }
}
