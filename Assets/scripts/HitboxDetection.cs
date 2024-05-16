using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDetection : MonoBehaviour
{
    AttackController attackController;
    public LayerMask enemyLayer;
    int enemyLayerInt;
    void Start()
    {
        attackController = GetComponentInParent<AttackController>();
        enemyLayerInt = (int) Mathf.Log(enemyLayer.value, 2);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D enemyCollider){
        if (enemyCollider.gameObject.layer != enemyLayerInt) return;

        attackController.DealDamage(enemyCollider);
    }
}
