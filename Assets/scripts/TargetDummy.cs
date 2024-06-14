using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    ShowDamageTaken showDamageTaken;
    public HeroKnight hero;
    void Start()
    {
        showDamageTaken = GetComponentInChildren<ShowDamageTaken>();
    }

    void Update()
    {
        // TakeDamage(10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Hand hit");
            TakeDamage(hero.damage);
        }

    }

    public void TakeDamage(int damage)
    {
        showDamageTaken.ShowDamageTakenOnScreen(damage.ToString(), false);
    }
}
