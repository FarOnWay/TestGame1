using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [CreateAssetMenu(fileName = "NewAttackItem", menuName = "Items/AttackItem")]
    public class AttackItem : Item
    {
        public int damage;
        public float attackSpeed;
        public float criticalChance;
        public float knockbackForce;
    }
}
