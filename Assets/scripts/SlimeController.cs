using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyController
{
    public int jumpForce = 0, jumpCoodown = 0;
    public bool canJump = false;

    // randomizing the jumpCoodown 
    int randJumpCoolDown()
    {
        jumpCoodown = Random.Range(3, 5);
        return jumpCoodown;
    }

    #region Loot
    // public Slime()
    // {

    // }
    #endregion

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            // can only jump if it's on the ground
            case "Ground":
              //  Debug.Log("no chao");
                canJump = true;
                //  Debug.Log("POSSO PULAR, TO NO CHAO CARAI");
                StartCoroutine(JumpCooldown(randJumpCoolDown(), canJump));
                break;
            case "Player":
               // Debug.Log("batendo no Player");
                canJump = false;
                base.DealDamage(10, false, true);
                // Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
                // //  Debug.Log(knockbackDirection);
                // StartCoroutine(KnockbackCoroutine(knockbackDirection));
                break;
            default:
               // Debug.Log("???");
                canJump = false;
                break;
        }


        // if (other.gameObject.CompareTag("Ground"))
        // {
        //     canJump = true;
        //     Debug.Log("POSSO PULAR, TO NO CHAO CARAI");
        //     StartCoroutine(JumpCooldown(randJumpCoolDown(), canJump));
        // }

        // else
        // {
        //     canJump = false;
        // }
        // //StartCoroutine(JumpCooldown(randJumpCoolDown(), canJump));

        // if (other.gameObject.CompareTag("Player"))
        // {
        //     base.DealDamage(10, false, true);
        //     Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
        //     //  Debug.Log(knockbackDirection);
        //     StartCoroutine(KnockbackCoroutine(knockbackDirection));
        // }
    }

    void Jump()
    {
        Debug.Log("pulando");
        //  Debug.Log("items to drop " + Item.itemsToDrop[0]);
        rb.velocity = new Vector2(rb.velocity.y, jumpForce);
        canJump = false;
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

    IEnumerator JumpCooldown(float timer, bool canJump)
    {
        if (!canJump)
        {
            yield break;
        }

        yield return new WaitForSecondsRealtime(timer);
        Jump();
        Move();
    }
}
