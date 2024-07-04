using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [CreateAssetMenu(fileName = "NewProjectileItem", menuName = "Items/ProjectileItem")]
    public class ProjectileItem : Item
    {
        public int damage;
    }
}
