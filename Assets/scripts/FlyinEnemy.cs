using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;

    float projectSpeed = 10f;
    public float minY = -0.5f;
    public float maxY = 1f;
    public GameObject objectToInstantiate; // The prefab you want to instantiate
    public Transform player;

    public float attackSpeed = 1f; // Attacks per second
    private float nextAttackTime = 0.0f;

    void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate the target position with an offset from the player's position
        Vector3 targetPosition = player.position - directionToPlayer * 3;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        // Check if it's time for the next attack
        if (Time.time >= nextAttackTime)
        {
            // Perform the attack
            Attack();

            // Calculate the time for the next attack
            nextAttackTime = Time.time + 3;
        }
    }

    void Attack()
    {
        // Calculate direction towards the player at the time of instantiation
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Instantiate the object at the current position
        GameObject instantiatedObject = Instantiate(objectToInstantiate, transform.position, Quaternion.identity);

        // Move the instantiated object towards the player's direction
        instantiatedObject.GetComponent<Rigidbody2D>().velocity = directionToPlayer * projectSpeed;
    }
}
