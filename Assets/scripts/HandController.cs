using UnityEngine;
using System.Collections;

public class HandController : Entity
{
    public SpriteRenderer sprite;
    public Transform player;
    public Item equippedItem;
    public float rotationSpeed = 0;
    public Collider2D itemHitbox;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //  itemHitbox = GetComponent<Collider2D>();
    }


    // then call it in the player's script to check if the enemy is hit
    // if so, calls the dealDamage method +
    public bool isTouchingEnemy()
    {
        if (itemHitbox != null)
        {
            // Get the bounds of the itemHitbox
            Bounds itemBounds = itemHitbox.bounds;

            // Get all colliders overlapping with the itemHitbox
            Collider2D[] hits = Physics2D.OverlapBoxAll(itemBounds.center, itemBounds.size, 0);

            // Check if any of the overlapping colliders have the "Enemy" tag
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Debug.Log("aiaiai beebe toma ai pode escolher");
                    Debug.Log("Item is touching enemy");
                    return true;
                }
            }
        }
        return false;
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
                        Debug.Log("Attack speed: " + attackSpeed);
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

    void Update()
    {
        if (equippedItem != null && equippedItem.itemPrefab != null)
        {
            itemHitbox = equippedItem.itemPrefab.GetComponent<BoxCollider2D>();
        }
        else itemHitbox = null;

        isTouchingEnemy();
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

        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            StartCoroutine(Attack());
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
}