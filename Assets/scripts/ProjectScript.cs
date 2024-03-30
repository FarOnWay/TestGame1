using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectScript : MonoBehaviour
{

    public HeroKnight player;
    public float speed = 10f;

    

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        // player = GetComponent<HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.MoveTowards(transform.position, new Vector3(-6, -1, 0), speed * Time.deltaTime);

    }
}
