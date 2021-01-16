package Server.Game.Item;

public class Item {
    private ItemType type;
    private int damage;
    private int armor;
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
