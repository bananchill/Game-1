package Test;

import Server.Game.Enemy.EnemyBot;
import Server.Game.Enemy.EnemyType;
import Server.Message.Converter;
import com.fasterxml.jackson.core.JsonProcessingException;

import java.util.ArrayList;
import java.util.List;

public class Ser {
    public static List<EnemyBot> listEnemy;

    public static void main(String[] args) throws JsonProcessingException {
        listEnemy = new ArrayList<>();

        EnemyBot enemy = new EnemyBot(EnemyType.ACOLYTE, rnd(10, 20), rnd(1, 3), 1, 1);
        String s = Converter.objectToXml(enemy);
        System.out.println("xml " + s);
    }

    private static void createEnemy() {
        float x, z;
        int typeEnemy;
        EnemyBot enemy = null;

        for (int i = 0; i < 10; ) {
            x = rnd(0, 3000);
            z = rnd(0, 1000);
            typeEnemy = rnd(0, 2);

            switch (typeEnemy) {
                case 0:
                    enemy = new EnemyBot(EnemyType.ACOLYTE, rnd(10, 20), rnd(1, 3), x, z);
                    break;
                case 1:
                    enemy = new EnemyBot(EnemyType.WARRIOR, rnd(20, 35), rnd(5, 10), x, z);
                    break;
                case 3:
                    enemy = new EnemyBot(EnemyType.HEADMAN, rnd(50, 70), rnd(25, 50), x, z);
                    break;
            }
            listEnemy.add(enemy);
            i++;
        }
    }

    private static String parseEnemy(EnemyBot enemy) {
        String enemyInfo = "";
        try {
            enemyInfo = Converter.objectToXml(enemy);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return enemyInfo;
    }

    public static int rnd(int min, int max) {
        max -= min;
        return (int) (Math.random() * ++max) + min;
    }
}