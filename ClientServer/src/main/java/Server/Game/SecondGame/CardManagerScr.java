package Server.Game.SecondGame;

import Server.Game.Item.Item;

public class CardManagerScr {
    public void awake() {
        CardManagerList.allCards.add(new Item("wolf", 3, 3));
        CardManagerList.allCards.add(new Item("parrot", 2, 1));
        CardManagerList.allCards.add(new Item("snake", 1, 6));
        CardManagerList.allCards.add(new Item("squirrel", 5, 2));
    }
}
