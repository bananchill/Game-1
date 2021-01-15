package Interfaces;

import MenuAndChat.Cli;
import MenuAndChat.ConsoleHelper;
import MenuAndChat.Message.Message;
import MenuAndChat.Server;

public interface SendBroadcast {
    static void sendBroadcastMessage(Message message) {
        try {
            for (Cli client : Server.connectionList) {
                client.getConnection().send(message);
            }
        } catch (Exception e) {
            e.printStackTrace();
            ConsoleHelper.writeMessage("Сообщение не отправлено");
        }
    }
}
