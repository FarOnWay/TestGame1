using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    public int jumpForce = 0;
    public int jumpCoodown = 0;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine("JumpCooldown", 2f);
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


    // fucking shit need to be overrided. Why? i dont know, unisty is a fucking shit
    //nothing works in this terrible engine
    // i mean it
    override public void Move()
    {
        if (transform.position.x < playerTransform.position.x)
        {
            Debug.Log("a");
            rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
            // transform.position += new Vector3(2, 0);
        }
        else
        {
            Debug.Log("b");
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
