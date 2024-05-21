using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public Item[] itemsToDrop; // Changed to hold Item objects

    public void DropPrefab()
    {
        // Instantiate the item prefabs at the current position
        foreach (Item item in itemsToDrop)
        {
            Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}