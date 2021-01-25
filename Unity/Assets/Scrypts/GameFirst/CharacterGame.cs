using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts
{
    public class CharacterGame : MonoBehaviour
    {
        public static List<Chest> listChests;
        public static GameObject obj;
        private float x;
        private float z;
        public static bool isGame = false;
        public static bool isSpawn = false;
        private GameServer gameServer;


        void Start()
        {
            obj = Resources.Load<GameObject>("ChestCartoon") as GameObject;
            listChests = new List<Chest>();
            gameServer = new GameServer();
            gameServer.StartClient("localhost", 3001);
            gameServer.ConnectToServer();
            gameServer.StartMain();
        }

        void Update()
        {
            if(isSpawn)
            {
                setChest();
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
                    if (!CheckChest(x, z))
                    {
                        Debug.Log("Nice");
                    }
                }

                if (Input.GetKey(KeyCode.S))
                {
                    x = transform.position.x;
                    z = transform.position.z - 1;
                    transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("S");
                    if (!CheckChest(x, z))
                    {
                        Debug.Log("Nice");
                    }
                }

                if (Input.GetKey(KeyCode.A))
                {
                    x = transform.position.x - 1;
                    z = transform.position.z;
                    transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("A");
                    if (!CheckChest(x, z))
                    {
                        Debug.Log("Nice");
                    }
                }

                if (Input.GetKey(KeyCode.D))
                {
                    x = transform.position.x + 1;
                    z = transform.position.z;
                    transform.position = new Vector3(x, 0, z);
                    gameServer.SendStep("D");
                    if (!CheckChest(x, z))
                    {
                        Debug.Log("Nice");
                    }
                }
            }
        }

        public static void setChest()
        {
            foreach (Chest chest in listChests)
            {
                var o = Instantiate(obj, new Vector3(chest.x, 0, chest.z), Quaternion.identity);
                o.transform.localScale = new Vector3(3f, 3f, 3f);
            }
        }

        public void setEnemy()
        {
            foreach (Chest chest in listChests)
            {
                //Instantiate(Obj, new Vector3(chest.x, 0, chest.z), Quaternion.identity);
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
    }
}