using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    ItemController item;
    // has_many items
    // belongs_to player and enemy's

    // enemys can ONLY dropItems

    public string test() => "test";

    //  public List<GameObject> Inventory = new();
    public List<ItemData> Inventory = new();



    public void seeInventory()
    {
        if (Inventory == null)
        {
            Debug.Log("Seu inventario esta vazio!");
        }
        else
        {
            Debug.Log("Seu items sao: ");
            foreach (var i in Inventory)
            {
                Debug.Log($"{i}\n");
            }
        }

    }


    void DragItem(ItemController item)
    {

    }

    public void CollectItem(ItemData item)
    {
        Inventory.Add(item);
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
