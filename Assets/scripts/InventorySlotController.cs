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
}