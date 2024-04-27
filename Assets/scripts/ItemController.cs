using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    void Awake()
    {
        // CreateItems();
        int j = 0;

        // foreach (var i in itemsToDrop)
        // {
        //     Debug.Log("essa bosta aqui mostrou " + j);
        //     j++;
        //     Debug.Log("\n");
        //     Debug.Log("items to drop in items controllr to test (awake)" + i);
        //     Debug.Log("\n");
        // }
    }

    public static string Name { get; set; }
    public ItemController(string name)
    {
        Name = name;
    }

    public List<GameObject> itemsToDrop = new();



    public void ItemsToDrop(GameObject item)
    {
        itemsToDrop.Add(item);
        //  itemsToDrop.Add(new("Gel"));
    }
    // method that will store the items that en enemy can drop
    // magic, melee, ranged, throwable, defense
    // rarity items are: commum, uncommum, rare, very rare, epic, legendary
    // this is the base class for creating ANY item, rather damage or defense, melee or range


    #region CraftableItems

    // void CreateItems() => ItemsToDrop(new ("Gel"));


    /**
    This reagion is for create MATERIALS for craftable items
    So, this reagion stores the materials to create an item, like a sword, a shield, a potion, a key, etc.
    Example: A slime can drop 'Gel', that is used to craft a potion, among other things. 
    So to create this item, i thought it would be better if each enemy class had their own method to create 
    these items, like if there are N items in our game, it would be a mess to have all the items in a single file
    So i separated in each enemy class. In how @EnemyController<T> class, we have a method called ItemsToDrop()
    So, we want to create the item 'Gel', that is a drop of the enemy @Slime, right? So, we go to the Slime class
    and in the method, we create a new instance of the class ItemController, and pass the name of the item as a parameter  
  


    So, the attributes to create a craftable item are:

    @Name
    @Rarity       
    **/


    #endregion



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

    // int damage, criticalChance, knockback;
    // public ItemController(int damage, int criticalChance, int knockback)
    // {
    //     this.damage = damage;
    //     this.criticalChance = criticalChance;
    //     this.knockback = knockback;
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
}
