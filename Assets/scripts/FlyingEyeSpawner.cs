using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeSpawner : MonoBehaviour
{
    [SerializeField] public float spawnRate = 0.1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] public bool canSpawn = true;
    public float avoidForce = 5f;
    public LayerMask obstacleLayers;
    public DayNightCycle dayNight;

    void Start()
    {
        StartCoroutine(Spawner());
        enemyPrefab.transform.position = new Vector3(999, 999, 999);
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (true)
        {
            canSpawn = !dayNight.isDay;
            if (canSpawn)
            {
                yield return wait;
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                yield return null;
            }
        }
    }


    public void AvoidCollisionWithNonPlayer()
    {
        // Cast a ray forward from the enemy's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 1f, obstacleLayers);

        // If the ray hits an obstacle
        if (hit.collider != null)
        {
            // Make the enemy move up or down to avoid the obstacle
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Determine the direction to move based on the enemy's and the obstacle's vertical positions
                float direction = (transform.position.y < hit.transform.position.y) ? -1f : 1f;

                // Apply the avoid force
                rb.AddForce(new Vector2(0, direction * avoidForce), ForceMode2D.Impulse);
            }
        }
    }
}
