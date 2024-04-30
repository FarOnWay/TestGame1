using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Image icon;
    public Text itemAmount; // Add this line

    public void SetItem(Sprite itemIcon, string itemText)
    {
        icon.sprite = itemIcon;
    }

    public void SetQuantity(int quantity)
    {
        // Update the quantity display
        itemAmount.text = "x" + quantity.ToString();
        itemAmount.text = "test";
    }

    public void Clear()
    {
        icon.sprite = null;
        itemAmount.text = ""; // Clear the quantity display
    }
}