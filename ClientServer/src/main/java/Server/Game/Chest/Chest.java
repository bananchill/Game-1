package Server.Game.Chest;

import Server.Game.Item.Item;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlElementWrapper;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

import java.util.List;

@JacksonXmlRootElement(localName = "Chest")
public class Chest {

    @JacksonXmlProperty(localName = "x")
    private float x;
    @JacksonXmlProperty(localName = "z")
    private float z;
    @JacksonXmlProperty(localName="Item")
    @JacksonXmlElementWrapper(localName = "listItem")
    private List<Item> listItem;

    private boolean isOpen = false;

    public Chest() {
    }

    public Chest(float x, float z, List<Item> listItem) {
        this.x = x;
        this.z = z;
        this.listItem = listItem;
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

    public boolean isOpen() {
        return isOpen;
    }

    public void setOpen(boolean open) {
        isOpen = open;
    }
}
