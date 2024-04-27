using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    ItemController item;
    // has_many items
    // belongs_to player

    // enemys can ONLY dropItems

    public string test() => "test";

    //  public List<GameObject> Inventory = new();
    // public List<GameObject> Inventory = new();
    public Dictionary<string, int> Inventory = new();

    public void CollectItem(GameObject item)
    {
        string itemName = item.name;
        if (Inventory.ContainsKey(itemName))
        {
            Inventory[itemName]++;
        }
        else
        {
            Inventory.Add(itemName, 1);
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

    void DropItem(ItemController item)
    {

    }
    void Trash(ItemController item)
    {

    }


}
