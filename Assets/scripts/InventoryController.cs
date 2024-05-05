using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryController : MonoBehaviour
{
    ItemController item;
    public InventoryUIController inventoryUIController;

    public GameObject extendedInventory;



    // has_many items
    // belongs_to player

    // enemys can ONLY dropItems

    public string test() => "test";

    //  public List<GameObject> Inventory = new();
    // public List<GameObject> Inventory = new();
    public Dictionary<string, InventoryItem> Inventory = new();

    void Start()
    {
        extendedInventory.SetActive(false);
    }

    public void CollectItem(ItemController item)
    {
        string itemName = item.gameObject.name;
        if (Inventory.ContainsKey(itemName))
        {
            Inventory[itemName].Quantity++;
            // inventoryUIController.updateInventoryHUD(item);
            inventoryUIController.updateInventoryHUD(item, false);
        }
        else
        {
            // Debug.Log($"Adicionando item ao inventario {item.Name} {item.Icon}");
            InventoryItem inventoryItem = new InventoryItem(itemName, item.Icon, item.Prefab);
            Inventory.Add(itemName, inventoryItem);
            inventoryUIController.updateInventoryHUD(item, true);
        }

    }

    public void seeInventory()
    {
        extendedInventory.SetActive(!extendedInventory.activeSelf);

        if (Inventory.Count == 0)
        {
            Debug.Log("Seu inventario esta vazio!");
        }
        else
        {
            Debug.Log("Seu items sao: ");
            foreach (var i in Inventory)
            {
                Debug.Log($"{i.Key} (x{i.Value.Quantity})\n");
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


    public void DropItem()
    {
        // Check if the inventory is not empty
        if (Inventory.Count > 0)
        {
            // Get the first item in the inventory
            var firstItem = Inventory.ElementAt(0);
            string itemName = firstItem.Key;
            InventoryItem inventoryItem = firstItem.Value;

            // Decrease the quantity of the item in the inventory
            inventoryItem.Quantity--;

            // If the quantity of the item in the inventory is 0, remove the item from the inventory
            if (inventoryItem.Quantity == 0)
            {
                Inventory.Remove(itemName);
            }

            // Get the item prefab from the inventory item
            GameObject itemPrefab = inventoryItem.Prefab;

            // Define the drop position and rotation
            Vector3 dropPosition = transform.position;
            Quaternion dropRotation = Quaternion.identity;

            // Instantiate the item at the drop position with the drop rotation
            GameObject droppedItem = Instantiate(itemPrefab, dropPosition, dropRotation);

            // Set the name of the dropped item
            droppedItem.name = itemName;
        }
        else
        {
            Debug.LogError("The inventory is empty.");
        }
    }
    void Trash(ItemController item)
    {

    }


}
