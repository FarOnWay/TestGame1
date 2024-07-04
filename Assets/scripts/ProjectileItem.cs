using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileItem : ItemController
{
    public int Damage;

    public ProjectileItem(string name, Sprite icon, GameObject prefab, int rarity, int damage) : base(name, icon, prefab, rarity)
    {
        Damage = damage;
    }

  
}
