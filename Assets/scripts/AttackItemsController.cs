using UnityEngine;

public class AttackItemsController : ItemController
{
    public int Damage;
    public int CriticalChance;
    public int Knockback;
    public int AttackSpeed;

    public AttackItemsController(string name, Sprite icon, GameObject prefab, int rarity, int damage, int criticalChance, int knockback, int attackSpeed) 
        : base(name, icon, prefab, rarity)
    {
        Damage = damage;
        CriticalChance = criticalChance;
        Knockback = knockback;
        AttackSpeed = attackSpeed;
    }
}