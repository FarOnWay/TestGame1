using UnityEngine;

public class HandController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Item equippedItem;

    Transform positionWhenLookingLeft;  //looking right: x - 52.03 y - 1.72 
                                        //looking left: x - 0.139 y - 0.354

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        positionWhenLookingLeft = GetComponent<Transform>();
    }

    void setSprite()
    {
        if (equippedItem == null) sprite.sprite = null;

        else sprite.sprite = equippedItem.icon;
    }

    void Update()
    {
        positionWhenLookingLeft.position = new Vector3(10f, 10f, 0);
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
