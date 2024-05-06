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

    void Update()
    {
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                SelectSlot(i);
            }
        }
    }

    public void SelectSlot(int index)
    {
      //  Debug.Log("SelectSlot called with index " + index);
        inventoryController.UseItem(index);


        for (int i = 0; i < transform.childCount; i++)
        {
            InventorySlotController slotController = transform.GetChild(i).GetComponent<InventorySlotController>();
            slotController.ClearHighlight();
        }

        InventorySlotController selectedSlotController = transform.GetChild(index).GetComponent<InventorySlotController>();
        selectedSlotController.SetHighlight();
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
       // Debug.Log("Updating inventory HUD");
        Debug.Log("nome do item: " + item.Name);

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
        if (inventoryController.Inventory.ContainsKey(item))
        {
            slotController.SetQuantity(inventoryController.Inventory[item]);
        }
    }
}