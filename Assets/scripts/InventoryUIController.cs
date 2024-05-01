using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public InventoryController inventoryController;
    public GameObject inventorySlotPrefab;

    private int currentSlotIndex = 0; // The index of the current slot where the next item should be added

    const int INVENTORY_SIZE = 10; // amount of slots we have in our inventory

    void Start()
    {
        createSlots();
    }

    void createSlots()
    {
        for (int i = 1; i < INVENTORY_SIZE; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, transform);
            Text slotPositionText = slot.GetComponentInChildren<Text>(); // Replace this with the correct way to access the Text component
            slotPositionText.text = i.ToString();
        }
    }

    public void updateInventoryHUD(ItemController item, bool isNewItem)
    {
        Debug.Log("Updating inventory HUD");
        Debug.Log("nome do item: " + item.Name);
    //    Debug.Log( "item do inventário: " + inventoryController.Inventory.Keys[item.Name]);

        // If the item is new, increment the current slot index
        if (isNewItem)
        {
            currentSlotIndex++;
        }

        // Get the current slot
        InventorySlotController slotController = transform.GetChild(currentSlotIndex).GetComponent<InventorySlotController>();

        // Update the current slot with the collected item
        slotController.SetItem(item.Icon);

        // Set the quantity of the item in the current slot
        if (inventoryController.Inventory.ContainsKey(item.gameObject.name))
        {
            //  slotController.SetQuantity(inventoryController.Inventory[item.Name].Quantity);
        }
    }
}