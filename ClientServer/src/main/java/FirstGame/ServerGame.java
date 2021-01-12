package FirstGame;

import MenuAndChat.Connection;
import MenuAndChat.ConsoleHelper;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class ServerGame {
//    private static Map<String, Connection> connectionMap = new ConcurrentHashMap<>();
//
//    public static void main(String[] args) throws IOException {
//        try (ServerSocket serverSocket = new ServerSocket(3002)) {
//            ConsoleHelper.writeMessage("Сервер запущен");
//
//            while (true) {
//                Socket socket = serverSocket.accept();
//                Handler handler = new Handler(socket);
//                handler.start();
//            }
//        }
//    }
//
//    void createUser(NewPlayer player) {
//        player.getNickname();
//        player.getMail();
//        player.getPassword();
//    }
//
//    private static class Handler extends Thread {
//        private Socket socket;
//
//        public Handler(Socket socket) {
//            this.socket = socket;
//        }
//
//        @Override
//        public void run() {
//            ConsoleHelper.writeMessage("Установлено новое соединение с удаленным адресом " + socket.getRemoteSocketAddress());
//            String userName = null;
//            try (Connection connection = new Connection(socket)) {
//                userName = serverHandshake(connection);
//                sendListOfUsers(connection, userName);
//                serverMainLoop(connection, userName);
//            } catch (IOException | ClassNotFoundException e) {
//                ConsoleHelper.writeMessage("Ошибка при обмене данными с удаленным адресом");
//            }
//
//            if (userName != null) {
//                connectionMap.remove(userName);
//            }
//            ConsoleHelper.writeMessage("Соединение с удаленным адресом закрыто");
//        }
//
//        private String serverHandshake(Connection connection) throws IOException, ClassNotFoundException {
//            while (true) {
//                // Сформировать и отправить команду запроса имени пользователя
//                connection.send(new Message(MessageType.NAME_REQUEST));
//                // Получить ответ клиента
//                Message message = connection.receive();
//
//                // Проверить, что получена команда с именем пользователя
//                if (message.getType() == MessageType.USER_NAME) {
//                    // Достать из ответа имя, проверить, что оно не пустое
//                    if (!message.getData().isEmpty()) {
//                        // и пользователь с таким именем еще не подключен (используй connectionMap)
//                        if (connectionMap.get(message.getData()) == null) {
//                            // Добавить нового пользователя и соединение с ним в connectionMap
//                            connectionMap.put(message.getData(), connection);
//                            // Отправить клиенту команду информирующую, что его имя принято
//                            connection.send(new Message(MessageType.NAME_ACCEPTED));
//
//                            // Вернуть принятое имя в качестве возвращаемого значения
//                            return message.getData();
//                        }
//                    }
//                }
//            }
//        }
//
//        private void sendListOfUsers(Connection connection, String userName) throws IOException {
//            for (String key : connectionMap.keySet()) {
//                Message message = new Message(MessageType.USER_ADDED, key);
//
//                if (!key.equals(userName)) {
//                    connection.send(message);
//                }
//            }
//        }
//
//        private void serverMainLoop(Connection connection, String userName) throws IOException, ClassNotFoundException {
//            while (true) {
//                Message message = connection.receive();
//
//                if (message.getType() == MessageType.TEXT) {
//                    String s = userName + ": " + message.getData();
//                    Message formattedMessage = new Message(MessageType.TEXT, s);
//                    sendBroadcastMessage(formattedMessage);
//                } else {
//                    ConsoleHelper.writeMessage("Error");
//                }
//            }
//        }
//    }
}
