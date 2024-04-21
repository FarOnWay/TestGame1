using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    public int jumpForce = 0;
    public int jumpCoodown;

    // randomizing the jumpCoodown 
    int randJumpCoolDown()
    {
        jumpCoodown = Random.Range(4, 12);
        return jumpCoodown;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(JumpCooldown(randJumpCoolDown()));
        }

        if (other.gameObject.CompareTag("Player"))
        {
            base.DealDamage(10, false);
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
            Debug.Log(knockbackDirection);
            StartCoroutine(KnockbackCoroutine(knockbackDirection));
        }
    }



    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.y, jumpForce);
    }

    override public void Update()
    {
        // overriding update and leaving it empty because the base method has a Move() method
        // and we only want to call Move() when the slime is jumping, otherwise it would 
        // follow the player while on ground
    }

    override public void Move()
    {
        if (transform.position.x < playerTransform.position.x)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
        }
    }

    IEnumerator JumpCooldown(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        Jump();
        Move();
    }
}
