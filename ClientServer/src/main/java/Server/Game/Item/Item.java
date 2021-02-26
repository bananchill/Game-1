package Server.Game.Item;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

@JacksonXmlRootElement(localName = "Item")
public class Item {

    @JacksonXmlProperty(localName = "itemType")
    private ItemType type;
    @JacksonXmlProperty(localName = "damage")
    private int damage;
    @JacksonXmlProperty(localName = "health")
    private int health;

    private String name;
    private boolean canAttack;
    private boolean isPlaced;

    public boolean isAlive() {
        return health > 0;
    }

    public Item() {
    }

    public Item(ItemType type, int damage, int health) {
        this.type = type;
        this.damage = damage;
        this.health = health;
    }

    public Item(String name, int damage, int health) {
        this.name = name;
        this.damage = damage;
        this.health = health;
        canAttack = false;
        isPlaced = false;
    }

    public ItemType getType() {
        return type;
    }

    public int getDamage() {
        return damage;
    }

    public int getHealth() {
        return health;
    }

    public void changeAttackState(boolean value) {
        canAttack = value;
    }
}
