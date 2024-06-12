using UnityEngine;

public enum ItemType
{
    Consumable,
    Attack,
    Defense,
    Material,

}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string ItemName { get { return this.name; } }
    public ItemType itemType;
    public ItemRarity itemRarity;
    public MonoBehaviour itemScript;
    public Sprite icon;
    public GameObject itemPrefab;
    public ItemCollisionDetector itemCollision;

    public bool IsTouchingEnemy()
    {
        if (itemCollision != null)
        {
            return itemCollision.isTouchingEnemy;
        }
        return false;

    }

}