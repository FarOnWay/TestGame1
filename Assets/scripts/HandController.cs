using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Transform player; // Reference to the player's transform
    public Item equippedItem;
    public float rotationSpeed = 0; // Speed of the rotation

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void setSprite()
    {
        if (equippedItem == null)
            sprite.sprite = null;
        else
        {
            sprite.sprite = equippedItem.icon;


        }
    }


    void getInfoFromEquippedItem()
    {
        if (equippedItem != null)
        {
            // Get the item type of the equipped item
            ItemType itemType = equippedItem.itemType;

            // Handle different item types
            switch (itemType)
            {
                case ItemType.Consumable:
                    // Do something for consumable items
                    Debug.Log("Equipped item is a consumable.");
                    break;
                case ItemType.Attack:
                    // Cast the equipped item to AttackItem and get the attack speed
                    Attack.AttackItem attackItem = equippedItem as Attack.AttackItem;
                    if (attackItem != null)
                    {
                        float attackSpeed = attackItem.attackSpeed;
                        rotationSpeed = attackSpeed * 360f;
                        Debug.Log("Attack speed: " + attackSpeed);
                    }
                    break;
                case ItemType.Defense:
                    // Do something for defense items
                    Debug.Log("Equipped item is a defense item.");
                    break;
                case ItemType.Material:
                    // Do something for material items
                    Debug.Log("Equipped item is a material.");
                    break;
            }
        }
    }
    private bool isAttacking = false;

    void Update()
    {
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

        // If the attack button is initially pressed
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;

        // Determine the rotation direction based on the player's facing direction
        Vector3 rotationDirection = sprite.flipX ? Vector3.forward : Vector3.back;

        // Set the sword's local rotation to 4.4 degrees in the z-axis
        transform.localRotation = Quaternion.Euler(0, 0, 4.4f);

        // Perform a full rotation
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            transform.RotateAround(player.position, rotationDirection, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // Continue rotating while the attack button is held down
        while (Input.GetButton("Fire1"))
        {
            transform.RotateAround(player.position, rotationDirection, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isAttacking = false;
    }
}