using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1.0f;
    private SpriteRenderer sprite;
    private Animator anim;
    public string Name; // name of the cat to be displayed 
    [SerializeField]
    private Behaviors currentBehavior;
    public Behaviors CurrentBehavior
    {
        get { return currentBehavior; }
        set { currentBehavior = value; }
    }
    public float fleeRange = 5f; // the range at which the cat starts to run away from the player
    Transform prayPos;
    bool isHunting = false;
    public SpriteRenderer mouth;
    private bool hasLayAnimationPlayed = false;
    bool justHunted = false;
    bool sleeping = false;
    int wannaFollowOrFlee1or0 = 0;


    #region Personality

    /* i want the cats to have different personalities
        irl, cats have different personalities, some likes to play, some don't
        some are more active, some are more lazy
        some are more friendly, some are more shy
        and thats what i want for the cats in our game
        i want them to have different personalities
        and i want them to have different behaviors based on their personalities
        is this necessary? no, but it would be cool
        so, the numbers below determine the cats type of personality, either they are more active or more lazy
        or more friendly or more shy
    */

    /// <summary>
    /// how active the cat is, higher numbers means the cat walks, plays and hunts more
    /// lower values means the cat sleeps, sits and grooms more
    /// </summary>
    public float Active;

    /// <summary>
    /// how friendly the cat is, higher numbers means the cat follows the player more
    /// lower values means the cat runs away from the player more
    /// </summary>
    public float Friendly;

    /// <summary>
    /// how much the cat likes to hunt, higher numbers means the cat hunts more
    /// </summary>
    public float Hunter;

    /// <summary>
    /// how lazy the cat is, higher numbers means the cat sleeps more
    /// </summary>
    public float Lazy;

    #endregion


    #region Behaviors
    /// <summary>
    /// the actions a cat can do
    /// @Idle @Wander @Flee @Sleep @Eat @Play @Hunt @Groom @Follow @Sit
    /// </summary>
    public enum Behaviors
    {
        Idle,  // does nothing, can either be sit or stand, stopped
        Wander, // moves around randomly
        Flee, // runs away from the player (sometimes)
        Sleep, // sleeps in some places it finds comfortable
        Eat, // eats food
        Play, // plays with another cats, bugs or the NPCS (NPCS and critters, generally)
        Hunt, // hunts bugs and other critters, such as mices, birds, etc, for fun or for food
        Groom, // grooms itself or another cat
        Follow, // follows the player (sometimes)
        Sit, // sits randomly
        Lay, // lays down randomly
    }
    int n = 0;
    private void setBehavior()
    {
        CurrentBehavior = (Behaviors)3;
        // Debug.Log("Current Behavior: " + CurrentBehavior);
        switch (CurrentBehavior)
        {
            case Behaviors.Idle:
                Idle();
                break;
            case Behaviors.Wander:
                Wander();
                break;
            case Behaviors.Flee:
                Flee();
                break;
            case Behaviors.Sleep:
                // Debug.Log("before the sleep method");
                Sleep();
                // Debug.Log("after the sleep method");
                n++;
                //  Debug.Log(n);
                break;
            case Behaviors.Eat:
                Eat();
                break;
            case Behaviors.Play:
                Play();
                break;
            case Behaviors.Hunt:
                Hunt(justHunted);
                break;
            case Behaviors.Groom:
                Groom();
                break;
            case Behaviors.Follow:
                Follow();
                break;
            case Behaviors.Sit:
                Sit();
                break;
            case Behaviors.Lay:
                Lay();
                break;
            default:
                break;
        }
    }
    #endregion

    private void Start()
    {
        _ = StartCoroutine(Timer());
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        CurrentBehavior = Behaviors.Sleep;
      //  _ = StartCoroutine(WannaFollowOrFleeFromThePlayer());

        //  mouth = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Sleep();
        //  Debug.Log("test " + CurrentBehavior);
        IsPlayerNearby();
        if (IsPreyNearby())
        {
            Hunt(justHunted);
        }
    }

    /// <summary>
    ///  returns either if the cat wants to follow or flee from the player
    /// cats are impredictable, so they can either follow or flee from the anytime
    /// </summary>
    /// <returns></returns>

    private IEnumerator  WannaFollowOrFleeFromThePlayer()
    {
        while (sleeping == false)
        {

            wannaFollowOrFlee1or0 = Random.Range(0, 2);
            Debug.Log("TESTANDO ESSA BOSTA " + wannaFollowOrFlee1or0);
         //   setBehavior();
            //  Debug.Log("setting behavior" + CurrentBehavior);
            yield return new WaitForSecondsRealtime(5);
        }
    }

    void FollowOrFlee()
    {
          StartCoroutine(WannaFollowOrFleeFromThePlayer());
        //   Debug.Log("enumarator follow or flee SSDSDIH");
        if (wannaFollowOrFlee1or0 == 1)
        {
            Debug.Log("follow");
            Follow();
        }
        else
        {
            Debug.Log("flee");
            Flee();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bug"))
        {
            Debug.Log("encstando em um inseto");
            mouth.sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Timer()
    {
        while (sleeping == false)
        {
            setBehavior();
            //  Debug.Log("setting behavior" + CurrentBehavior);
            yield return new WaitForSeconds(5);
        }
    }

    private void Idle()
    {
        // does nothing
        CurrentBehavior = Behaviors.Idle;
    }

    private void Wander()
    {
        // move in a random direction
        CurrentBehavior = Behaviors.Wander;
        rb.velocity = new Vector2(speed, rb.velocity.y);
        anim.SetTrigger("Walk");
    }

    private void Lay()
    {

    }

    private void Flee()
    {

    }

    Collider2D IsPlayerNearby()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, fleeRange);

        foreach (Collider2D collider in collidersInRange)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("player nearby");
                // FollowOrFlee();
                FollowOrFlee();
                // CurrentBehavior = Behaviors.Flee;
                // anim.SetTrigger("Run");

                // Vector2 fleeDirection = transform.position - collider.transform.position;

                // rb.velocity = fleeDirection.normalized * speed;
                return collider;
            }
        }
        return null;
    }

    private void Follow()
    {
        // follows the player
        anim.SetTrigger("Walk");
    }

    private void Play()
    {
        // plays
        //  
    }

    private void Sleep()
    {
        // Debug.Log("qadjksdjn ohhh god");
        // sleeping = true;
        // anim.SetTrigger("Lay");
        // justHunted = false;
        // StartCoroutine(SleepTimer());

        // // sleeps
        // /*
        //   when a cat starts to sleep, we dont want that another behavior be called, we want the cat to sleep
        //   so, we need to stop the behavior change and set up a random timer for the cat to sleep
        //   example: we can generate betwen 1 to 5 minutes for the cat to sleep
        // */
        // //  
    }

    IEnumerator SleepTimer()
    {

        yield return new WaitForSeconds(Random.Range(60, 300));
        sleeping = false;
    }

    private void Eat()
    {
        // eats
        mouth.sprite = null;
    }

    /// <summary>
    /// checks if there is a prey nearby a cat so it can hunts
    /// </summary>
    /// <returns>
    /// @true if there is a prey 
    /// @false if there is no prey 
    /// </returns>
    private bool IsPreyNearby()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, fleeRange);

        foreach (Collider2D collider in collidersInRange)
        {
            // perharps i will change these tags into one single tag, like catPrey, something'
            if (collider.gameObject.CompareTag("Bug"))
            {
                //  Debug.Log("encontrou uma presa por perto");
                prayPos = collider.transform;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Hunts preys (bugs, mices, birds, etc)
    /// if the cat sees a prey, it will stare at it for a while, then it will jump and run towards it until it catches it
    /// the cat has 50% chance of catching the prey, in case of success, the cat will eat the prey, falling 
    /// into the ground with the prey in its mouth
    /// in case of failure, the prey will run away and the cat will return to its previous behavior
    /// </summary>

    private void Hunt(bool canHunt)
    {
        if (canHunt == true) return;
        isHunting = true;
        justHunted = true;

        if (prayPos.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        StartCoroutine(HuntSequence());
    }

    IEnumerator HuntSequence()
    {
        if (isHunting == true && hasLayAnimationPlayed == false)
        {
            hasLayAnimationPlayed = true;

            // Debug.Log("suauuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");
            anim.SetTrigger("Lay");

            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }
        }

        yield return new WaitForSecondsRealtime(5);

        if (sprite.flipX)
        {
            rb.velocity = new Vector2(-2, 0);
        }
        else
        {
            rb.velocity = new Vector2(2, 0);
        }

        anim.SetTrigger("Run");
        isHunting = false;
    }

    private void Jump()
    {
        // jumps
        //  rb.velocity = new Vector2(rb.velocity.x, speed);
        rb.AddForce(Vector2.up * (speed + 5), ForceMode2D.Impulse);
    }

    private void Sit()
    {
        // sits
        anim.SetTrigger("Sit");
    }

    private void Groom()
    {
        // grooms
    }

}