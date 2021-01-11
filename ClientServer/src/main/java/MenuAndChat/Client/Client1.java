package MenuAndChat.Client;

import MenuAndChat.Connection;
import MenuAndChat.ConsoleHelper;
import MenuAndChat.Message.Message;
import MenuAndChat.Message.MessageType;

import java.io.IOException;
import java.net.Socket;

public class Client1 extends Thread {
    protected Connection connection;
    private volatile boolean clientConnected = false;
    static Client1 client;

    public static void main(String[] args) {
        client = new Client1();
        client.startClient();
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

            if (message.getType() == MessageType.NAME_REQUEST) {
                connection.send(new Message(MessageType.USER_NAME, "Doctor"));
            } else if (message.getType() == MessageType.NAME_ACCEPTED) {
                notifyConnectionStatusChanged(true);
                client.start();
                return;
            } else {
                throw new IOException("Unexpected MessageType");
            }
        }
    }

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

            if (!(message.getType() == MessageType.TEXT) &&
                    !(message.getType() == MessageType.USER_ADDED)) {
                throw new IOException("Unexpected MessageType");
            }

            if (message.getType() == MessageType.TEXT)
                processIncomingMessage(message.getData());
        }
    }

    private void processIncomingMessage(String message) {
        ConsoleHelper.writeMessage(message);
    }

    /**
     * Устанавливать значение поля clientConnected класса Client в соответствии с
     * переданным параметром.
     * Оповещать (пробуждать ожидающий) основной поток класса Client
     **/
    private void notifyConnectionStatusChanged(boolean clientConnected) {
        this.clientConnected = clientConnected;
    }

    protected String getUserName() {
        ConsoleHelper.writeMessage("Введите имя пользователя: ");
        return ConsoleHelper.readString();
    }
}