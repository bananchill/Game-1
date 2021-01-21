package Server.Game.Chest;

import Server.Game.Item.Item;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlElementWrapper;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

import java.util.List;

@JacksonXmlRootElement(localName = "Chest")
public class Chest {

    @JacksonXmlProperty(localName = "chestType")
    private ChestType type;
    @JacksonXmlProperty(localName="Item")
    @JacksonXmlElementWrapper(localName = "listItem")
    private List<Item> listItem;
    @JacksonXmlProperty(localName = "x")
    private float x;
    @JacksonXmlProperty(localName = "z")
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
