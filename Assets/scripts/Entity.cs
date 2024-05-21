using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    public LootSystem loot;
    public LifeManager lifeManager;
    public HeroKnight hero;
    public ItemController Item;
    public float knockbackForce;
    public Rigidbody2D rb;
    EnemyController enemy;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        loot = GetComponent<LootSystem>();
        Item = GetComponent<ItemController>();
    }

    public virtual void Start()
    {
        lifeManager = GetComponent<LifeManager>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.UpdateHealthBar(lifeManager.lifeCount, lifeManager.maxLife);
    }

    public virtual void DealDamage(int damage, bool isPlayer)
    {
        Vector2 hitboxSize;
        Vector2 hitboxCenter;

        if (isPlayer)
        {
            // Debug.Log("o tubarao é viado");
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
                        enemy.TakeDamage(damage);
                        Rigidbody2D enemyRb = collider.gameObject.GetComponent<Rigidbody2D>();
                        if (enemyRb != null)
                        {
                            // Debug.Log("sexo 2");
                            Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;
                            knockbackDirection = (knockbackDirection + new Vector2(0, 1f)).normalized;
                            enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                        }
                    }
                }
            }
        }

        else
        {
            hero.TakeDamage(damage);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        //  Debug.Log("aii pai paraa");
        //Debug.Log(lifeManager.lifeCount);
        lifeManager.lifeCount -= damage;
        healthBar.UpdateHealthBar(lifeManager.lifeCount, lifeManager.maxLife);
        //lifeManager.lifeCount -= damage;

        if (lifeManager.lifeCount <= 0)
        {
            Destroy(gameObject);
            loot.DropPrefab();
        }
    }
}
