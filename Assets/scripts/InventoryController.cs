using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class InventoryController : MonoBehaviour
{
    // public ItemController item;
    public InventoryUIController inventoryUIController;

    public HeroKnight player;
    public GameObject playerHand;
    // public ItemController[] Slots = new ItemController[10];
    public static Dictionary<Item, int> Inventory = new();
    public bool isInventorFull = false;
    public GameObject extendedInventory;
    Item item;
    // has_many items
    // belongs_to player
    // enemys can ONLY dropItems

    //  public List<GameObject> Inventory = new();
    // public List<GameObject> Inventory = new();

    private void Start()
    {
        extendedInventory.SetActive(false);

        if (playerHand == null)
        {
            //   Debug.Log("começando sem mao SEM MAO NAO TEM MAO PQ É NULL");
        }
        // else Debug.Log("COMEÇANDO COM A PORRA DA MAO NESSE CARALHO");
        player = GetComponent<HeroKnight>();
        // item = GetComponent<ItemController>();
    }

    void Update()
    {
        // Debug.Log("Player Hand in Update: " + playerHand);
        // UseItem(0);
    }

    public void CollectItem(Item item)
    {
        int maxInventorySize = 4; // Define your maximum inventory size here

        // Calculate the total number of items in the inventory
        int totalItemCount = Inventory.Values.Sum();

        // Check if the inventory is full
        if (totalItemCount >= maxInventorySize)
        {
            Debug.Log("INVENTORY IS FULL");
            isInventorFull = true;
            return;
        }

        else
        {
            // Debug.Log("QUERO MINHA EX DE VOLTA");
            if (Inventory.ContainsKey(item))
            {
                Inventory[item]++;
            }
            else
            {
                Inventory.Add(item, 1);
            }

            inventoryUIController.updateInventoryHUD(item, Inventory.ContainsKey(item));
        }
        //  Debug.Log("INVENTORY COUNT " + totalItemCount);

        // Collect the item

    }

    public void UseItem(int index)
    {
        List<Item> items = new List<Item>(Inventory.Keys);

        if (index >= 0 && index < items.Count)
        {
            item = items[index];

            if (item.itemType == ItemType.Projectile)
            {
                Debug.Log("You have a projectile in your hand and you have " + Inventory[item] + " of this projectile");

                if (Inventory.ContainsKey(item) && Inventory[item] > 0)
                {
                    Inventory[item]--;

                    if (Inventory[item] == 0)
                    {
                        Inventory.Remove(item);
                    }
                }
            }
            else if (item != null && item.icon != null)
            {
                playerHand.GetComponent<HandController>().equippedItem = item;
            }
            else
            {
                playerHand.GetComponent<HandController>().equippedItem = null;
            }
        }
        else
        {
            playerHand.GetComponent<HandController>().equippedItem = null;
        }
    }

    public void SeeInventory()
    {
        foreach (KeyValuePair<Item, int> entry in Inventory)
        {
            Debug.Log(entry.Key.ItemName + ": " + entry.Value);
            Debug.Log(entry.Key.itemType + ": " + entry.Value);
            Debug.Log(entry.Key.itemPrefab + ": " + entry.Value);
        }
        Debug.Log(ShootProjectile());

    }

    public static bool ShootProjectile()
    {
        foreach (KeyValuePair<Item, int> entry in Inventory)
        {
            if (entry.Key.itemType == ItemType.Projectile && Inventory[entry.Key] > 0)
            {
                Inventory[entry.Key]--;
                //  RetrieveProjectile(entry.Key);
                return true;
            }
            return false;
        }
        return false;
    }

    public static GameObject GetProjectilePrefab()
    {
        foreach (KeyValuePair<Item, int> entry in Inventory)
        {
            if (entry.Key.itemType == ItemType.Projectile && Inventory[entry.Key] > 0)
            {
                Debug.Log(" s sign");
                Debug.Log("aqui no inventário o negocio é " + entry.Key.itemPrefab);
                return entry.Key.itemPrefab;
            }
        }
        return null;
    }

}

