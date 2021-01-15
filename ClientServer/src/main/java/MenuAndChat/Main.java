package MenuAndChat;

import DataHelper.DatabaseHelper;

import java.io.IOException;

public class Main {
    public static void main(String[] args) throws IOException {
        DatabaseHelper.connectDatabase();
        ServerMenuAndChat s = new ServerMenuAndChat();
        s.start(3000);
    }
}
