package MenuAndChat;

public class Cli {
    private int id;
    private String nickname;
    private Connection connection;
    private boolean status;
    private String password;
    private String email;
    private int gold;
    private int crystal;

    public Cli(Connection connection) {
        this.connection = connection;
        status = true;
    }

    public Cli(String nickname, String password, String email, int gold, int crystal, boolean status) {
        this.nickname = nickname;
        this.password = password;
        this.email = email;
        this.gold = gold;
        this.crystal = crystal;
        this.status = status;
    }

    public Cli(int id, String nickname, String password, String email, int gold, int crystal, boolean status) {
        this.id = id;
        this.nickname = nickname;
        this.password = password;
        this.email = email;
        this.gold = gold;
        this.crystal = crystal;
        this.status = status;
    }

    public Cli() {

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

    public void close() {
        connection.close();
    }
}
