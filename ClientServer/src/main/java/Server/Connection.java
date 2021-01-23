package Server;

import Server.Message.Converter;
import Server.Message.Message;

import java.io.*;
import java.net.Socket;
import java.nio.file.Path;
import java.util.NoSuchElementException;
import java.util.Scanner;

public class Connection implements Closeable, Serializable {
    private String path = "../info.txt";
    private File fileInfo;
    private FileInputStream in;
    private OutputStream out;
    private final Scanner scanner;
    private final PrintWriter pr;

    public Connection(Socket client) throws IOException {
        scanner = new Scanner(client.getInputStream());
        pr = new PrintWriter(client.getOutputStream(), true);
        out = client.getOutputStream();
        in = new FileInputStream(path);
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

    public void writeFile(String info) {
        fileInfo = new File(path);
        if (!fileInfo.exists()) {
            try {
                fileInfo.createNewFile();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        try (FileWriter writer = new FileWriter(path)) {
            writer.write(info);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void send(String info) {
        byte b[] = new byte[9999];
        writeFile(info);
        try {
            in.read(b, 0, b.length);
            out.write(b, 0, b.length);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void close() {
        scanner.close();
        pr.close();
    }
}