using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConsumableItems : ItemController
{
    public int AmountOfHeal;

    /** here declare the buffs that an consumable item can give to the player
      for now, we will leave just the amount of heal, but we can add more buffs later
      like speed, attack, defense, attack speed, life regeneration, etc.
    **/

    public ConsumableItems(string name, Sprite icon, GameObject prefab, int rarity, int amountOfHeal) : base(name, icon, prefab, rarity)
    {
        AmountOfHeal = amountOfHeal;
    }

    public override void Use()
    {
        Debug.Log("Consumed " + Name);
        // Add code here to apply the effects of the consumable item
    }
}