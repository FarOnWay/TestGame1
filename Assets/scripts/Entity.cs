using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    LifeManager lifeManager;

   public HeroKnight hero;




    void Start()
    {

        lifeManager = GetComponent<LifeManager>();
        hero = GetComponent<HeroKnight>();

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
                Debug.Log("foreach");
                Test.Enemy enemy = collider.gameObject.GetComponent<Test.Enemy>();
                if (enemy != null)
                {
                    Debug.Log("sexo");
                    enemy.TakeDamage(damage);
                }
            }
        }

        else
        {
            Debug.Log("player sendo comido");
            Debug.Log("testando sexo");

            hero.TakeDamage(10);
        }

    }


    public virtual void TakeDamage(int damage)
    {
        Debug.Log("alo hero");
        lifeManager.lifeCount -= damage;

        if (lifeManager.lifeCount <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }
}
