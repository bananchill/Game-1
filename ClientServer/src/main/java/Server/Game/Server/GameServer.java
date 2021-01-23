package Server.Game.Server;

import Server.Client.Client;
import Server.Connection;
import Server.ConsoleHelper;
import Server.Game.GameProgress;
import Server.Message.Message;
import Server.Message.MessageType;
import Server.Server;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.io.IOException;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class GameServer extends Server {
    public static ArrayList<Client> gamerList = new ArrayList<>();
    public static ArrayList<Client> potentialPlayersList = new ArrayList<>();

    @Override
    public void startHandler(Socket socket) {
        Client client;
        try (Connection connection = new Connection(socket)) {
            ConsoleHelper.writeMessage("Установлено новое соединение с удаленным адресом " + socket.getRemoteSocketAddress() + " на игру");
            client = serverHandshake(connection);

            while (true) {
                Message message = connection.receive();
                if (message != null) {
                    if (message.getType() == MessageType.AUTHORIZATION) {
                        ConsoleHelper.writeMessage("Client want to authorization in the game");
                        if (checkAccount(connection, message)) {
                            sendMessage(client.getConnection(), new Message(MessageType.AUTHORIZATION));
                            break;
                        }
                        else {
                            clientRemove(client);
                            return;
                        }
                    }
                } else {
                    clientRemove(client);
                    return;
                }
            }

            serverMainLoop(client);
        } catch (IOException e) {
            ConsoleHelper.writeMessage("Ошибка при обмене данными с удаленным адресом");
        }
    }

    private boolean checkAccount(Connection connection, Message message) throws JsonProcessingException {
        for (Client client : potentialPlayersList) {
            String[] data = message.getData().split("#");
            String mail, password;

            try {
                mail = data[0];
                password = data[1];
            } catch (Exception e) {
                sendMessage(connection, new Message(MessageType.ERROR_AUTHORIZATION));
                ConsoleHelper.writeMessage("Client wasn't founded");
                return false;
            }

            if (client.getEmail().equals(mail) && client.getPassword().equals(password)) {
                potentialPlayersList.remove(client);
                client.setConnection(connection);
                gamerList.add(client);
                return true;
            }
        }
        return false;
    }

    @Override
    public void serverMainLoop(Client client) throws IOException {
        while (true) {
            Message message = client.getConnection().receive();
            if (message != null) {
                if (message.getType() == MessageType.TEST_WORK) {
                    client.setOnline(true);
                } else {
                    ConsoleHelper.writeMessage("Error type " + message.getType() + " " + message.getData());
                }
            } else {
                clientRemove(client);
                System.out.println("main in game close");
                return;
            }
        }
    }

    public void createAPairOfPlayers() {
        while (true) {
            int listSize = gamerList.size();
            if (listSize > 0) {
                int idGamerInList = rnd(0, listSize - 1);
                Client firstGamer = gamerList.get(idGamerInList);
                gamerList.remove(firstGamer);

//                idGamerInList = rnd(0, listSize - 2);
//                Client secondGamer = gamerList.get(idGamerInList);
//                gamerList.remove(secondGamer);
//
//                new GameProgress(firstGamer, secondGamer);
                new GameProgress(firstGamer, null);
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    public static int rnd(int min, int max) {
        max -= min;
        return (int) (Math.random() * ++max) + min;
    }
}
