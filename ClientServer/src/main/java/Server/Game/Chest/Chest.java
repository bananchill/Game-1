package Server.Game.Chest;

import Server.Game.Item.Item;

import java.util.List;

public class Chest {
    private ChestType type;
    private List<Item> listItem;
    private float x;
    private float z;

    public Chest() {
    }

    public Chest(ChestType type, List<Item> listItem, float x, float z) {
        this.type = type;
        this.listItem = listItem;
        this.x = x;
        this.z = z;
    }

    public ChestType getType() {
        return type;
    }

    public List<Item> getListItem() {
        return listItem;
    }

    public float getX() {
        return x;
    }

    public float getZ() {
        return z;
    }
}
