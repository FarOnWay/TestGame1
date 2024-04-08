using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Entity : HealthBar
{

    LifeManager lifeManager;
    public HeroKnight hero;

    EnemyController enemy;
    public int vida;
    public int maxLife;



    void Start()
    {
        lifeManager = GetComponent<LifeManager>();
        hero = GetComponent<HeroKnight>();
        vida = maxLife;

    }

    public virtual void DealDamage(int damage, bool isPlayer)
    {
        if (isPlayer)
        {
            Debug.Log("player comendo monstros");
            LayerMask layerMask = LayerMask.GetMask("Enemy");
            Vector2 hitboxSize = new Vector2(1f, 1f);
            Vector2 hitboxCenter = transform.position + new Vector3(1f * 1, 0, 0);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0, layerMask);

            foreach (Collider2D collider in colliders)
            {
                Debug.Log(colliders);
                Debug.Log(collider);
                //EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();

                if (enemy != null)
                {
                    Debug.Log("player dando dano");
                    TakeDamage(damage);
                }
            }
        }

        else
        {
            Debug.Log("player sendo comido");
            Debug.Log("testando sexo");
            hero.TakeDamage(damage);
        }

    }

    public virtual void TakeDamage(int damage)
    {
        Debug.Log("aii pai paraa");
        //Debug.Log(lifeManager.lifeCount);
        vida -= damage;
        //lifeManager.lifeCount -= damage;

        if (vida <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }
}
