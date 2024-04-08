using UnityEngine;

public class FlyingEnemyController : EnemyController
{

    //Default script for EVERY ENEMY THAT FLIES
    public float moveSpeed = 1f;
    public int damage;
    public float minY = -1f;
    public float maxY = 1.5f;


    // void Start()
    // {
    //     player = hero.GetComponent<Transform>();
    // }


    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position + new Vector3(0, 1), moveSpeed * Time.deltaTime);

    }
}
