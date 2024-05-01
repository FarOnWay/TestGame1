using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Image icon;
    public Text itemAmount; 

    public void SetItem(Sprite itemIcon)
    {
        icon.sprite = itemIcon;
      //  itemAmount.text = quantity.ToString();

    }

    // public void SetQuantity(int quantity)
    // {
    //     // Update the quantity display
    //     itemAmount.text = quantity.ToString();
    // }

    public void Clear()
    {
        icon.sprite = null;
        itemAmount.text = "";
    }
}