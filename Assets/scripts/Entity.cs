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

    public virtual void DealDamage(int damage, bool isPlayer, bool isLookLeft)
    {
       // Debug.Log("DealDamage");
        Vector2 hitboxSize = new Vector2(1.5f, 1.5f); // Hitbox size is constant
        Vector2 hitboxCenter;

        if (isPlayer)
        {
            if (isLookLeft)
            {
                hitboxCenter = transform.position + new Vector3(-0.5f, 0, 0);
            }
            else
            {
                hitboxCenter = transform.position + new Vector3(0.5f, 0, 0);
            }

            Collider2D[] colliders = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                   // Debug.Log("Enemy hit");
                    if (collider.gameObject.TryGetComponent<EnemyController>(out var enemy))
                    {
                        enemy.TakeDamage(damage);
                        if (collider.gameObject.TryGetComponent<Rigidbody2D>(out var enemyRb))
                        {
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

        if (lifeManager.lifeCount < 1)
        {
            Destroy(gameObject);
            loot.DropPrefab();
        }
    }
}
