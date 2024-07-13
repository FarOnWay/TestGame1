using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryUIController : MonoBehaviour
{
    public InventoryController inventoryController;
    public GameObject inventorySlotPrefab;
    public GameObject tooltip;
    public Text itemNameText;
    public Text itemRarityText;
    public Text itemTypeText;

    public bool showTooltip = false;
    public Item itemGot;
    // the index of the current slot where the next item should be added
    // starts at 0 and goes up to INVENTORY_SIZE - 1
    private int currentSlotIndex = 0;
    private const int INVENTORY_SIZE = 10; // amount of slots we have in our inventory (in the hotbar)

    private void Start()
    {
        createSlots();
    }

    private void Update()
    {
        GetPressedKey();
    }

    public void GetPressedKey()
    {
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                //  Debug.Log("DFIUHHHH AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + i);
                // Debug.Log("Key " + i + " pressed");
                inventoryController.UseItem(i);
                SelectSlot(i);
            }
        }
    }

    public void SelectSlot(int index)
    {
        //  Debug.Log("test");
        inventoryController.UseItem(index);

        for (int i = 0; i < transform.childCount; i++)
        {
            InventorySlotController slotController = transform.GetChild(i).GetComponent<InventorySlotController>();
            slotController.ClearHighlight();
        }

        InventorySlotController selectedSlotController = transform.GetChild(index).GetComponent<InventorySlotController>();
        selectedSlotController.SetHighlight();
    }

    private void createSlots()
    {
        for (int i = 0; i < INVENTORY_SIZE - 1; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, transform);
            Text slotPositionText = slot.GetComponentInChildren<Text>();
            slotPositionText.text = i.ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip(itemGot);
        Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    public void updateInventoryHUD(Item item, bool isNewItem)
    {
        itemGot = item;
        // Debug.Log("Updating inventory HUD");
        //  Debug.Log("nome do item: " + item.name);
        // SetItemDetails(item);

        if (isNewItem && currentSlotIndex < INVENTORY_SIZE - 1)
        {
            currentSlotIndex++;
        }

        // get the current slot
        InventorySlotController slotController = transform.GetChild(currentSlotIndex).GetComponent<InventorySlotController>();

        // update the current slot with 
        slotController.SetItem(item.icon);

        // set the quantity of the item in the current slot (not workin)
        if (InventoryController.Inventory.ContainsKey(item))
        {
            slotController.SetQuantity(InventoryController.Inventory[item]);
        }
    }

    public void ShowTooltip(Item slot)
    {
        tooltip.SetActive(true);
        Image icon = tooltip.GetComponent<Image>();
        icon.enabled = true;
        SetItemDetails(slot);
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    // public Item GetItem()
    // {
    //     return item;
    // }

    private void SetItemDetails(Item item)
    {
        if (item == null) return;

        itemNameText.text = item.ItemName;
        itemTypeText.text = $"{item.itemType}";
        itemRarityText.text = $"{item.itemRarity}";
    }

}