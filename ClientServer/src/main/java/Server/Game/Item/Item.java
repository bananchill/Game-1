package Server.Game.Item;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

public class Item {

    @JacksonXmlProperty(localName = "itemType")
    private ItemType type;
    @JacksonXmlProperty(localName = "damage")
    private int damage;
    @JacksonXmlProperty(localName = "armor")
    private int armor;
    @JacksonXmlProperty(localName = "health")
    private int health;

    public Item() {
    }

    public Item(ItemType type, int damage, int armor, int health) {
        this.type = type;
        this.damage = damage;
        this.armor = armor;
        this.health = health;
    }

    public ItemType getType() {
        return type;
    }

    public int getDamage() {
        return damage;
    }

    public int getArmor() {
        return armor;
    }

    public int getHealth() {
        return health;
    }
}
