package Server.Interfaces;

import Server.Client.Client;
import Server.ConsoleHelper;
import Server.Message.Message;
import Server.Server;

public interface SendBroadcast {

    default void sendBroadcastMessage(Message message) {
        try {
            for (Client client : Server.connectionList) {
                client.getConnection().send(message);
            }
        } catch (Exception e) {
            e.printStackTrace();
            ConsoleHelper.writeMessage("Сообщение не отправлено");
        }
    }
}
