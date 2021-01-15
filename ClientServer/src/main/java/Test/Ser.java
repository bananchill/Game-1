package Test;

import MenuAndChat.Cli;

import java.sql.*;

public class Ser {
    static final String DB_URL = "jdbc:postgresql://127.0.0.1:5432/game";
    static final String USER = "postgres";
    static final String PASS = "pass";

    public static void main(String[] args) {
        System.out.println("Hello");
    }

//    public static void main(String[] args) {
//        try {
//            Class.forName("org.postgresql.Driver");
//        } catch (ClassNotFoundException e) {
//            System.out.println("PostgreSQL JDBC Driver is not found. Include it in your library path ");
//            e.printStackTrace();
//            return;
//        }
//
//        System.out.println("PostgreSQL JDBC Driver successfully connected");
//        Connection connection = null;
//
//        try {
//            connection = DriverManager.getConnection(DB_URL, USER, PASS);
//        } catch (SQLException e) {
//            System.out.println("Connection Failed");
//            e.printStackTrace();
//            return;
//        }
//
//        if (connection != null) {
//            System.out.println("You successfully connected to database now");
//        } else {
//            System.out.println("Failed to make connection to database");
//        }
//
//        Statement statement = null;
//        try {
//            statement = connection.createStatement();
//        } catch (SQLException throwables) {
//            throwables.printStackTrace();
//        }
//        try {
//            addCharacter(connection);
//            ResultSet result = statement.executeQuery("select * from character");
//
//            while (result.next()) {
//                System.out.println(result.getInt("idCharacter") + " " +
//                        result.getString("nickname"));
//            }
//        } catch (SQLException throwables) {
//            throwables.printStackTrace();
//        } finally {
//            if (connection != null) {
//                try {
//                    connection.close();
//                } catch (SQLException throwables) {
//                    throwables.printStackTrace();
//                }
//            }
//        }
//    }
//
//    public static boolean addCharacter(Connection connection) {
//        String statementSql = "INSERT INTO CHARACTER (nickname, password, email, gold, crystal, status) VALUES(?, ?, ?, ?, ?, ?);";
//
//        Cli cli = new Cli("doctor", "123", "agazman@mail.ru", 10000, 500, false);
//
//        try (PreparedStatement pstmt = connection.prepareStatement(statementSql, Statement.RETURN_GENERATED_KEYS)) {
//            pstmt.setString(1, cli.getNickname());
//            pstmt.setString(2, cli.getPassword());
//            pstmt.setString(3, cli.getEmail());
//            pstmt.setInt(4, cli.getGold());
//            pstmt.setInt(5, cli.getCrystal());
//            pstmt.setBoolean(6, cli.isOnline());
//
//            pstmt.executeUpdate();
//        } catch (SQLException ex) {
//            System.out.println(ex.getMessage());
//        }
//        return true;
//    }
}
