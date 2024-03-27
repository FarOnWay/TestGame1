using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f; // Speed at which the enemy moves
    public float attackRange = 2f; // Range within which the enemy can attack

    private Transform player; // Reference to the player's transform

    private void Start()
    {
        // Find the player object by tag ("Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found. Make sure player object has the 'Player' tag assigned.");
        }
    }

    private void Update()
    {
        // Check if player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Attack the player
            Attack();
        }
        else
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        // Implement attack logic here
        Debug.Log("Attacking Player!");
    }
}
