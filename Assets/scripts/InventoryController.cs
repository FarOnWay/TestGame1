using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    ItemController item;
    public InventoryUIController inventoryUIController; // Set this in the Unity editor


    // has_many items
    // belongs_to player

    // enemys can ONLY dropItems

    public string test() => "test";

    //  public List<GameObject> Inventory = new();
    // public List<GameObject> Inventory = new();
    public Dictionary<string, InventoryItem> Inventory = new();

    public void CollectItem(ItemController item)
    {
        string itemName = item.Name;
        if (Inventory.ContainsKey(itemName))
        {
            Inventory[item.Name].Quantity++;
            // inventoryUIController.updateInventoryHUD(item);
            inventoryUIController.updateInventoryHUD(item, false);


        }
        else
        {
            // Debug.Log($"Adicionando item ao inventario {item.Name} {item.Icon}");
            InventoryItem inventoryItem = new InventoryItem(item.Name, item.Icon);
            Inventory.Add(item.Name, inventoryItem);
            inventoryUIController.updateInventoryHUD(item, true);

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

    void DropItem(ItemController item)
    {

    }
    void Trash(ItemController item)
    {

    }


}
