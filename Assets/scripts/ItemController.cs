using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // magic, melee, ranged, throwable, defense


    #region Attack
    /**
    Attack items attributes are:
    @Types {Melee, Magic, Ranged, Throwable}
    @Damage
    @KnockbackForce
    @CriticalChance
    @AttackSpeed
     * Attack items does not contain definitions for chracter attributes, like @Armor, @Life, @Mana and @MoveSpeed
    **/
    public int damage, knockback, criticalChance, attackSpeed, amount;
    public bool costAmount;
    public string type;

    // public int damage = 0, knockback = 0, criticalChance = 0;
    // void createAttackItem(string type, int damage, int knockback, int criticalChance)
    // {
    //     switch (type)
    //     {
    //         case "throwable":
    //             int amount;
    //             break;
    //         case "ranged":
    //             int bullets;
    //             break;
    //         case "melee":
    //             this.damage = damage;
    //             this.knockback = knockback;
    //             this.criticalChance = criticalChance;
    //            // Items.Add("Melee");

    //             break;
    //         case "magic":
    //         int manaCost;

    //             break;

    //         default:
    //             throw new System.Exception("wrong name type, mofo");

    //     }
    // }
    #endregion

    #region Defense
    /**
    Defense items attributes are:
    @Armor
    @LifeRegen
    @BlockKnockback{true/false}
    @MoveSpeed
    @FallDamage{true/false, reduce fall damage}
    @MeleeDamage{reduction}
    @RangedDamage{reduction}
    @PoisoningDamage{reduction}
    @Mana{increase}
    @ManaRegen
    **/
    #endregion

   


    public ItemController(string itemType, string itemName, string itemRarity)
    {
        // type = itemType;
        // this.itemName = itemName;
       // this.itemRarity = itemRarity;


        // Debug.Log(rarity[1]);
        // rarity[ite] = itemRarity;
    }
}
