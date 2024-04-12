using UnityEngine;
using System.Collections;


public class FlyingEnemyController : EnemyController
{

    //Default script for EVERY ENEMY THAT FLIES
    public float moveSpeed = 1f;
    public int damage;
    public float minY = -1f;
    public float maxY = 1.5f;


    // void Start()
    // {
    //     player = hero.GetComponent<Transform>();
    // }


    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position + new Vector3(0, 1), moveSpeed * Time.deltaTime);

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

    public  IEnumerator KnockbackCoroutine(Vector2 knockbackDirection)
    {
        float elapsedTime = 0f;
        

        while (elapsedTime < knockbackDuration)
        {
            Vector2 newPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance * Time.deltaTime;

            transform.position = newPosition;
           // Debug.Log("tomando knockback");

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

}
