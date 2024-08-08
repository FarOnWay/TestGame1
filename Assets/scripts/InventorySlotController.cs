using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Text itemAmount;
    public GameObject tooltip;
    public Text itemNameText;
    public Text itemRarityText;
    public Text itemTypeText;
    public InventoryUIController inventoryUIController;
    public Item items;

    public void SetItem(Sprite itemIcon)
    {
        icon.sprite = itemIcon;
    }

    public void SetQuantity(int quantity)
    {
        itemAmount.text = quantity.ToString();
    }

    public void Clear()
    {
        icon.sprite = null;
        itemAmount.text = "";
    }

    public void SetHighlight()
    {
        GetComponent<Image>().color = Color.yellow;
    }

    public void ClearHighlight()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // inventoryUIController.ShowTooltip(inventoryUIController.itemGot);
        // Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip
       // inventoryUIController.HideTooltip();
    }

    //needs to write a method to get the item info from a determinet slot whn the mouse pass over it
}

