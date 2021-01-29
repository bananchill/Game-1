package Server.Game;

import Server.Client.Client;
import Server.Game.Chest.Chest;
import Server.Game.Chest.ChestType;
import Server.Game.Enemy.AcolyteEnemy;
import Server.Game.Enemy.EnemyBot;
import Server.Game.Enemy.EnemyType;
import Server.Game.Item.Item;
import Server.Game.Item.ItemType;
import Server.Game.Server.GameServer;
import Server.Message.Converter;
import Server.Message.Message;
import Server.Message.MessageType;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.util.ArrayList;
import java.util.List;

public class GameProgress extends Thread {
    private List<Chest> listChests;
    private List<EnemyBot> listEnemy;
    public Client firstGamer;
    public Client secondGamer;
    private GameServer gameServer;

    public GameProgress(Client firstGamer, Client secondGamer) {
        this.firstGamer = firstGamer;
        this.secondGamer = secondGamer;
    }

    @Override
    public void run() {
        gameServer = new GameServer();

        loadingGame();
        startGame();
    }

    private void startGame() {
//        while (true) {
//        }
    }

    private void loadingGame() {
        listChests = new ArrayList<>();
        listEnemy = new ArrayList<>();
        sendInfo(MessageType.LOADING_GAME, null);

        createChests();
        createEnemy();

        sendInfo(MessageType.SET_INFO, null);

        sendInfo();

        System.out.println("Start the game!");
    }

