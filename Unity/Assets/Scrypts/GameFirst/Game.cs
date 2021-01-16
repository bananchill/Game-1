using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts
{
    public class Game : MonoBehaviour
    {
        public static List<Chest> listChests;
        public GameObject obj;

        void Start()
        {
            listChests = new List<Chest>();
            float x;
            float z;
            int typeChest;

            for (int i = 0; i < 5;)
            {
                x = Random.Range(0, 1200);
                z = Random.Range(0, 520);
                typeChest = Random.Range(0, 4);
                Chest chest = new Chest((ChestType)typeChest, null, x, z);
                if (CheckChest(chest.X(), chest.Z()))
                {
                    listChests.Add(chest);
                    var o = Instantiate(obj, new Vector3(x, 0, z), Quaternion.identity);
                    o.transform.localScale = new Vector2(3f, 3f);
                    i++;
                }
            }
        }

        public static bool CheckChest(float x, float z)
        {
            foreach (Chest elem in listChests)
            {
                if (elem.X() >= x)
                {
                    if (elem.X() - x <= 30)
                    {
                        if (elem.Z() >= z)
                        {
                            if (elem.Z() - z <= 30) return false;
                        }
                        else
                        {
                            if (z - elem.Z() <= 30) return false;
                        }
                    }
                }
                else
                {
                    if (x - elem.X() <= 30)
                    {
                        if (elem.Z() >= z)
                        {
                            if (elem.Z() - z <= 30) return false;
                        }
                        else
                        {
                            if (z - elem.Z() <= 30) return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}