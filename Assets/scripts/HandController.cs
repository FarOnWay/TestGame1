using UnityEngine;

public class HandController : MonoBehaviour
{
    public SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public Item equippedItem;

    void setSprite()
    {
        if (equippedItem == null) sprite.sprite = null;

        else sprite.sprite = equippedItem.icon;
    }

    void Update()
    {
        setSprite();
        float inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }


    }



}
