using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemNameDisplay : MonoBehaviour
{
    public Text itemNameText;

    public void DisplayItemName(Item item)
    {
        itemNameText.text = item.ItemName;

        switch (item.itemRarity)
        {
            case ItemRarity.Common:
                itemNameText.color = Color.white;
                StartCoroutine(ClearMessage(2f));
                break;
            case ItemRarity.Uncommon:
                itemNameText.color = Color.green;
                StartCoroutine(ClearMessage(2f));
                break;
            case ItemRarity.Rare:
                itemNameText.color = Color.blue;
                StartCoroutine(ClearMessage(2f));
                break;
            case ItemRarity.Epic:
                itemNameText.color = Color.magenta;
                StartCoroutine(ClearMessage(2f));
                break;
            case ItemRarity.Legendary:
                itemNameText.color = Color.yellow;
                StartCoroutine(ClearMessage(2f));
                break;
        }
    }

    private IEnumerator ClearMessage(float delay)
    {
        yield return new WaitForSeconds(delay);

        itemNameText.text = "";
    }
}