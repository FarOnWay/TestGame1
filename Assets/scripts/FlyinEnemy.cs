using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float minY = -1f;
    public float maxY = 0.5f;

    public Transform player;

    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate the target position with an offset from the player's position
        Vector3 targetPosition = player.position - directionToPlayer * 3;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
