using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int damage = 10; 
    public float knockbackForce = 5f; 

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("test");
            // Call the DealDamage method to attack
            DealDamage();
        }
    }

    void DealDamage()
    {
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        Vector2 hitboxSize = new Vector2(1f, 1f);
        Vector2 hitboxCenter = transform.position;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0, layerMask);

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Deal damage to the enemy
                enemy.TakeDamage(damage);

                Rigidbody2D enemyRb = collider.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    // Apply knockback force to the enemy
                    Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;
                    enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
