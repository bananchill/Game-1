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
    public static List<Client> gamerList = new ArrayList<>();
    public static List<Client> potentialPlayersList = new ArrayList<>();

    @Override
    public void startHandler(Socket socket) {
        Client client;
        try (Connection connection = new Connection(socket)) {
            ConsoleHelper.writeMessage("Установлено новое соединение с удаленным адресом " + socket.getRemoteSocketAddress());
            client = serverHandshake(connection);

            while (true) {
                Message message = connection.receive();
                if (message != null) {
                    if (message.getType() == MessageType.AUTHORIZATION) {
                        ConsoleHelper.writeMessage("Client want to authorization");
                        if (checkAccount(connection, message)) {
                            client.setConnection(connection);
                        }
                        break;
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
                return;
            }
        }
    }

    public void createAPairOfPlayers() {
        while (true) {
            int listSize = gamerList.size();
            if (listSize > 1) {
                int idGamerInList = rnd(listSize);
                Client firstGamer = gamerList.get(idGamerInList);
                gamerList.remove(idGamerInList);

                idGamerInList = rnd(--listSize);
                Client secondGamer = gamerList.get(idGamerInList);

                gamerList.remove(idGamerInList);

                GameServer.gamerList.add(firstGamer);
                GameServer.gamerList.add(secondGamer);

                GameProgress gameProgress = new GameProgress(firstGamer, secondGamer);
            }
            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    public static int rnd(int max) {
        return (int) (Math.random() * ++max);
    }

    public static int rnd(int min, int max) {
        max -= min;
        return (int) (Math.random() * ++max) + min;
    }
}
