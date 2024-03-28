using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int damage = 10; 
    public float knockbackForce = 5f;

    public void DealDamage(Collider2D enemyCollider)
    {
        Debug.Log("DEALING DAMAGE");
        Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            // Apply knockback force to the enemy
            Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;
            knockbackDirection = (knockbackDirection + new Vector2(0, 1f)).normalized;
            enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
