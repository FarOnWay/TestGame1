using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryController : MonoBehaviour
{
    public ItemController item;
    public InventoryUIController inventoryUIController;
    public ItemController[] Slots = new ItemController[10];
    public Dictionary<ItemController, int> Inventory = new();

    public GameObject extendedInventory;



    // has_many items
    // belongs_to player

    // enemys can ONLY dropItems

    //  public List<GameObject> Inventory = new();
    // public List<GameObject> Inventory = new();

    private void Start()
    {
        extendedInventory.SetActive(false);
        item = GetComponent<ItemController>();
    }

    public void CollectItem(ItemController item)
    {

        foreach (var i in Inventory)
        {
            Debug.Log("item na posiçao: " + i);
            Debug.Log(i.Value);
            Debug.Log(i.Key.Name);

        }

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

    public void UseItem(int index)
    {
      //  Debug.Log("ALOOOO");

        if (index < 0 || index >= Inventory.Count)
        {
           // Debug.Log("Invalid slot index");
            return;
        }

        var item = Inventory.Keys.ElementAt(index);

        if (item != null)
        {
           // Debug.Log("Using item " + item.Name);
            item.Use();
            if (Inventory[item] > 1)
            {
                Inventory[item]--;
            }
            else
            {
                Inventory.Remove(item);
            }
        }
        else
        {
           // Debug.Log("No item in slot");
        }
    }

    public void seeInventory()
    {
        extendedInventory.SetActive(!extendedInventory.activeSelf);

        if (Inventory.Count == 0)
        {
           // Debug.Log("Seu inventario esta vazio!");
        }
        else
        {
            Debug.Log("Seus itens são: ");
            foreach (KeyValuePair<ItemController, int> i in Inventory)
            {
                Debug.Log($"{i.Key.Name} (x{i.Value})");
            }
        }
    }
}
