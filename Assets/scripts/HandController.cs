using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Transform player;
    public Item equippedItem;
    public float rotationSpeed = 0;
    public Collider2D itemHitbox;
    public Collider2D self_Collider;
    public bool isHittingEnemy = false;
    public InGameMessages inGameMessages;
    public bool rangedItem = false;
    public GameObject projectilePrefab;
    public float bowDelay = 1 * Time.deltaTime;
    public float projectileSpeed;
    public float shootCooldown = 1f * Time.deltaTime;
    private float lastShootTime;
    public float initialImpulse = 2f;
    int counter = 0;
    private bool isAttacking = false;
    public AudioClip shootSound;
    private AudioSource audioSource;
    private bool isShooting = false;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        self_Collider.enabled = true;
        // self_Collider = this.GetComponent<Collider2D>();
        //  itemHitbox = GetComponent<Collider2D>();
    }

    void Update()
    {
        //   Debug.Log(self_Collider + " self no começo do update");
        isTouchingEnemy();
        if (equippedItem != null && equippedItem.itemPrefab != null)
        {
            itemHitbox = equippedItem.itemPrefab.GetComponent<BoxCollider2D>();
            itemHitbox.enabled = true;
            // Debug.Log("HITBOX DO ITEM  bounds" + itemHitbox.bounds);
            // Debug.Log("self_Collider bounds" + self_Collider.bounds);
            self_Collider = itemHitbox;
        }
        // if(equippedItem  == null)
        // {
        //     self_Collider.enabled = false;
        // }

        else
        {
            itemHitbox = null;
            if (self_Collider != null)
            {
                //   self_Collider.enabled = false;
                self_Collider = null;
            }
        }
        //  Debug.Log(itemHitbox);
        //  Debug.Log(self_Collider + " self");

        setSprite();
        getInfoFromEquippedItem();

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            sprite.flipX = false;
        }

        else if (inputX < 0)
        {
            sprite.flipX = true;
        }

        if (Input.GetButton("Fire1") && !isAttacking && rangedItem == false)
        {
            StartCoroutine(Attack());
        }

        else if (Input.GetButtonDown("Fire1") && !isAttacking && rangedItem == true)
        {
            StartCoroutine(RangedAttack());
        }
    }

    // then call it in the player's script to check if the enemy is hit
    // if so, calls the dealDamage method +

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            counter++;
            inGameMessages.PrintMessage("HITTING ENEMY " + counter, Color.red);
            isHittingEnemy = true;
            // Debug.Log("hittando um inimigo nessa porra ");
            // Debug.Log("isHittingEnemy: " + isHittingEnemy);
        }
        else isHittingEnemy = false;


        // Debug.Log("AIAIA BEEBE TOMA AI PODE ESCOLHER");

    }

    void OnTriggerExit2D(Collider2D other)
    {
        counter = 0;
        //  Debug.Log("AOIUCA DZRIKUYVSIURSTHGNBDIRUYN");
        // isHittingEnemy = false;
    }
    public bool isTouchingEnemy()
    {
        //  Debug.Log("isHittingEnemy: " + isHittingEnemy);
        //  Debug.Log("isHittingEnemy: " + isHittingEnemy);
        return isHittingEnemy;
        // if (itemHitbox != null)
        // {
        //     // Calculate the world space position of the itemHitbox
        //     Vector2 itemPosition = transform.position + (Vector3)itemHitbox.offset;
        //     Vector2 itemSize = itemHitbox.bounds.size;
        //     // Debug.Log("itemPosition: " + itemPosition);
        //     // Debug.Log("itemSize: " + itemSize);

        //     // Get all colliders overlapping with the itemHitbox
        //     Collider2D[] hits = Physics2D.OverlapBoxAll(itemPosition, itemSize, 0);
        //     // Debug.Log("hits: " + hits.Length);

        //     // Check if any of the overlapping colliders have the "Enemy" tag
        //     foreach (Collider2D hit in hits)
        //     {
        //         if (hit.CompareTag("Enemy"))
        //         {
        //             Debug.Log("bota q bota");
        //             return true;
        //         }
        //     }
        // }
        // return true;
    }

    void setSprite()
    {
        if (equippedItem == null) sprite.sprite = null;

        else if (isAttacking == true) sprite.sprite = equippedItem.icon;

        else sprite.sprite = null;
    }

    void getInfoFromEquippedItem()
    {
        if (equippedItem != null)
        {
            ItemType itemType = equippedItem.itemType;

            switch (itemType)
            {
                case ItemType.Consumable:
                    Debug.Log("Equipped item is a consumable.");
                    break;

                case ItemType.Attack:
                    // cast the equipped item to an AttackItem
                    Attack.AttackItem attackItem = equippedItem as Attack.AttackItem;
                    if (attackItem != null)
                    {
                        rangedItem = false;

                        float attackSpeed = attackItem.attackSpeed;
                        rotationSpeed = attackSpeed * 360f; // times 360 to convert to degrees                                                          // Debug.Log("Attack speed: " + attackSpeed);
                    }
                    break;

                case ItemType.Defense:
                    Debug.Log("Equipped item is a defense item.");
                    break;

                case ItemType.Material:
                    Debug.Log("Equipped item is a material.");
                    break;

                case ItemType.Ranged:
                  //  Debug.Log("Equipped item is a ranged weapon.");
                    rangedItem = true;
                    break;

                default:
                    rangedItem = false;
                    equippedItem = null;
                    break;
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        Vector3 rotationDirection = sprite.flipX ? Vector3.forward : Vector3.back;

        transform.localRotation = Quaternion.Euler(0, 0, 0.4f);

        // perform a full rotation
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            transform.RotateAround(player.position, rotationDirection, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // continue rotating while the attack button is held down
        while (Input.GetButton("Fire1"))
        {
            transform.RotateAround(player.position, rotationDirection, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isAttacking = false;
    }

    IEnumerator RangedAttack()
    {
        if (isShooting || !CanShoot()) yield break;

        isShooting = true;

        // Calculate the direction to launch the projectile
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 launchDirection = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle
        float rotationAngle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;

        // Play the shoot sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Instantiate the projectile at the player's position with the calculated rotation
        Quaternion rotation = Quaternion.Euler(0, 0, rotationAngle);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);

        // Add force to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(launchDirection * projectileSpeed, ForceMode2D.Impulse);
            rb.AddForce(launchDirection * initialImpulse, ForceMode2D.Impulse);
        }

        lastShootTime = Time.time;

        yield return new WaitForSeconds(1); 

        isShooting = false;
    }

    private bool CanShoot()
    {
        return Time.time >= lastShootTime + shootCooldown;
    }
}