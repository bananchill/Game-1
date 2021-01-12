package MenuAndChat;

import DataHelper.DatabaseHelper;
import MenuAndChat.Message.Message;
import MenuAndChat.Message.MessageType;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class ServerMenuAndChat extends Thread {
    private static List<Cli> connectionList = new ArrayList<>();
    private static List<Cli> gamerList = new ArrayList<>();

    public static void main(String[] args) throws IOException {
        try (ServerSocket serverSocket = new ServerSocket(3000)) {
            ConsoleHelper.writeMessage("Server started");
            DatabaseHelper.connectDatabase();

            Check check = new Check();
            check.start();

            while (true) {
                Socket socket = serverSocket.accept();
                Handler handler = new Handler(socket);
                handler.start();
            }
        }
    }

    private static class Handler extends Thread {
        private Socket socket;

        public Handler(Socket socket) {
            this.socket = socket;
        }

        @Override
        public void run() {
            Cli cli;
            try (Connection connection = new Connection(socket)) {
                ConsoleHelper.writeMessage("Установлено новое соединение с удаленным адресом " + socket.getRemoteSocketAddress());
                cli = serverHandshake(connection);

                while (true) {
                    Message message = connection.receive();
                    if (message != null) {
                        if (message.getType() == MessageType.AUTHORIZATION) {
                            ConsoleHelper.writeMessage("Client want to authorization");
                            cli = getAccount(connection, message);
                            break;
                        } else if (message.getType() == MessageType.REGISTRATION) {
                            ConsoleHelper.writeMessage("Client want to registration");
                            cli = addAccount(connection, message);
                            break;
                        }
                    } else {
                        clientRemove(cli);
                        return;
                    }
                }

                sendBroadcastMessage(new Message(MessageType.USER_ADDED, cli.getNickname()));
                serverMainLoop(cli);
            } catch (IOException e) {
                ConsoleHelper.writeMessage("Ошибка при обмене данными с удаленным адресом");
            }
        }

        private Cli serverHandshake(Connection connection) throws IOException {
            while (true) {
                connection.send(new Message(MessageType.CONNECTION_REQUEST));
                Message message = connection.receive();

                if (message != null) {
                    if (message.getType() == MessageType.CONNECTION_ACCEPTED) {
                        Cli cli = new Cli(connection);
                        connectionList.add(cli);
                        connection.send(new Message(MessageType.CONNECTION_ACCEPTED));
                        return cli;
                    }
                }
            }
        }

        private void serverMainLoop(Cli cli) throws IOException {
            while (true) {
                Message message = cli.getConnection().receive();
                if (message != null) {
                    if (message.getType() == MessageType.TEXT) {
                        String s = cli.getNickname() + ": " + message.getData();
                        Message formattedMessage = new Message(MessageType.TEXT, s);
                        sendBroadcastMessage(formattedMessage);
                    } else if (message.getType() == MessageType.TEST_WORK) {
                        cli.setOnline(true);
                    } else if (message.getType() == MessageType.GAME) {
                        clientGoGame(cli);
                    } else {
                        ConsoleHelper.writeMessage("Error type " + message.getType() + " " + message.getData());
                    }
                } else {
                    clientRemove(cli);
                    return;
                }
            }
        }
    }

    private static class Check extends Thread {

        @Override
        public void run() {
            while (true) {
                for (Cli client : connectionList) {
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
    }

    public static void sendBroadcastMessage(Message message) {
        try {
            for (Cli client : connectionList) {
                client.getConnection().send(message);
            }
        } catch (Exception e) {
            e.printStackTrace();
            ConsoleHelper.writeMessage("Сообщение не отправлено");
        }
    }

    public static void sendMessage(Connection connection, Message message) {
        try {
            connection.send(message);
            ConsoleHelper.writeMessage("Message send - " + message.getData());
        } catch (Exception e) {
            e.printStackTrace();
            ConsoleHelper.writeMessage("Сообщение до пользователя - " + " не отправлено");
        }
    }

    private static Cli getAccount(Connection connection, Message message) throws JsonProcessingException {
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
        Cli cli = DatabaseHelper.entryCli(mail, password);
        cli.setConnection(connection);

        if (cli == null) {
            sendMessage(connection, new Message(MessageType.ERROR_AUTHORIZATION));
            ConsoleHelper.writeMessage("Client wasn't founded");
            return null;
        }

        String mess = cli.getId() + "#" + cli.getNickname() + "#" + cli.getPassword() + "#" + cli.getEmail() +
                "#" + cli.getGold() + "#" + cli.getCrystal();

        DatabaseHelper.setStatus(cli.getId(), true);

        ConsoleHelper.writeMessage("Client was founded");
        sendMessage(connection, new Message(MessageType.AUTHORIZATION, mess));
        return cli;
    }

    private static Cli addAccount(Connection connection, Message message) throws JsonProcessingException {
        String[] data = message.getData().split("#");

        String mail = data[0];
        String password = data[1];
        String nickname = data[2];

        try {
            DatabaseHelper.addCharacter(new Cli(nickname, password, mail, 1000, 0, true));
            ConsoleHelper.writeMessage("Client was added");
        } catch (Exception e) {
            sendMessage(connection, new Message(MessageType.ERROR_REGISTRATION));
            ConsoleHelper.writeMessage("Client wasn't added");
            return null;
        }

        Cli cli = DatabaseHelper.entryCli(mail, password);
        cli.setConnection(connection);

        String mess = cli.getId() + "#" + cli.getNickname() + "#" + cli.getPassword() + "#" + cli.getEmail() + "#" +
                cli.getGold() + "#" + cli.getCrystal();

        DatabaseHelper.setStatus(cli.getId(), true);

        sendMessage(connection, new Message(MessageType.REGISTRATION, mess));
        return cli;
    }

    private static void clientRemove(Cli client) {
        client.close();
        connectionList.remove(client);
        DatabaseHelper.setStatus(client.getId(), false);
        ConsoleHelper.writeMessage("Client " + client.getNickname() + " disconnected");
    }

    private static void clientGoGame(Cli client) {
        gamerList.add(client);
        checkGameList();
        client.close();
        connectionList.remove(client);
        ConsoleHelper.writeMessage("Client " + client.getNickname() + " goes to game");
    }

    private static void checkGameList() {
        while (true) {
            int listSize = gamerList.size();
            if (listSize > 1) {
                int idGamerInList = rnd(listSize);
                Cli firstGamer = gamerList.get(idGamerInList);
                gamerList.remove(idGamerInList);

                idGamerInList = rnd(--listSize);
                Cli secondGamer = gamerList.get(idGamerInList);
                gamerList.remove(idGamerInList);
            }
        }
    }

    public static int rnd(int max) {
        return (int) (Math.random() * ++max);
    }
}