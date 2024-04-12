using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{

    LayerMask groundLayer;

    public float fallSpeed;
    public float moveSpeed;

    // Update is called once per frame
    // void Update()
    // {
    //     Debug.Log("test");
    //     Move();
    // }


    //Overriding the move method because the slime has a diferent style of move 
    public override void Move()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }


    // void OnCollisionEnter2D(Collision2D other)
    // {

    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         fallSpeed = 0;
    //         Debug.Log("slime dando o buras");
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("slime dando o buras");
        Debug.Log(other.name);

        if (other.CompareTag("Ground"))
        {
            fallSpeed = 0;
        }

        if (other.CompareTag("Player"))
        {
            DealDamage(10, false);
        }
    }

}
