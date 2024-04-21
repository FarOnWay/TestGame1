using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // magic, melee, ranged
    string type;
    string itemName;
    string[] rarity = { "commum", "uncommum", "rare", "very rare", "epic", "legendary" };

    public ItemController(string itemType, string itemName, string itemRarity)
    {
        type = itemType;
        this.itemName = itemName;
       // rarity[ite] = itemRarity;
    }
}
