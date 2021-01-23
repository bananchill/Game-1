package Server.Interfaces;

import Server.Connection;
import Server.ConsoleHelper;
import Server.Message.Message;

public interface SendMessage {

    default void sendMessage(Connection connection, Message message) {
        try {
            connection.send(message);
        } catch (Exception e) {
            e.printStackTrace();
            ConsoleHelper.writeMessage("Сообщение до пользователя не отправлено");
        }
    }

    default void sendMessage(Connection connection, String message) {
        try {
            connection.send(message);
        } catch (Exception e) {
            e.printStackTrace();
            ConsoleHelper.writeMessage("Сообщение до пользователя не отправлено");
        }
    }
}
