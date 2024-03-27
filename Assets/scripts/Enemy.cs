using UnityEngine;
using System.Threading;

namespace Test
{


    public class Enemy : MonoBehaviour
    {
        public int damage;
        public HeroKnight heroKnight;
        public int health;
        public int maxHealth;
        public Transform player; // Reference to the player's transform
        public float moveSpeed = 5f; // Speed at which the enemy moves 





        // CoinSystem coinSystem;

        private Rigidbody2D rb;
        private Animator animator;
        private bool isMoving;

        public CoinManager cm;

        // Define a variable to track the time elapsed since the last attack
        private float timeSinceLastAttack = 0f;
        // Define the cooldown time for the enemy's attack (in seconds)
        public float attackCooldown = 1f;

        public GameObject coinPrefab;

        void Start()
        {

            cm.enemyCoinCount = UnityEngine.Random.Range(1, 5);


            health = maxHealth;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }



        void Update()
        {


            if (player != null)
            {

                float enemyPos = transform.position.x;
                float playerPos = player.transform.position.x;

                float distanceBetweenPlayerAndEnemy = Mathf.Abs(enemyPos - playerPos);

                // Debug.Log("Distance between player and enemy: " + distanceBetweenPlayerAndEnemy);

                if (distanceBetweenPlayerAndEnemy <= 20)
                {
                    Vector2 direction = (player.position - transform.position).normalized;

                    // Move the enemy towards the player
                    rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

                    // Set isMoving parameter based on movement
                    isMoving = direction.magnitude > 0.1f;
                    animator.SetBool("IsMoving", isMoving);
                }

                // Calculate direction towards the player

                Attack(distanceBetweenPlayerAndEnemy);

            }
        }

        // void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.gameObject.CompareTag("Coin"))
        //     {
        //         cm.coinCount++;
        //         Destroy(other.gameObject);
        //     }

        // }


        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {

                animator.SetTrigger("Death");
                Destroy(gameObject, 1.0f); // Destroy after 1 second
            }
            else
            {
                // Trigger hurt animation
                animator.SetTrigger("Hurt");
            }
        }

        private void OnDestroy()
        {
            // Check if the object has been destroyed before instantiating coins
            for (int i = 0; i < cm.enemyCoinCount; i++)
            {
                Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            }
        }

        public void Attack(float distanceBetweenPlayerAndEnemy)
        {
            timeSinceLastAttack += Time.deltaTime;

            if (timeSinceLastAttack >= attackCooldown)
            {

                if (distanceBetweenPlayerAndEnemy <= 2)
                {
                    Debug.Log("Attacking!");
                    animator.SetTrigger("Attack");

                    if (heroKnight)
                    {
                        heroKnight.TakeDamage(damage);
                    }

                    timeSinceLastAttack = 0f;
                }
            }
        }


        // public void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.gameObject.tag == "Player")
        //     {
        //         // Perform attack when colliding with player
        //         Attack();
        //     }
        // }
    }
}