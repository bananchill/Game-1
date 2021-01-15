package Server;

import Server.Database.DatabaseHelper;
import Server.Game.Server.GameServer;
import Server.MainServer.MainServer;

import java.io.IOException;

public class Main {
    public static void main(String[] args) throws IOException {
        DatabaseHelper.connectDatabase();

        Server mainServer = new MainServer();
        mainServer.start(3000);

        GameServer gameServer = new GameServer();
        gameServer.start(3001);

        Thread threadCreateAPairOfPlayers = new Thread(() -> gameServer.createAPairOfPlayers());
        threadCreateAPairOfPlayers.start();
    }
}
