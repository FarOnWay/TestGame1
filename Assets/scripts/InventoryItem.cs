using UnityEngine;
using System;

public class InventoryItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public Sprite Icon { get; set; }

    public InventoryItem(string name, Sprite icon)
    {
        Name = name;
        Icon = icon;
        Quantity = 1;
    }
}