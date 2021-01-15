package FirstGame;

import DataHelper.DatabaseHelper;
import MenuAndChat.Cli;
import MenuAndChat.Connection;
import MenuAndChat.ConsoleHelper;
import MenuAndChat.Message.Message;
import MenuAndChat.Message.MessageType;
import MenuAndChat.ServerMenuAndChat;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class ServerGame {
    public static List<Cli> gamerList = new ArrayList<>();

    public static void main(String[] args) throws IOException {
        try (ServerSocket serverSocket = new ServerSocket(3002)) {
            ConsoleHelper.writeMessage("Сервер запущен");

            ClientToGame client = new ClientToGame();
            client.start();

            Check check = new Check();
            check.start();

            while (true) {
                Socket socket = serverSocket.accept();
                Handler handler = new Handler(socket);
                handler.start();
            }
        }
    }

    void createUser(NewPlayer player) {
        player.getNickname();
        player.getMail();
        player.getPassword();
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
                        }
                    } else {
                        clientRemove(cli);
                        return;
                    }
                }

                serverMainLoop(cli);
            } catch (IOException e) {
                ConsoleHelper.writeMessage("Ошибка при обмене данными с удаленным адресом");
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

        public static void sendMessage(Connection connection, Message message) {
            try {
                connection.send(message);
                ConsoleHelper.writeMessage("Message send - " + message.getData());
            } catch (Exception e) {
                e.printStackTrace();
                ConsoleHelper.writeMessage("Сообщение до пользователя - " + " не отправлено");
            }
        }

        private Cli serverHandshake(Connection connection) throws IOException {
            while (true) {
                connection.send(new Message(MessageType.CONNECTION_REQUEST));
                Message message = connection.receive();

                if (message != null) {
                    if (message.getType() == MessageType.CONNECTION_ACCEPTED) {
                        Cli cli = new Cli(connection);
                        gamerList.add(cli);
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
                    if (message.getType() == MessageType.TEST_WORK) {
                        cli.setOnline(true);
                    } else if (message.getType() == MessageType.GAME) {
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
                for (Cli client : gamerList) {
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

    private static class ClientToGame extends Thread {

        @Override
        public void run() {
            while (true) {
                int listSize = gamerList.size();
                if (listSize > 1) {
                    int idGamerInList = rnd(listSize);
                    Cli firstGamer = gamerList.get(idGamerInList);
                    gamerList.remove(idGamerInList);

                    idGamerInList = rnd(--listSize);
                    Cli secondGamer = gamerList.get(idGamerInList);
                    gamerList.remove(idGamerInList);
                    ServerGame.gamerList.add(firstGamer);
                    ServerGame.gamerList.add(secondGamer);
                }
                try {
                    Thread.sleep(100);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private static void clientRemove(Cli client) {
        client.close();
        gamerList.remove(client);
        DatabaseHelper.setStatus(client.getId(), false);
        ConsoleHelper.writeMessage("Client " + client.getNickname() + " disconnected from game");
    }

    public static int rnd(int max) {
        return (int) (Math.random() * ++max);
    }
}
