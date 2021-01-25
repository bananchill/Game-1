package Server;

import Server.Message.Converter;
import Server.Message.Message;

import java.io.*;
import java.net.Socket;
import java.nio.file.Path;
import java.util.NoSuchElementException;
import java.util.Scanner;

public class Connection implements Closeable, Serializable {
    private final Scanner scanner;
    private final PrintWriter pr;

    public Connection(Socket client) throws IOException {
        scanner = new Scanner(client.getInputStream());
        pr = new PrintWriter(client.getOutputStream(), true);
    }

    public void send(Message message) {
        synchronized (pr) {
            pr.println(message.getXml());
        }
    }

    public Message receive() throws IOException {
        Message message;

        synchronized (scanner) {
            String s;
            try {
                s = scanner.nextLine();
                if (s != null) {
                    message = (Message) Converter.xmlToObject(s, new Message());
                    return message;
                } else {
                    return null;
                }
            } catch (NoSuchElementException | IllegalStateException e) {
                return null;
            }
        }
    }

    public void close() {
        scanner.close();
        pr.close();
    }
}