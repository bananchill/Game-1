package Test;

import Server.Game.Chest.Chest;
import Server.Game.GameProgress;
import Server.Message.Converter;

import java.io.FileInputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class Ser {
    public static void main(String[] args) throws IOException {
        ServerSocket s = new ServerSocket(6777);
        Socket sr = s.accept();
        FileInputStream fr = new FileInputStream("D:");
        byte b[] = new byte[2002];

        Test.main();
    }
}
