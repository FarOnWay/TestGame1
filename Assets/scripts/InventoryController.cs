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
        // Debug.Log("CollectItem called with item " + item.Name);
        if (Inventory.ContainsKey(item)) Inventory[item]++;
        
         else Inventory.Add(item, 1);

        inventoryUIController.updateInventoryHUD(item, Inventory.ContainsKey(item));
        // Debug.Log("Inventory count after collecting item: " + Inventory.Count);
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

