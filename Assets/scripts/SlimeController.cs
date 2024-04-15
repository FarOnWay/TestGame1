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


    //overriding the move method because the slime has a diferent style of move 
    public override void Move()
    {
      
    }

   
}
