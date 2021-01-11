package FirstGame;

import java.sql.*;

public class DataHelper {

    public static void main(String[] args) {
        String url = "jdbc:postgresql://127.0.0.1:5432/game";
        String user = "postgres";
        String password = "doctor";
        Connection conn = null;
        try {
            Class.forName("org.postgresql.Driver");
            conn = DriverManager.getConnection(url, user, password);

            Statement statement = conn.createStatement();

            ResultSet resultSet = statement.executeQuery("select * from Character;");

            while (resultSet.next()) {
                System.out.println(resultSet.getInt("id") + " " + resultSet.getString(2));
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if(conn != null) {
                try {
                    conn.close();
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
