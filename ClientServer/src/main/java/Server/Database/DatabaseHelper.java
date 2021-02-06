package Server.Database;

import Server.Client.Client;

import java.sql.*;

public class DatabaseHelper {
    private static final String DB_URL = "jdbc:postgresql://127.0.0.1:5432/game";
    private static final String USER = "postgres";
    private static final String PASS = "password";
    private static Connection connection;
    private static Statement statement = null;

//    public static void main(String[] args) {
//        connectDatabase();
//        addCharacter(new Cli("nick1", "passsssssssssss", "pochta", 1111, 2222, true));
//        Cli cli = entryCli("pocshta", "passsssssssssss");
//        System.out.println(cli.getNickname() + cli.getPassword());
//        close();
//    }

    public static void connectDatabase() {
        try {
            Class.forName("org.postgresql.Driver");
            System.out.println("PostgreSQL JDBC Driver successfully connected");
        } catch (ClassNotFoundException e) {
            System.out.println("PostgreSQL JDBC Driver is not found. Include it in your library path ");
            e.printStackTrace();
            return;
        }

        try {
            connection = DriverManager.getConnection(DB_URL, USER, PASS);

            if (connection != null) {
                System.out.println("You successfully connected to database now");
            } else {
                System.out.println("Failed to make connection to database");
            }
        } catch (SQLException e) {
            System.out.println("Connection Failed");
            return;
        }

        try {
            statement = connection.createStatement();
        } catch (SQLException e) {
            System.out.println("Ошибка объявления sql запроса");
        }
    }

    public static boolean addCharacter(Client client) {
        String sql = "INSERT INTO CHARACTER (nickname, password, email, gold, crystal, status) VALUES(?, ?, ?, ?, ?, ?);";

        try (PreparedStatement pstmt = connection.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            pstmt.setString(1, client.getNickname());
            pstmt.setString(2, client.getPassword());
            pstmt.setString(3, client.getEmail());
            pstmt.setInt(4, client.getGold());
            pstmt.setBoolean(5, client.isOnline());

            pstmt.executeUpdate();
        } catch (SQLException ex) {
            System.out.println("Ошибка добавления нового пользователя");
        }
        return true;
    }

    public static Client getCli(int id) {
        Client client = null;
        try {
            ResultSet result = statement.executeQuery("select * from character where idCharacter = " + id + ";");

            while (result.next()) {
                client = new Client(result.getInt("idCharacter"), result.getString("nickname"), result.getString("password"),
                        result.getString("email"), result.getInt("gold"), result.getBoolean("status"));
            }
        } catch (SQLException e) {
            System.out.println("Ошибка sql запроса");
        }
        return client;
    }

    public static Client entryCli(String email, String password) {
        Client client = null;
        try {
            ResultSet result = statement.executeQuery("select * from character where email = '" + email + "'" +
                    "and password = '" + password + "';");

            while (result.next()) {
                client = new Client(result.getInt("idCharacter"), result.getString("nickname"), result.getString("password"),
                        result.getString("email"), result.getInt("gold"), result.getBoolean("status"));
            }

            if(client == null) {
                return null;
            }
        } catch (SQLException e) {
            System.out.println("Ошибка sql запроса");
        } catch (NullPointerException e) {
            System.out.println("Client not founded");
            return null;
        }
        return client;
    }

    public static void setStatus(int id, boolean status) {
        String sql = "UPDATE character SET status = ? WHERE idCharacter = ?";

        try (PreparedStatement pstmt = connection.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            pstmt.setBoolean(1, status);
            pstmt.setInt(2, id);

            pstmt.executeUpdate();
        } catch (SQLException ex) {
            System.out.println("Ошибка смены статуса у пользователя");
        }
    }

    public static void close() {
        if (connection != null) {
            try {
                connection.close();
                System.out.println("Connection closed");
            } catch (SQLException e) {
                System.out.println("Ошибка закрытия соединения с базой данных");
            }
        }
    }
}
