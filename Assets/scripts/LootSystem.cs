using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : ItemController
{
    // public GameObject prefab;
    // public int dropProbability = 70;
    public void DropPrefab()
    {
        Instantiate(itemsToDrop[1], transform.position, Quaternion.identity);
        Instantiate(itemsToDrop[0], transform.position, Quaternion.identity);

        //Debug.Log("supossed to drop this: " + itemsToDrop[selectLoot()]);
        // Debug.Log(selectLoot());

        foreach (var i in itemsToDrop)
        {
            //  Debug.Log("items to drop in LOOT SYSTEM " + i);
            // Debug.Log("SEXO ANALLLLLLLLLLLLLLL");

        }
        //   Debug.Log(itemsToDrop);
        //  Debug.Log(itemsToDrop[selectLoot()]);
    }

    public LootSystem() : base("SomeName", null)
    {
        // ...
    }

    public void lootChance()
    {
        int rnd = UnityEngine.Random.Range(1, 101);
        if (rnd <= 101)
        {
            // Debug.Log("dropando " + rnd);
            DropPrefab();

        }
        // else Debug.Log("nao foi dessa vez" + rnd);
    }

    int selectLoot()
    {
        int randItemToDrop = UnityEngine.Random.Range(1, itemsToDrop.Count - 1);
        return randItemToDrop;
    }
}
