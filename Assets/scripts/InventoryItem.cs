using UnityEngine;
using System;

public class InventoryItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public Sprite Icon { get; set; }
    public GameObject Prefab { get; set; } // Reference to the item prefab


    public InventoryItem(string name, Sprite icon, GameObject prefab)
    {
        Name = name;
        Icon = icon;
        Quantity = 1;
        Prefab = prefab;
    }
}