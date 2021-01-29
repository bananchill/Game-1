package Server.Client;

import Server.Connection;
import Server.Game.Item.Item;

import java.util.List;

public class Client {
    private int id;
    private String nickname;
    private Connection connection;
    private boolean status;
    private String password;
    private String email;
    private int gold;
    private int crystal;

    private List<Item> listItem;

    private float x;
    private float z;

    public Client() {
    }

    public Client(Connection connection) {
        this.connection = connection;
        status = true;
    }

    public Client(String nickname, String password, String email, int gold, int crystal, boolean status) {
        this.nickname = nickname;
        this.password = password;
        this.email = email;
        this.gold = gold;
        this.crystal = crystal;
        this.status = status;
    }

    public Client(int id, String nickname, String password, String email, int gold, int crystal, boolean status) {
        this.id = id;
        this.nickname = nickname;
        this.password = password;
        this.email = email;
        this.gold = gold;
        this.crystal = crystal;
        this.status = status;
    }

    public int getId() {
        return id;
    }

    public boolean isStatus() {
        return status;
    }

    public String getNickname() {
        return nickname;
    }

    public Connection getConnection() {
        return connection;
    }

    public void setConnection(Connection connection) { this.connection = connection; }

    public boolean isOnline() {
        return status;
    }

    public void setOnline(boolean online) {
        this.status = online;
    }

    public String getPassword() {
        return password;
    }

    public String getEmail() {
        return email;
    }

    public int getGold() {
        return gold;
    }

    public int getCrystal() {
        return crystal;
    }

    public List<Item> getListItem() {
        return listItem;
    }

    public void setListItem(List<Item> listItem) {
        this.listItem = listItem;
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

    public void close() {
        connection.close();
    }
}
