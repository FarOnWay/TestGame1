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
    /// i want the cats to have several behaviors, such as:
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
    #endregion

    void setBehavior()
    {
        CurrentBehavior = (Behaviors)UnityEngine.Random.Range(0, 9);
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        throw new NotImplementedException();


    }

    void Flee()
    {
        // run away from the player
        throw new NotImplementedException();

    }

    void Play()
    {
        // plays
        throw new NotImplementedException();

    }

    void Sleep()
    {
        // sleeps
        throw new NotImplementedException();

    }

    void Eat()
    {
        // eats
        throw new NotImplementedException();

    }

    void Hunt()
    {
        // hunts
        throw new NotImplementedException();

    }

    void Follow()
    {
        // follows the player
        throw new NotImplementedException();

    }

    void Sit()
    {
        // sits
        throw new NotImplementedException();

    }

    void Groom()
    {
        // grooms
        throw new NotImplementedException();
    }

    void Update()
    {

    }
}
