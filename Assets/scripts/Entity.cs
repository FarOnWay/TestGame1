using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : HealthBar
{

    public LifeManager lifeManager;
    public HeroKnight hero;

    public float knockbackForce;

    EnemyController enemy;


    public void Start()
    {
        lifeManager = GetComponent<LifeManager>();
        hero = GetComponent<HeroKnight>();
    }

    public virtual void DealDamage(int damage, bool isPlayer)
    {
        Vector2 hitboxSize;
        Vector2 hitboxCenter;

        if (isPlayer)
        {

            Debug.Log("o tubarao é viado");
            hitboxSize = new Vector2(1f, 1f);
            hitboxCenter = transform.position + new Vector3(1f * 1, 0, 0);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0);


            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
                    if (enemy != null)
                    {
                        Debug.Log("sexo");
                        enemy.TakeDamage(damage);

                        Rigidbody2D enemyRb = collider.gameObject.GetComponent<Rigidbody2D>();
                        if (enemyRb != null)
                        {
                            Debug.Log("sexo 2");

                            Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;
                            knockbackDirection = (knockbackDirection + new Vector2(0, 1f)).normalized;
                            enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

                        }
                    }
                }

            }
            //Debug.Log("player comendo monstros");
            // LayerMask layerMask = LayerMask.GetMask("Enemy");
            // Vector2 hitboxSize = new Vector2(1f, 1f);
            // Vector2 hitboxCenter = transform.position + new Vector3(1f * 1, 0, 0);

            // Collider2D[] colliders = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0, layerMask);

            // foreach (Collider2D collider in colliders)
            // {
            //     Debug.Log(colliders);
            //     Debug.Log(collider);
            //     //EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();

            //     if (enemy != null)
            //     {
            //          Debug.Log("player dando dano");
            //         TakeDamage(damage);
            //     }
            // }

        }

        else
        {
            //  Debug.Log("player sendo comido");
            // Debug.Log("testando sexo");
            hero.TakeDamage(damage);
            Debug.Log("o tubarao é viado e da o rabim");




        }
    }

    public virtual void TakeDamage(int damage)
    {
        //  Debug.Log("aii pai paraa");
        //Debug.Log(lifeManager.lifeCount);
        lifeManager.lifeCount -= damage;
        //lifeManager.lifeCount -= damage;

        if (lifeManager.lifeCount <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }
}
