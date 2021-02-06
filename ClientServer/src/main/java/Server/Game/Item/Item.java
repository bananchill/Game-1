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

    public Item() {
    }

    public Item(ItemType type, int damage, int health) {
        this.type = type;
        this.damage = damage;
        this.health = health;
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
}
