using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : Entity
{

    // general script for every NPC
    public float moveSpeed = 2f;
    public float minX, maxX, minY, maxY;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 targetPosition;
    public Image DialogBox;
    private bool isDialoging = false;
    public HeroKnight player;
    private SpriteRenderer spriteRenderer;
    Text dialogText;
    Text dialogButton;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogText = DialogBox.GetComponentInChildren<Text>();
       // dialogButton = DialogBox.GetComponentInChildren<Button>();
     //  dialogButton =  dialogButton.GetComponentInChildren<Text>();
       dialogButton = DialogBox.GetComponentInChildren<Button>().GetComponentInChildren<Text>();

        // Walk();
    }

    void Walk()
    {
        // transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(10, 5), 10f);  
        if (isDialoging == true)
        {
            //  Debug.Log("era pra parar");
            Idle();
            return;
        }

        rb.velocity = new Vector2(moveSpeed, 0);
        // anim.SetFloat("Speed", moveSpeed);
        //anim.
    }

    void TalkToAnotherNpc()
    {

    }

    void Inventory()
    {

    }

    void Idle()
    {
        rb.velocity = new Vector2(0, 0);
        // set anim idle
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteract();
        Walk();
    }

    // if player interacts with NPC
    void PlayerInteract()
    {
        if (Input.GetMouseButtonDown(1) && isDialoging == false)
        {
            Debug.Log("A");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }

                else
                {
                    spriteRenderer.flipX = false;
                }
                Debug.Log("Right-clicked on " + gameObject.name);
                DialogBox.enabled = true;
              //  DialogBox.GetComponentInChildren<Text>().text = "Hello, I'm " + gameObject.name + ". How can I help you?";
                dialogText.enabled = true;
                dialogButton.enabled = true;
                isDialoging = true;
                return;
            }
        }

        else
        {
            //  Walk();
            Debug.Log("andando addsds");
        }


        if (Input.GetMouseButtonDown(1) && isDialoging == true)
        {
            Debug.Log("B");
            DialogBox.enabled = false;
            dialogText.enabled = false;
            dialogButton.enabled = false;
            isDialoging = false;


            return;
        }

        Debug.Log("C");
    }
}
