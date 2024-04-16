using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{


    public int jumpForce = 0;
    public int jumpCoodown = 0;

    // Update is called once per frame
    // void Update()
    // {
    //     Debug.Log("test");
    //     Move();
    // }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("slime encostando no chao");
            StartCoroutine("JumpCooldown", 2f);
            //  Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.y, jumpForce);
        base.Move();
    }

    public override void Update()
    {
        // Debug.Log("overring update");
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    IEnumerator JumpCooldown(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        Jump();

    }
    // IEnumerator JumpCooldown()
    // {
    //     yield return new WaitForSeconds(2f);
    //     Jump();

    // }


}
