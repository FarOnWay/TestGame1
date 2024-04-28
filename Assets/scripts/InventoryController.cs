using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    // has_many items
    // belongs_to player

    // enemys can ONLY dropItems

    //  public List<GameObject> Inventory = new();
    // public List<GameObject> Inventory = new();
    ItemController item;
    public InventorySlotController[] inventorySlots;
    public Transform playerPosition;
    public GameObject itemPrefab;



    public Dictionary<string, ItemController> Inventory = new();
    public void CollectItem(ItemController item)
    {
        if (item == null)
        {
            Debug.LogError("Tried to collect a null item.");
            return;
        }

        string itemName = item.Name;
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("Tried to collect an item with a null or empty name.");
            return;
        }

        if (Inventory.ContainsKey(itemName))
        {
            Inventory[itemName].Quantity++;
            UpdateInventorySlots();

        }
        else
        {
            Inventory.Add(itemName, item);
            UpdateInventorySlots();

        }

    }
    void UpdateInventorySlots()
    {
        // Clear all slots
        foreach (var slot in inventorySlots)
        {
            slot.ClearSlot();
        }

        // Fill slots with items from the inventory
        int index = 0;
        foreach (var item in Inventory.Values)
        {
            if (index < inventorySlots.Length)
            {
                inventorySlots[index].SetItem(item);
                index++;
            }
            else
            {
                break; // If there are more items than slots, stop adding items
            }
        }
    }
    public void seeInventory()
    {
        if (Inventory.Count == 0)
        {
            Debug.Log("Seu inventario esta vazio!");
        }
        else
        {
            Debug.Log("Seu items sao: ");
            foreach (var i in Inventory)
            {
                Debug.Log($"{i.Key} (x{i.Value})\n");
            }
        }
    }

    void DragItem(ItemController item)
    {

    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Item"))
    //     {
    //         Destroy(other.gameObject);
    //         Debug.Log("voce coletou " + other.gameObject.name);
    //     }
    // }

    public void DropItem(string itemName)
    {
        if (Inventory.ContainsKey(itemName))
        {
            Inventory[itemName].Quantity--;
            if (Inventory[itemName].Quantity == 0)
            {
                Inventory.Remove(itemName);
            }

            // Instantiate a new GameObject at the player's position.
            // You'll need a reference to the player's position and the item prefab.
            //  GameObject item = Instantiate(itemPrefab, playerPosition, Quaternion.identity);
            //   item.name = itemName;
        }
        else
        {
            Debug.Log("Item not found in inventory.");
        }
    }
    public void Trash(ItemController item)
    {
        string itemName = item.Name;
        if (Inventory.ContainsKey(itemName))
        {
            Inventory[itemName].Quantity--;
            if (Inventory[itemName].Quantity == 0)
            {
                Inventory.Remove(itemName);
            }

            GameObject droppedItem = Instantiate(itemPrefab, playerPosition.position, Quaternion.identity);
            droppedItem.name = itemName;
        }
        else
        {
            Debug.Log("Item not found in inventory.");
        }

        UpdateInventorySlots();
    }


}
