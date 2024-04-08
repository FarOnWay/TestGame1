using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyController : Entity
{

    Vector3 playerPos;

    public Transform playerTransform;
    public float knockbackDistance;
    public float knockbackDuration;
    public int speed;


    void Start()
    {
    }

    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        playerPos = playerTransform.position;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("escostando no player");

            base.DealDamage(10, false);
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;

            StartCoroutine(KnockbackCoroutine(knockbackDirection));

        }
    }

    IEnumerator KnockbackCoroutine(Vector2 knockbackDirection)
    {
        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            Vector2 newPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance * Time.deltaTime;

            transform.position = newPosition;
            Debug.Log("tomando knockback");

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
