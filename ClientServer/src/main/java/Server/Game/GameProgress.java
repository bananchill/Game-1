package Server.Game;

import Server.Client.Client;
import Server.Game.Chest.Chest;
import Server.Game.Server.GameServer;

import java.util.ArrayList;
import java.util.List;

public class GameProgress extends GameServer {
    public static List<Chest> listChests;

    public GameProgress(Client firstGamer, Client secondGamer) {
        loadingGame();
    }

    private void loadingGame() {
        listChests = new ArrayList<>();
        float x;
        float z;
        int typeChest;

//        for (int i = 0; i < 5;)
//        {
//            x = rnd(1200);
//            z = rnd(520);
//            typeChest = rnd(4);
//            ChestType type;
//            switch (typeChest) {
//                case 0: type
//                case 1:
//                case 3:
//                case 4:
//            }
//
//            Chest chest = new Chest(x, z, ChestType.COMMON, null);
//
//            if (CheckChest(chest.X(), chest.Z()))
//            {
//                listChests.Add(chest);
//                var o = Instantiate(obj, new Vector3(x, 0, z), Quaternion.identity);
//                o.transform.localScale = new Vector2(3f, 3f);
//                i++;
//            }
//        }
    }
}
