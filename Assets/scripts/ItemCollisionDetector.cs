using UnityEngine;

public class ItemCollisionDetector : MonoBehaviour
{
    public bool isTouchingEnemy;

    void Start()
    {
      //  Debug.Log("TESTANDO ALO");
    }

    void Update()
    {
      //  Debug.Log("TESTANDO ALO");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("BANDO DE CA");
            isTouchingEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isTouchingEnemy = false;
        }
    }
}