using UnityEngine;
using System.Collections;


public class FlyingEnemyController : EnemyController
{
    //Default script for EVERY ENEMY THAT FLIES
    public float moveSpeed = 1f;
    public int damage;
    public float minY = -1f;
    public float maxY = 1.5f;
    public float avoidForce = 5f;
    // public LayerMask obstacleLayers; 
    // void Start()
    // {
    //     player = hero.GetComponent<Transform>();
    // }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position + new Vector3(0, 1), moveSpeed * Time.deltaTime);
        AvoidCollisionWithNonPlayer();
    }

    public override void AvoidCollisionWithNonPlayer()
    {
        // Cast a ray forward from the enemy's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 1f, base.obstacleLayers);

        // If the ray hits an obstacle
        if (hit.collider != null)
        {
            // Make the enemy move up or down to avoid the obstacle
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Determine the direction to move based on the enemy's and the obstacle's vertical positions
                float direction = (transform.position.y < hit.transform.position.y) ? -1f : 1f;

                // Apply the avoid force
                rb.AddForce(new Vector2(0, direction * avoidForce), ForceMode2D.Impulse);
            }
        }
    }


    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log("escostando no player");

    //         base.DealDamage(10, false);
    //         Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;

    //         //StartCoroutine(KnockbackCoroutine(knockbackDirection));

    //     }
    // }

    // public  IEnumerator KnockbackCoroutine(Vector2 knockbackDirection)
    // {
    //     float elapsedTime = 0f;


    //     while (elapsedTime < knockbackDuration)
    //     {
    //         Vector2 newPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance * Time.deltaTime;

    //         transform.position = newPosition;
    //        // Debug.Log("tomando knockback");

    //         elapsedTime += Time.deltaTime;

    //         yield return null;
    //     }
    // }

}
