using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    ShowDamageTaken showDamageTaken;
    private bool isAnimationPlaying = false;
    Animator animator;
    public HeroKnight hero;
    
    void Start()
    {
        showDamageTaken = GetComponentInChildren<ShowDamageTaken>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        // TakeDamage(10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
           // Debug.Log("Hand hit");
            TakeDamage(hero.damage);
        }

    }

    public void TakeDamage(int damage)
    {
        if (animator != null && !isAnimationPlaying)
        {
            // Set the boolean parameter to trigger the animation
            animator.SetBool("isBeeingAttacked", true);
            isAnimationPlaying = true;
        }

        StartCoroutine(ResetDamageAnimation(0.0f));
        // if (damage <= 0)
        // {
        //     showDamageTaken.ShowDamageTakenOnScreen("é, é isso", false);
        // }
        // else if (damage > 0) showDamageTaken.ShowDamageTakenOnScreen("aouba", false);

        showDamageTaken.ShowDamageTakenOnScreen(damage.ToString(), false);
        //   showDamageTaken.ShowDamageTakenOnScreen("aouba", false);
    }

    private IEnumerator ResetDamageAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isBeeingAttacked", false);
        isAnimationPlaying = false;
    }
}
