package Test;

import Server.Game.Chest.Chest;
import Server.Game.GameProgress;
import Server.Message.Converter;

import java.io.IOException;

public class Ser {
    public static void main(String[] args) throws IOException {
        GameProgress g = new GameProgress(null, null);

        for(int i = 0; i < g.listChests.size(); i++) {
            String s = Converter.objectToXml(g.listChests.get(i));
            System.out.println(s);
            Chest c = (Chest)Converter.xmlToObject(s, new Chest());
            System.out.println(c.getListItem().get(0).getType());
            System.out.println(c.getListItem().get(1).getType());
            System.out.println("end");
        }
    }
}
