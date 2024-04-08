using UnityEngine;
using System.Threading;
using System.Collections;
using Unity.Jobs.LowLevel.Unsafe;
using System;

namespace Test
{


    public class Enemy : MonoBehaviour
    {
        public int damage;
        public HeroKnight heroKnight;
        public int health;
        public int maxHealth;
        public Transform player;
        public float moveSpeed = 5f;


        [SerializeField] HealthBar healthBar;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            healthBar = GetComponentInChildren<HealthBar>();
        }




        // CoinSystem coinSystem;

        private Rigidbody2D rb;
        private Animator animator;
        private bool isMoving;

        CoinManager cm;

        private float timeSinceLastAttack = 0f;
        public float attackCooldown = 1f;

        public GameObject coinPrefab;

        void Start()
        {

            cm = GetComponent<CoinManager>();

            player = heroKnight.GetComponent<Transform>();


            cm.coinCount = UnityEngine.Random.Range(1, 5);

            health = maxHealth;
            animator = GetComponent<Animator>();
            healthBar.UpdateHealthBar(health, maxHealth);

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
                    // rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

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
            healthBar.UpdateHealthBar(health, maxHealth);
            if (health <= 0)
            {

                animator.SetTrigger("Death");
                StartCoroutine ("scheduleDestroy", 1.5f);
              


            }
            else
            {
                // Trigger hurt animation
                animator.SetTrigger("Hurt");

            }
        }
        


        IEnumerator scheduleDestroy(float timer)
        {
            yield return new WaitForSecondsRealtime(timer);
             spawnCoins();

            Destroy(gameObject);
           
        }
        private void spawnCoins()
        {
            // Check if the object has been destroyed before instantiating coins
            for (int i = 0; i < cm.coinCount; i++)
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
                    Debug.Log(" enemy Attacking!");
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