using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyController : Entity
{


    public Transform playerTransform;
    public float knockbackDistance;
    HeroKnight hero;
    public float knockbackDuration;
    public int speed;


    void Start()
    {
    }

    public virtual void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log("escostando no player");

    //         base.DealDamage(10, false);
    //         Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
    //         Debug.Log(knockbackDirection);

    //         StartCoroutine(KnockbackCoroutine(knockbackDirection));

    //     }
    // }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           // Debug.Log("escostando no player");

            base.DealDamage(10, false);
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
            Debug.Log(knockbackDirection);

            StartCoroutine(KnockbackCoroutine(knockbackDirection));

        }
    }

    IEnumerator KnockbackCoroutine(Vector2 knockbackDirection)
    {

        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {

            if (knockbackDirection.x < 0) playerTransform.position += Vector3.left * knockbackDistance * Time.deltaTime;

            else playerTransform.position += Vector3.right * knockbackDistance * Time.deltaTime;

            // Vector2 newPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance * Time.deltaTime;

            // transform.position = newPosition;
            // Debug.Log("tomando knockback");

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
