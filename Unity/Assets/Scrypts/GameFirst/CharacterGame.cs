using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    public class CharacterGame : MonoBehaviour
    {
        public static List<Chest> listChests;
        public static List<EnemyBot> listEnemy;
        public static GameObject objCharacter, objChest, objEnemy;
        private float x;
        private float z;
        public static bool isGame = false;
        public static bool isSpawn = false;
        public static bool isTimer = false;
        public static Thread threadTimer;
        private GameServer gameServer;
        public Text timer;


        void Start()
        {
            objCharacter = GameObject.FindGameObjectWithTag("Player");
            objChest = Resources.Load<GameObject>("Chest") as GameObject;
            objEnemy = Resources.Load<GameObject>("Enemy") as GameObject;
            listChests = new List<Chest>();
            listEnemy = new List<EnemyBot>();
            gameServer = new GameServer();
            //gameServer.StartClient("176.117.134.51", 14883);
            gameServer.StartClient("localhost", 3001);
            gameServer.ConnectToServer();
            gameServer.StartMain();
        }

        void Update()
        {
            if (isSpawn)
            {
                SetChest();
                SetEnemy();
                Account.character.health = 100;
                isSpawn = false;
            }

            if (isGame)
            {
                if (!isTimer)
                {
                    threadTimer = new Thread(new ThreadStart(TimerWork));
                    threadTimer.Start();
                    isTimer = true;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    x = transform.position.x;
                    z = transform.position.z + 1;
                    objCharacter.transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("W");
                }

                if (Input.GetKey(KeyCode.S))
                {
                    x = transform.position.x;
                    z = transform.position.z - 1;
                    objCharacter.transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("S");
                }

                if (Input.GetKey(KeyCode.A))
                {
                    x = transform.position.x - 1;
                    z = transform.position.z;
                    objCharacter.transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("A");
                }

                if (Input.GetKey(KeyCode.D))
                {
                    x = transform.position.x + 1;
                    z = transform.position.z;
                    objCharacter.transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("D");
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameServer.SendStep("DOWN_E");
                }

                if (Input.GetKeyUp(KeyCode.E))
                {
                    gameServer.SendStep("UP_E");
                }
            }
        }

        private void TimerWork()
        {
            int timeRound = 0;
            while (timeRound != 60)
            {
                //timer.text = timeRound.ToString();
                timeRound++;
                Thread.Sleep(1000);
            }
            isGame = false;
            threadTimer.Abort();
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
                var o = Instantiate(objEnemy, new Vector3(enemy.x, 10, enemy.z), Quaternion.identity);
                o.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
            foreach (Chest chest in listChests)
            {
                if (chest.x == x && chest.z == z)
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