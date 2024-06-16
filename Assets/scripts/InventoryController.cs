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
            Debug.Log("QUERO MINHA EX DE VOLTA");
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
        List<Item> items = new(Inventory.Keys);

        if (index >= 0 && index < items.Count)
        {
            Item item = items[index];

            if (item != null && item.icon != null) playerHand.GetComponent<HandController>().equippedItem = item;

            else playerHand.GetComponent<HandController>().equippedItem = null;
        }

        else playerHand.GetComponent<HandController>().equippedItem = null;
    }

    public void SeeInventory()
    {
        foreach (KeyValuePair<Item, int> entry in Inventory)
        {
            Debug.Log(entry.Key.ItemName + ": " + entry.Value);
        }
    }
}

