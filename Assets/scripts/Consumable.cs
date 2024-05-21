using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [CreateAssetMenu(fileName = "NewConsumableItem", menuName = "Items/ConsumableItem")]
    public class ConsumableItem : Item
    {
        public int healingAmount;
    }
}
