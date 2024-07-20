using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : Entity
{
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
    private Image speechBubble;
    private Transform position;

    public int? willingToChat = null;
    private int direction; // 0 = left, 1 = right
    public float directionChangeInterval = 4f;
    public const int ATTACK_DAMAGE = 50;
    private float attackInterval = 3f;
    private float nextAttackTime = 0f;

    public override void Start()
    {
        StartCoroutine(ChangeDirection());
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogText = DialogBox.GetComponentInChildren<Text>();
        dialogButton = DialogBox.GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        position = GetComponent<Transform>();
        Transform childTransform = DialogBox.transform.Find("SpeechBubble");
        childTransform = DialogBox.GetComponentInChildren<Transform>().Find("SpeechBubble");

        if (childTransform != null)
        {
            speechBubble = childTransform.GetComponent<Image>();
        }

        closeBtn = DialogBox.GetComponentInChildren<Button>();
        closeBtn.onClick.AddListener(() =>
         {
             DialogBox.enabled = false;
             dialogText.enabled = false;
             dialogButton.enabled = false;
             speechBubble.enabled = false;
             isDialoging = false;
         });
    }

    void Update()
    {
        PlayerInteract();
        Walk();
        DetectNearbyNPCs();
        Attack();
    }

    public virtual void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, 2f);

            foreach (Collider2D collider in collidersInRange)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    anim.SetTrigger("Attack"); // Trigger attack animation

                    if (collider.gameObject.transform.position.x < transform.position.x)
                    {
                        spriteRenderer.flipX = true;
                        base.DealDamage(20, true, true);

                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                        base.DealDamage(20, true, false);
                    }
                }
            }

            nextAttackTime = Time.time + attackInterval;
        }
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
                anim.SetBool("Run", true); // Trigger run animation
                break;

            case 1: // right
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                spriteRenderer.flipX = false;
                anim.SetBool("Run", true); // Trigger run animation
                break;

            case 2: // stopped
                Idle();
                anim.SetBool("Run", false); // Trigger run animation
                anim.SetBool("Idle", true); // Trigger run animation
                break;

            default:
                break;
        }
    }

    private void TalkToAnotherNpc(GameObject otherNPC)
    {
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
                        Idle();
                        yield return new WaitForSecondsRealtime(10);
                        speechBubble.enabled = false;
                        break;

                    case > 5: // wants to chat for a while, 5s
                        Idle();
                        yield return new WaitForSecondsRealtime(5);
                        speechBubble.enabled = false;
                        break;

                    case > 3: // don't want to chat
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
                TalkToAnotherNpc(hitCollider.gameObject);
            }
        }
    }

    private void Idle()
    {
        rb.velocity = new Vector2(0, 0);
        anim.SetBool("Run", false); // Trigger idle animation
    }

    private void setSpeechBubble()
    {
        speechBubble.rectTransform.position = position.position + new Vector3(0, 1.5f, 0);
        Debug.Log(speechBubble.rectTransform.position);
        Debug.Log(position.position);
    }

    private void PlayerInteract()
    {
        if (Input.GetMouseButtonDown(1) && isDialoging == false &&
         Vector2.Distance(player.transform.position, transform.position) < 4)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                spriteRenderer.flipX = player.transform.position.x < transform.position.x;
                DialogBox.enabled = true;
                dialogText.enabled = true;
                dialogButton.enabled = true;
                setSpeechBubble();
                speechBubble.enabled = true;
                isDialoging = true;
                Idle(); // Ensure the NPC is idling while interacting
                return;
            }
        }
        else if (Input.GetMouseButtonDown(1) && isDialoging)
        {
            DialogBox.enabled = false;
            dialogText.enabled = false;
            dialogButton.enabled = false;
            speechBubble.enabled = false;
            isDialoging = false;
        }
    }
}
