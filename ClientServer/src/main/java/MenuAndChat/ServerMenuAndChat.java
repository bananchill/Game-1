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

public class ServerMenuAndChat extends Thread {
    private static List<Cli> connectionList = new ArrayList<>();
    static DatabaseHelper databaseHelper = new DatabaseHelper();

    public static void main(String[] args) throws IOException {
        try (ServerSocket serverSocket = new ServerSocket(3000)) {
            ConsoleHelper.writeMessage("Сервер запущен");
            databaseHelper.connectDatabase();

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
            String userName;
            Connection connection = null;
            Cli cli;
            try {
                connection = new Connection(socket);
            } catch (IOException e) {
                e.printStackTrace();
            }
            ConsoleHelper.writeMessage("Установлено новое соединение с удаленным адресом " + socket.getRemoteSocketAddress());
            try {
                cli = serverHandshake(connection);
                userName = cli.getNickname();
                sendBroadcastMessage(new Message(MessageType.USER_ADDED, userName));
                serverMainLoop(connection, cli);
            } catch (IOException e) {
                ConsoleHelper.writeMessage("Ошибка при обмене данными с удаленным адресом");
            }
        }

        private Cli serverHandshake(Connection connection) throws IOException {
            while (true) {
                connection.send(new Message(MessageType.NAME_REQUEST));
                Message message = connection.receive();

                if (message != null) {
                    if (message.getType() == MessageType.USER_NAME) {
                        if (!message.getData().isEmpty()) {
                            Cli cli = new Cli(message.getData(), connection);
                            connectionList.add(cli);
                            connection.send(new Message(MessageType.NAME_ACCEPTED));
                            return cli;
                        }
                    } else if (message.getType() == MessageType.TEST_WORK) {
                    }
                }
            }
        }

        private void serverMainLoop(Connection connection, Cli cli) throws IOException {
            while (true) {
                Message message = connection.receive();
                if (message != null) {
                    if (message.getType() == MessageType.TEXT) {
                        String s = cli.getNickname() + ": " + message.getData();
                        Message formattedMessage = new Message(MessageType.TEXT, s);
                        sendBroadcastMessage(formattedMessage);
                    } else if (message.getType() == MessageType.TEST_WORK) {
                        cli.setOnline(true);
                    } else if (message.getType() == MessageType.AUTHORIZATION) {
                        ConsoleHelper.writeMessage("Client want to authorization");
                        getAccount(connection, message);
                    } else if (message.getType() == MessageType.REGISTRATION) {
                        ConsoleHelper.writeMessage("Client want to registration");
                        addAccount(connection, message);
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

    private static void clientRemove(Cli client) {
        client.close();
        connectionList.remove(client);
        ConsoleHelper.writeMessage("Error happened, client " + client.getNickname() + " disconnected");
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

    private static void getAccount(Connection connection, Message message) throws JsonProcessingException {
        String[] data = message.getData().split("#");
        String mail = null, password = null;
        try {
            mail = data[0];
            password = data[1];
        } catch (Exception e) {
            sendMessage(connection, new Message(MessageType.ERROR_AUTHORIZATION));
            ConsoleHelper.writeMessage("Client wasn't founded");
            return;
        }
        Cli cli = databaseHelper.entryCli(mail, password);
        if (cli == null) {
            sendMessage(connection, new Message(MessageType.ERROR_AUTHORIZATION));
            ConsoleHelper.writeMessage("Client wasn't founded");
            return;
        }

        String mess = cli.getId() + "#" + cli.getNickname() + "#" + cli.getPassword() + "#" + cli.getEmail() + "#" +
                cli.getGold() + "#" + cli.getCrystal();

        ConsoleHelper.writeMessage("Client was founded");
        sendMessage(connection, new Message(MessageType.AUTHORIZATION, mess));
    }

    private static void addAccount(Connection connection, Message message) throws JsonProcessingException {
        String[] data = message.getData().split("#");

        String mail = data[0];
        String password = data[1];
        String nickname = data[2];

        try {
            databaseHelper.addCharacter(new Cli(nickname, password, mail, 1000, 0, true));
            ConsoleHelper.writeMessage("Client was added");
        } catch (Exception e) {
            sendMessage(connection, new Message(MessageType.ERROR_REGISTRATION));
            ConsoleHelper.writeMessage("Client wasn't added");
            return;
        }

        Cli cli = databaseHelper.entryCli(mail, password);

        String mess = cli.getId() + "#" + cli.getNickname() + "#" + cli.getPassword() + "#" + cli.getEmail() + "#" +
                cli.getGold() + "#" + cli.getCrystal();

        sendMessage(connection, new Message(MessageType.REGISTRATION, mess));
    }

    private void close() {
        databaseHelper.close();
    }
}