package Server;

import Server.Client.Client;
import Server.Database.DatabaseHelper;
import Server.Interfaces.SendMessage;
import Server.Message.Message;
import Server.Message.MessageType;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public abstract class Server implements SendMessage {
    public static List<Client> connectionList = new ArrayList<>();

    public abstract void startHandler(Socket socket);
    public abstract void serverMainLoop(Client client) throws IOException;

    public void start(int port) throws IOException {
        try (ServerSocket serverSocket = new ServerSocket(port)) {
            ConsoleHelper.writeMessage("Server with port + " + port + " started");

            Thread threadCheckClientConnection = new Thread(() -> checkClientConnection());
            threadCheckClientConnection.start();

            while (true) {
                Socket socket = null;
                try {
                    socket = serverSocket.accept();
                } catch (IOException e) {
                    e.printStackTrace();
                }

                Socket finalSocket = socket;
                Thread threadHandler = new Thread(() -> startHandler(finalSocket));
                threadHandler.start();
                ConsoleHelper.writeMessage("Add Socket");
            }
        }
    }

    public Client serverHandshake(Connection connection) throws IOException {
        while (true) {
            connection.send(new Message(MessageType.CONNECTION_REQUEST));
            Message message = connection.receive();

            if (message != null) {
                if (message.getType() == MessageType.CONNECTION_ACCEPTED) {
                    Client client = new Client(connection);
                    connectionList.add(client);
                    connection.send(new Message(MessageType.CONNECTION_ACCEPTED));
                    return client;
                }
            }
        }
    }

    public Client getAccount(Connection connection, Message message) throws JsonProcessingException {
        String[] data = message.getData().split("#");
        String mail, password;
        try {
            mail = data[0];
            password = data[1];
        } catch (Exception e) {
            sendMessage(connection, new Message(MessageType.ERROR_AUTHORIZATION));
            ConsoleHelper.writeMessage("Client wasn't founded");
            return null;
        }
        Client client = DatabaseHelper.entryCli(mail, password);
        client.setConnection(connection);

        if (client == null) {
            sendMessage(connection, new Message(MessageType.ERROR_AUTHORIZATION));
            ConsoleHelper.writeMessage("Client wasn't founded");
            return null;
        }

        String mess = client.getId() + "#" + client.getNickname() + "#" + client.getPassword() + "#" + client.getEmail() +
                "#" + client.getGold() + "#" + client.getCrystal();

        DatabaseHelper.setStatus(client.getId(), true);

        ConsoleHelper.writeMessage("Client was founded");
        sendMessage(connection, new Message(MessageType.AUTHORIZATION, mess));
        return client;
    }

    private void checkClientConnection() {
        while (true) {
            for (Client client : connectionList) {
                try {
                    client.getConnection().send(new Message(MessageType.TEST_WORK));
                    if (!client.isOnline()) {
                        Thread.sleep(30000);
                        if (!!client.isOnline()) {
                            clientRemove(client);
                        }
                    }
                } catch (JsonProcessingException | InterruptedException e) {
                    e.printStackTrace();
                }
            }
            try {
                Thread.sleep(30000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    public static void clientRemove(Client client) {
        client.close();
        connectionList.remove(client);
        DatabaseHelper.setStatus(client.getId(), false);
        ConsoleHelper.writeMessage("Client " + client.getNickname() + " disconnected");
    }
}
