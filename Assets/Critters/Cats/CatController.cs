using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1.0f;
    Animator anim;
    public string Name; // name of the cat to be displayed 
    public Behaviors CurrentBehavior { get; private set; }
    public float fleeRange = 5f; // the range at which the cat starts to run away from the player

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
        Play, // plays with another cats, bugs or the NPCS (NPCS and critter, generally)
        Hunt, // hunts bugs and other critters, such as mices, birgs, etc, for fun or for food
        Groom, // grooms itself or another cat
        Follow, // follows the player (sometimes)
        Sit, // sits randomly
    }

    void setBehavior()
    {
        CurrentBehavior = (Behaviors)UnityEngine.Random.Range(0, 9);
        Debug.Log("Current Behavior: " + CurrentBehavior);
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
                Sleep();
                break;
            case Behaviors.Eat:
                Eat();
                break;
            case Behaviors.Play:
                Play();
                break;
            case Behaviors.Hunt:
                Hunt();
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
        }
    }
    #endregion

    IEnumerator Timer()
    {
        while (true)
        {
            setBehavior();
            yield return new WaitForSeconds(5);
        }
    }

    void Start()
    {
        StartCoroutine(Timer());
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        //  Debug.Log("test " + CurrentBehavior);
        Flee();
    }



    void Idle()
    {
        // does nothing
        CurrentBehavior = Behaviors.Idle;
    }

    void Wander()
    {
        // move in a random direction
        // if the cat hits a wall, turn around
        CurrentBehavior = Behaviors.Wander;
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }


    void Flee()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, fleeRange);

        foreach (Collider2D collider in collidersInRange)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                CurrentBehavior = Behaviors.Flee;

                Vector2 fleeDirection = transform.position - collider.transform.position;

                rb.velocity = fleeDirection.normalized * speed;
            }
        }
    }
    void Play()
    {
        // plays
      //  
    }

    void Sleep()
    {
        // sleeps
        /*
          when a cat starts to sleep, we dont want that another behavior be called, we want the cat to sleep
          so, we need to stop the behavior change and set up a random timer for the cat to sleep
          example: we can generate betwen 1 and 5 minutes for the cat to sleep
        */
      //  
    }

    void Eat()
    {
        // eats
        
    }

    void Hunt()
    {
        // hunts
        
    }

    void Follow()
    {
        // follows the player
        
    }

    void Sit()
    {
        // sits
        
    }

    void Groom()
    {
        // grooms
        
    }
}
