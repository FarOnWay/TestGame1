using UnityEngine;
using System.Collections;

public class HandController : Entity
{
    public SpriteRenderer sprite;
    public Transform player;
    public Item equippedItem;
    public float rotationSpeed = 0;
    public Collider2D itemHitbox;
    public Collider2D self_Collider;
    public bool isHittingEnemy = false;
    public InGameMessages inGameMessages;
    int counter = 0;

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
            Debug.Log("HITBOX DO ITEM  " + itemHitbox);
            Debug.Log("self_Collider  " + self_Collider);
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

        if (Input.GetButton("Fire1") && !isAttacking)
        {
            StartCoroutine(Attack());
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
        if (equippedItem == null)
            sprite.sprite = null;
        else if (isAttacking == true)
        {
            sprite.sprite = equippedItem.icon;
        }
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
                        float attackSpeed = attackItem.attackSpeed;
                        rotationSpeed = attackSpeed * 360f; // times 360 to convert to degrees
                                                            // Debug.Log("Attack speed: " + attackSpeed);
                    }
                    break;

                case ItemType.Defense:
                    Debug.Log("Equipped item is a defense item.");
                    break;

                case ItemType.Material:
                    Debug.Log("Equipped item is a material.");
                    break;
            }
        }
    }
    private bool isAttacking = false;


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
}