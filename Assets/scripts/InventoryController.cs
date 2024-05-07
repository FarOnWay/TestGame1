using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryController : MonoBehaviour
{
    // public ItemController item;
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
        // item = GetComponent<ItemController>();
    }

    public void CollectItem(ItemController item)
    {
        // Debug.Log("CollectItem called with item " + item.Name);

        if (Inventory.ContainsKey(item))
        {
            Inventory[item]++;
        }
        else
        {
            Inventory.Add(item, 1);
        }

        inventoryUIController.updateInventoryHUD(item, Inventory.ContainsKey(item));
        // Debug.Log("Inventory count after collecting item: " + Inventory.Count);

    }

    public void UseItem(int index)
    {
        //  Debug.Log("ALOOOO");
        //  Debug.Log("UseItem called with index " + index);
        // Debug.Log("Inventory count before using item: " + Inventory.Count);



        // Debug.Log("tentado usar item nessa buceta");
        //  Debug.Log("tamanho do inventário: " + Inventory.Count);

        if (Inventory.Count == 0)
        {
            // Debug.Log("Invalid slot index");
            Debug.Log("ai é foda meu patral");
            return;
        }

        else
        {
            var item = Inventory.ElementAt(index).Key;
            // Debug.Log("agua coca latao agua coca latao" + item);
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

        //  var item = Inventory.Keys.ElementAt(index);

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
            var item = Inventory.ElementAt(2).Key;
            Debug.Log("Item na posiçao 2: " + Inventory.ElementAt(2).Key);
            // Debug.Log("agua coca latao agua coca latao" + item);
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
            Debug.Log("Seus itens são: ");
            foreach (KeyValuePair<ItemController, int> i in Inventory)
            {
                Debug.Log($"{i.Key.Name} (x{i.Value})");
            }
        }
    }
}
