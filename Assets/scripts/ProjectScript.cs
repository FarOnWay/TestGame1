using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectScript : MonoBehaviour
{

    public float speed = 10f;

    public GameObject owner;


    Rigidbody2D rb;



    void OnTriggerEnter2D(Collider2D other)
    {
        HeroKnight player = other.GetComponent<HeroKnight>();
        if (other.gameObject == owner) return;

        if (player != null)
        {
            player.TakeDamage(10);
        }


        Destroy(gameObject);


    }





}
