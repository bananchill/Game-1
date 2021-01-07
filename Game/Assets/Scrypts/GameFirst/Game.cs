using Assets.Scrypts;
using System.Collections.Generic;
using UnityEngine;

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
            typeChest = Random.Range(1, 5);
            Chest chest = new Chest(x, z, (ChestType)typeChest, null);
            if (checkChest(chest))
            {
                listChests.Add(chest);
                var o = Instantiate(obj, new Vector3(x, 0, z), Quaternion.identity);
                o.transform.localScale = new Vector2(3f, 3f);
                i++;
            }
        }
    }

    public static bool checkChest(Chest chest)
    {
        foreach (Chest elem in listChests)
        {
            if (elem.X() >= chest.X())
            {
                if (elem.X() - chest.X() <= 30)
                {
                    if (elem.Z() >= chest.Z())
                    {
                        if (elem.Z() - chest.Z() <= 30) return false;
                    }
                    else
                    {
                        if (chest.Z() - elem.Z() <= 30) return false;
                    }
                }
            }
            else
            {
                if (chest.X() - elem.X() <= 30)
                {
                    if (elem.Z() >= chest.Z())
                    {
                        if (elem.Z() - chest.Z() <= 30) return false;
                    }
                    else
                    {
                        if (chest.Z() - elem.Z() <= 30) return false;
                    }
                }
            }
        }
        return true;
    }

    public static bool checkChest(float x, float z)
    {
        foreach (Chest elem in listChests)
        {
            if (elem.X() >= x)
            {
                if (elem.X() - x <= 30)
                {
                    if (elem.Z() >= z)
                    {
                        if (elem.Z() - z <= 30) return true;
                    }
                    else
                    {
                        if (z - elem.Z() <= 30) return true;
                    }
                }
            }
            else
            {
                if (x - elem.X() <= 30)
                {
                    if (elem.Z() >= z)
                    {
                        if (elem.Z() - z <= 30) return true;
                    }
                    else
                    {
                        if (z - elem.Z() <= 30) return true;
                    }
                }
            }
        }
        return false;
    }
}
