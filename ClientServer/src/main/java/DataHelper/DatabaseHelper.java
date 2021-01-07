package DataHelper;

import MenuAndChat.Cli;

import java.sql.*;

public class DatabaseHelper {
    static final String DB_URL = "jdbc:postgresql://127.0.0.1:5432/game";
    static final String USER = "postgres";
    static final String PASS = "pass";
    static Connection connection;
    static Statement statement = null;

    public static void main(String[] args) {
        connectDatabase();
        //addCharacter(new Cli("nick1", "passsssssssssss", "pochta", 1111, 2222, true));
        Cli cli = entryCli("pocshta", "passsssssssssss");
        System.out.println(cli.getNickname() + cli.getPassword());
        close();
    }

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

    public static boolean addCharacter(Cli cli) {
        String sql = "INSERT INTO CHARACTER (nickname, password, email, gold, crystal, status) VALUES(?, ?, ?, ?, ?, ?);";

        try (PreparedStatement pstmt = connection.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            pstmt.setString(1, cli.getNickname());
            pstmt.setString(2, cli.getPassword());
            pstmt.setString(3, cli.getEmail());
            pstmt.setInt(4, cli.getGold());
            pstmt.setInt(5, cli.getCrystal());
            pstmt.setBoolean(6, cli.isOnline());

            pstmt.executeUpdate();
        } catch (SQLException ex) {
            System.out.println("Ошибка добавления нового пользователя");
        }
        return true;
    }

    public static Cli getCli(int id) {
        Cli cli = null;
        try {
            ResultSet result = statement.executeQuery("select * from character where idCharacter = " + id + ";");

            while (result.next()) {
                cli = new Cli(result.getInt("idCharacter"), result.getString("nickname"), result.getString("password"),
                        result.getString("email"), result.getInt("gold"),
                        result.getInt("crystal"), result.getBoolean("status"));
            }
        } catch (SQLException e) {
            System.out.println("Ошибка sql запроса");
        }
        return cli;
    }

    public static Cli entryCli(String email, String password) {
        Cli cli = null;
        try {
            ResultSet result = statement.executeQuery("select * from character where email = '" + email + "'" +
                    "and password = '" + password + "';");

            while (result.next()) {
                cli = new Cli(result.getInt("idCharacter"), result.getString("nickname"), result.getString("password"),
                        result.getString("email"), result.getInt("gold"),
                        result.getInt("crystal"), result.getBoolean("status"));
            }
        } catch (SQLException e) {
            System.out.println("Ошибка sql запроса");
        }
        return cli;
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
