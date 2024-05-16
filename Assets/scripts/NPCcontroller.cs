using System.Linq.Expressions;
using UnityEngine;

public class NPCcontroller : MonoBehaviour
{
    //generic script to move the passive npcs
    public float speed;
    private int direction = 1;

    public SpriteRenderer spriteRenderer;

    float timeWalking;

    int facingDirection;
    float timer = 0f;


    void Start()
    {
        //starts with idle animation
        timeWalking = Random.Range(5, 10);
        facingDirection = Random.Range(0, 3);

    }

    void Update()
    {
        if (timer < timeWalking)
        {
            Move(facingDirection, timeWalking);
            timer += Time.deltaTime;
            if (timer >= timeWalking)
            {
                //  Debug.Log("tempo estou");
                timeWalking = Random.Range(5, 10);
                facingDirection = Random.Range(0, 3);
                timer = 0f;
                return;

            }
        }
    }

    void Move(int facingDirection, float timeWalking)
    {
        if (this.facingDirection == 0)
        {
            transform.Translate(Vector3.left * speed * timeWalking * Time.deltaTime);
            Debug.Log("andando pra esquerda");
            spriteRenderer.flipX = true;
        }

        else if (facingDirection == 1)
        {
            transform.Translate(Vector3.right * speed * timeWalking * Time.deltaTime);
            Debug.Log("andando pra direita");
        }
        else
        {
            //idle
            Debug.Log("parado");
        }
    }
}
