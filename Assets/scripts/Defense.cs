using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{

    [CreateAssetMenu(fileName = "NewDefenseItem", menuName = "Items/DefenseItem")]
    public class DefenseItem : Item
    {
        public int defenseAmount;
    }
}
