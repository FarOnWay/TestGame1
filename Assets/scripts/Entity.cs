using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    LifeManager lifeManager;


     Animator m_animator;

    void Start()
    {

        lifeManager = GetComponent<LifeManager>();

        m_animator = GetComponent<Animator>();


    }

    public virtual void TakeDamage(int damage)
    {
        Debug.Log("alo hero");
        m_animator.SetTrigger("Hurt");
        lifeManager.lifeCount -= damage;

        if (lifeManager.lifeCount <= 0)
        {
            m_animator.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
    }
}
