using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    public int jumpForce = 0;
    public int jumpCoodown = 0;

    // public Rigidbody2D slime_rb;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Debug.Log("slime encostando no chao");
            StartCoroutine("JumpCooldown", 2f);
            // Jump();
        }
    }
    // void Move()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    //     //  Debug.Log("chamando move");
    //     // transform.position += new Vector3(1 * Time.deltaTime,0);
    // }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.y, jumpForce);
        // Move();

        // base.Move(); this wont work
        // to make the slime goes to the player's direction, firt we apply a force to its Y axxis(making it jump)
        // then we apply another force, that will make it moves(left, right)

    }

    override public void Update()
    {
        // Debug.Log("overrided update");
        //  Move();
        // Debug.Log("overring update");
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //  Move();
    }


    // fucking shit need to be overrided. Why? i dont know, unity is a fucking shit
    //nothing works in this terrible engine
    //   i mean it
    override public void Move()
    {
        //Debug.Log("chamando o move");
        // transform.position += new Vector3(2, 0);
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
        Debug.Log("chamando co rotina");
        Jump();
        Move();
    }
}
