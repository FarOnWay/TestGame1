using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : Entity
{
    // general script for every NPC
    public float moveSpeed = 2f;
    public float minX, maxX, minY, maxY;
    private new Rigidbody2D rb;
    private Animator anim;
    private Vector2 targetPosition;
    public Image DialogBox;
    private bool isDialoging = false;
    public HeroKnight player;
    private SpriteRenderer spriteRenderer;
    private Text dialogText;
    private Text dialogButton;
    private Button closeBtn;
    private readonly Image border;
    private readonly GameObject otherNPC;
    private Image speechBubble;
    private Transform position;

    // this defines if a NPC wants to chat with another NPC when one approach the other
    public int? willingToChat = null;
    private int direction; // 0 = left, 1 = right
    public float directionChangeInterval = 4f;


    public override void Start()
    {
        StartCoroutine(ChangeDirection());
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogText = DialogBox.GetComponentInChildren<Text>();
        // dialogButton = DialogBox.GetComponentInChildren<Button>();
        //  dialogButton =  dialogButton.GetComponentInChildren<Text>();
        dialogButton = DialogBox.GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        position = GetComponent<Transform>();
        Transform childTransform = DialogBox.transform.Find("SpeechBubble");
        childTransform = DialogBox.GetComponentInChildren<Transform>().Find("SpeechBubble");

        if (childTransform != null)
        {
            Debug.Log($"Found SpeechBubble in {gameObject.name}");
            speechBubble = childTransform.GetComponent<Image>();
        }

        else
        {
            Debug.Log("did not found speechj");
        }

        //   border = DialogBox.GetComponentInChildren<Image>();
        closeBtn = DialogBox.GetComponentInChildren<Button>();
        closeBtn.onClick.AddListener(() =>
         {
             DialogBox.enabled = false;
             dialogText.enabled = false;
             dialogButton.enabled = false;
             speechBubble.enabled = false;
             //     border.enabled = false;
             isDialoging = false;
         });

        // Walk();
    }

    void Update()
    {
        PlayerInteract();
        Walk();
        DetectNearbyNPCs();

    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(directionChangeInterval);
            direction = Random.Range(0, 3); // 0 = left, 1 = right, 2 = stopped

            rb.velocity = new Vector2(0, rb.velocity.y);

            // wait for the delay before starting to walk again
            yield return new WaitForSeconds(5);
        }
    }

   protected void Walk()
    {
        if (isDialoging)
        {
            Idle();
            return;
        }

        switch (direction)
        {
            case 0: // left
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                spriteRenderer.flipX = true;
                break;

            case 1: // right
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                spriteRenderer.flipX = false;
                break;

            case 2: // stopped
                Idle();
                break;

            default:
                break;
        }
    }

    private void TalkToAnotherNpc(GameObject otherNPC)
    {
        // talks to another NPC if willing to chat
        willingToChat = Random.Range(0, 10);
        StartCoroutine(Chat());
        spriteRenderer.flipX = otherNPC.transform.position.x < transform.position.x;

        IEnumerator Chat()
        {
            while (true)
            {
                switch (willingToChat)
                {
                    case > 7: // wants to chat longer, 10s
                        Debug.Log("I'm willing to chat");
                        //  speechBubble.enabled = true;
                        Idle();
                        yield return new WaitForSecondsRealtime(10);
                        speechBubble.enabled = false;
                        break;

                    case > 5: // wants to chat for a while, 5s
                        Debug.Log("I want to chat for a while");
                        // speechBubble.enabled = true;
                        Idle();
                        yield return new WaitForSecondsRealtime(5);
                        speechBubble.enabled = false;
                        break;

                    case > 3: // dont want to chat
                        Debug.Log("I don't want to chat");
                        speechBubble.enabled = false;
                        Idle();
                        yield return new WaitForSecondsRealtime(3);
                        break;

                    default:
                        yield return null;
                        break;
                }
            }
        }
    }

    private void DetectNearbyNPCs()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != gameObject && hitCollider.CompareTag("NPC"))
            {
                // Debug.Log("Detected NPC: " + hitCollider.gameObject.name);
                TalkToAnotherNpc(hitCollider.gameObject);
            }
        }
    }

    private void Idle()
    {
        rb.velocity = new Vector2(0, 0);
        // set anim idle
    }

    private void setSpeechBubble()
    {
        speechBubble.rectTransform.position = position.position + new Vector3(0, 1.5f, 0);
        Debug.Log(speechBubble.rectTransform.position);
        Debug.Log(position.position);
    }

    // if player interacts with NPC
    private void PlayerInteract()
    {
        if (Input.GetMouseButtonDown(1) && isDialoging == false &&
         Vector2.Distance(player.transform.position, transform.position) < 4) // 4 is the distance to interact with NPC
        {
            Debug.Log("A");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                spriteRenderer.flipX = player.transform.position.x < transform.position.x;
                Debug.Log("Right-clicked on " + gameObject.name);
                DialogBox.enabled = true;
                dialogText.enabled = true;
                dialogButton.enabled = true;
                setSpeechBubble();
                //  speechBubble.rectTransform.position = position.position;
                speechBubble.enabled = true;
                //   border.enabled = true;
                isDialoging = true;
                return;
            }
        }

        else
        {
            //  Walk();
            Debug.Log("andando addsds");
        }

        if (Input.GetMouseButtonDown(1) && isDialoging)
        {
            Debug.Log("B");
            DialogBox.enabled = false;
            dialogText.enabled = false;
            dialogButton.enabled = false;
            //  border.enabled = false;
            isDialoging = false;

            return;
        }
    }
}
