package MenuAndChat.Client;

import MenuAndChat.Connection;
import MenuAndChat.ConsoleHelper;
import MenuAndChat.Message.Message;
import MenuAndChat.Message.MessageType;
import MenuAndChat.ServerMenuAndChat;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.io.IOException;
import java.net.Socket;

public class Client extends Thread {
    protected static Connection connection;
    private volatile boolean clientConnected = false;
    static Client client;
    public static boolean online = false;

    public static void main(String[] args) {
        client = new Client();
        Check check = new Check();
        check.start();
        client.startClient();
    }

    private static class Check extends Thread {
        @Override
        public void run() {
            while (true) {
                try {
                    if (!online) {
                        Thread.sleep(30000);
                        if (!online) {
                            Client.serverClose();
                            return;
                        }
                    }
                    connection.send(new Message(MessageType.TEST_WORK));
                    online = false;
                } catch (JsonProcessingException | InterruptedException e) {
                }
                try {
                    Thread.sleep(30000);
                } catch (InterruptedException e) {
                }
            }
        }
    }

    public void startClient() {
        try {
            Socket socket = new Socket("127.0.0.1", 3000);
            connection = new Connection(socket);

            clientHandshake();
            clientMainLoop();
        } catch (IOException e) {
            notifyConnectionStatusChanged(false);
        }
    }

    private void clientHandshake() throws IOException {
        while (true) {
            Message message = connection.receive();

            if (message == null) {
                serverClose();
                return;
            }
            if (message.getType() == MessageType.CONNECTION_REQUEST) {
                connection.send(new Message(MessageType.CONNECTION_ACCEPTED));
            } else if (message.getType() == MessageType.CONNECTION_ACCEPTED) {
                notifyConnectionStatusChanged(true);
                client.start();
                return;
            } online = true;
        }
    }

    @Override
    public void run() {
        if (clientConnected) {
            ConsoleHelper.writeMessage("Соединение установлено.");

            while (clientConnected) {
                String message;
                message = ConsoleHelper.readString();
                if (shouldSendTextFromConsole()) {
                    sendTextMessage(message);
                } else {
                    return;
                }
            }
        } else {
            ConsoleHelper.writeMessage("Произошла ошибка во время работы клиента.");
        }
    }

    private boolean shouldSendTextFromConsole() {
        return true;
    }

    private void sendTextMessage(String text) {
        try {
            connection.send(new Message(MessageType.TEXT, text));
        } catch (Exception e) {
            ConsoleHelper.writeMessage("Ошибка отправки");
            clientConnected = false;
        }
    }

    private void clientMainLoop() throws IOException {
        while (true) {
            Message message = connection.receive();

            if (message != null) {
                if (message.getType() == MessageType.TEXT)
                    processIncomingMessage(message.getData());
                online = true;
            } else {
                serverClose();
                return;
            }
        }
    }

    private static void serverClose() {
        connection.close();
        ConsoleHelper.writeMessage("Error happened, server disconnected");
    }

    private void processIncomingMessage(String message) {
        ConsoleHelper.writeMessage(message);
    }

    private void notifyConnectionStatusChanged(boolean clientConnected) {
        this.clientConnected = clientConnected;
    }
}