    private String parseEnemy(EnemyBot enemy) {
        String enemyInfo = "";
        try {
            enemyInfo = Converter.objectToXml(enemy);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return enemyInfo;
    }

    private String parseChests(Chest chest) {
        String chestInfo = "";
        try {
            chestInfo = Converter.objectToXml(chest);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return chestInfo;
    }

    private void createEnemy() {
        float x, z;
        int typeEnemy;
        EnemyBot enemy = null;

        for (int i = 0; i < 10; ) {
            x = rnd(0, 3000);
            z = rnd(0, 1000);
            typeEnemy = rnd(0, 3);

            if (isTheChestFar(x, z) && isTheEnemyFar(x, z)) {
                switch (typeEnemy) {
                    case 0:
                        enemy = new AcolyteEnemy(EnemyType.ACOLYTE, rnd(10, 20), rnd(1, 3), x, z);
                        break;
                    case 1:
                        listEnemy.add(new AcolyteEnemy(EnemyType.WARRIOR, rnd(20, 35), rnd(5, 10), x, z));
                        break;
                    case 3:
                        listEnemy.add(new AcolyteEnemy(EnemyType.HEADMAN, rnd(50, 70), rnd(25, 50), x, z));
                        break;
                }
                listEnemy.add(enemy);
                i++;
            }
        }
    }

    private void createChests() {
        List<Item> listItems = new ArrayList<>();
        int x, z;
        int typeChest;

        for (int i = 0; i < 5; ) {
            x = rnd(0, 3000);
            z = rnd(0, 1000);
            typeChest = rnd(0, 3);

            ChestType type = null;

            if (isTheChestFar(x, z)) {
                switch (typeChest) {
                    case 0:
                        type = ChestType.COMMON;
                        listItems = createListItem(ChestType.COMMON);
                        break;
                    case 1:
                        type = ChestType.RARE;
                        listItems = createListItem(ChestType.RARE);
                        break;
                    case 2:
                        type = ChestType.EPIC;
                        listItems = createListItem(ChestType.EPIC);
                        break;
                    case 3:
                        type = ChestType.ENCHANTED;
                        listItems = createListItem(ChestType.ENCHANTED);
                        break;
                }

                Chest chest = new Chest(type, x, z, listItems);

                listChests.add(chest);
                i++;
            }
        }
    }

    public boolean isTheEnemyFar(float x, float z) {
        for (EnemyBot elem : listEnemy) {
            if (elem != null) {
                if (elem.getX() >= x) {
                    if (elem.getX() - x <= 30) {
                        if (elem.getZ() >= z) {
                            if (elem.getZ() - z <= 30) return false;
                        } else {
                            if (z - elem.getZ() <= 30) return false;
                        }
                    }
                } else {
                    if (x - elem.getX() <= 30) {
                        if (elem.getZ() >= z) {
                            if (elem.getZ() - z <= 30) return false;
                        } else {
                            if (z - elem.getZ() <= 30) return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    private List<Item> createListItem(ChestType type) {
        List<Item> listItems = new ArrayList<>();
        int countItems = rnd(1, 3);

        for (int i = 0; i < countItems; i++) {
            int typeItem = rnd(0, 1);
            switch (typeItem) {
                case 0:
                    type = ChestType.COMMON;
                    listItems.add(new Item(ItemType.SHIELD, rnd(1, 2), rnd(1, 4), rnd(1, 5)));
                    break;
                case 1:
                    type = ChestType.RARE;
                    listItems.add(new Item(ItemType.SWORD, rnd(1, 20), rnd(1, 40), rnd(1, 1000)));
                    break;
            }
        }
        return listItems;
    }

    public boolean isTheChestFar(float x, float z) {
        for (Chest elem : listChests) {
            if (elem.getX() >= x) {
                if (elem.getX() - x <= 30) {
                    if (elem.getZ() >= z) {
                        if (elem.getZ() - z <= 30) return false;
                    } else {
                        if (z - elem.getZ() <= 30) return false;
                    }
                }
            } else {
                if (x - elem.getX() <= 30) {
                    if (elem.getZ() >= z) {
                        if (elem.getZ() - z <= 30) return false;
                    } else {
                        if (z - elem.getZ() <= 30) return false;
                    }
                }
            }
        }
        return true;
    }

    public Chest isTheChestFar1(float x, float z) {
        for (Chest elem : listChests) {
            if (elem.getX() >= x) {
                if (elem.getX() - x <= 30) {
                    if (elem.getZ() >= z) {
                        if (elem.getZ() - z <= 30) return elem;
                    } else {
                        if (z - elem.getZ() <= 30) return elem;
                    }
                }
            } else {
                if (x - elem.getX() <= 30) {
                    if (elem.getZ() >= z) {
                        if (elem.getZ() - z <= 30) return elem;
                    } else {
                        if (z - elem.getZ() <= 30) return elem;
                    }
                }
            }
        }
        return null;
    }

    public EnemyBot isTheEnemyFar1(float x, float z) {
        for (EnemyBot elem : listEnemy) {
            if (elem != null) {
                if (elem.getX() >= x) {
                    if (elem.getX() - x <= 30) {
                        if (elem.getZ() >= z) {
                            if (elem.getZ() - z <= 30) return elem;
                        } else {
                            if (z - elem.getZ() <= 30) return elem;
                        }
                    }
                } else {
                    if (x - elem.getX() <= 30) {
                        if (elem.getZ() >= z) {
                            if (elem.getZ() - z <= 30) return elem;
                        } else {
                            if (z - elem.getZ() <= 30) return elem;
                        }
                    }
                }
            }
        }
        return null;
    }

    private void sendInfo(MessageType type, String message) {
        try {
            gameServer.sendMessage(firstGamer.getConnection(), new Message(type, message));
            //sendMessage(secondGamer.getConnection(), new Message(type, message));
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
    }

    private void sendInfo() {
        for (Chest chest : listChests) {
            try {
                gameServer.sendMessage(firstGamer.getConnection(), new Message(MessageType.SET_CHEST, parseChests(chest)));
            } catch (JsonProcessingException e) {
                e.printStackTrace();
            }
            //sendMessage(secondGamer.getConnection(), message);
        }

        for (EnemyBot enemy : listEnemy) {
            try {
                gameServer.sendMessage(firstGamer.getConnection(), new Message(MessageType.SET_ENEMY, parseEnemy(enemy)));
            } catch (JsonProcessingException e) {
                e.printStackTrace();
            }
            //sendMessage(secondGamer.getConnection(), message);
        }

        try {
            gameServer.sendMessage(firstGamer.getConnection(), new Message(MessageType.SET_END, null));
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
    }

    public static int rnd(int min, int max) {
        max -= min;
        return (int) (Math.random() * ++max) + min;
    }
}
