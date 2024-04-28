using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro
using UnityEngine.UI; // Image

public class InventorySlotController : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI quantityText;

    public void SetItem(ItemController item)
    {
        icon.sprite = item.Icon;
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
        quantityText.enabled = false;
    }

    public void DrawSlot(ItemController item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }
        icon.enabled = true;
        labelText.enabled = true;
        quantityText.enabled = true;

        icon.sprite = item.Icon;
        labelText.text = item.Name;
        quantityText.text = item.Quantity.ToString();

    }
}
