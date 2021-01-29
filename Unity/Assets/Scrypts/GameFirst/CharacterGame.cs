using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts
{
    public class CharacterGame : MonoBehaviour
    {
        public static List<Chest> listChests;
        public static List<EnemyBot> listEnemy;
        public static GameObject objChest, objEnemy;
        private float x;
        private float z;
        public static bool isGame = false;
        public static bool isSpawn = false;
        private GameServer gameServer;


        void Start()
        {
            objChest = Resources.Load<GameObject>("Chest") as GameObject;
            objEnemy = Resources.Load<GameObject>("Enemy") as GameObject;
            listChests = new List<Chest>();
            listEnemy = new List<EnemyBot>();
            gameServer = new GameServer();
            gameServer.StartClient("localhost", 3001);
            gameServer.ConnectToServer();
            gameServer.StartMain();
        }

        void Update()
        {
            if(isSpawn)
            {
                SetChest();
                SetEnemy();
                isSpawn = false;
            }

            if (isGame)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    x = transform.position.x;
                    z = transform.position.z + 1;
                    transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("W");
                }

                if (Input.GetKey(KeyCode.S))
                {
                    x = transform.position.x;
                    z = transform.position.z - 1;
                    transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("S");
                }

                if (Input.GetKey(KeyCode.A))
                {
                    x = transform.position.x - 1;
                    z = transform.position.z;
                    transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("A");
                }

                if (Input.GetKey(KeyCode.D))
                {
                    x = transform.position.x + 1;
                    z = transform.position.z;
                    transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("D");
                }
            }
        }

        public static void SetChest()
        {
            foreach (Chest chest in listChests)
            {
                var o = Instantiate(objChest, new Vector3(chest.x, 0, chest.z), Quaternion.identity);
                o.transform.localScale = new Vector3(3f, 3f, 3f);
            }
        }

        public void SetEnemy()
        {
            foreach (EnemyBot enemy in listEnemy)
            {
                Instantiate(objEnemy, new Vector3(enemy.x, 0, enemy.z), Quaternion.identity);
            }
        }

        public bool CheckChest(float x, float z)
        {
            foreach (Chest elem in listChests)
            {
                if (elem.x >= x)
                {
                    if (elem.x - x <= 30)
                    {
                        if (elem.z >= z)
                        {
                            if (elem.z - z <= 30) return false;
                        }
                        else
                        {
                            if (z - elem.z <= 30) return false;
                        }
                    }
                }
                else
                {
                    if (x - elem.x <= 30)
                    {
                        if (elem.z >= z)
                        {
                            if (elem.z - z <= 30) return false;
                        }
                        else
                        {
                            if (z - elem.z <= 30) return false;
                        }
                    }
                }
            }
            return true;
        }

        public static void WaitTheGame()
        {
            Debug.Log("Loading game, please wait");
        }

        public static Chest SearchChest(float x, float z)
        {
            foreach(Chest chest in listChests)
            {
                if(chest.x == x && chest.z == z)
                {
                    return chest;
                }
            }
            return null;
        }

        public static EnemyBot SearchEnemy(float x, float z)
        {
            foreach (EnemyBot chest in listEnemy)
            {
                if (chest.x == x && chest.z == z)
                {
                    return chest;
                }
            }
            return null;
        }
    }
}