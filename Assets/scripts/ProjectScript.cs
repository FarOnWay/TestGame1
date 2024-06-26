using UnityEngine;

public class ProjectScript : MonoBehaviour
{
    public float speed;
    public GameObject owner;
    public GameObject playerGameObject;
    private HeroKnight player;
    public int projectileDamage;

    private void Start()
    {
        player = playerGameObject.GetComponent<HeroKnight>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == owner)
        {
            return;
        }

        // if (player != null)
        // {
        //     player.TakeDamage(10);
        // }

        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyController>() == null)
            {
                other.GetComponent<TargetDummy>().TakeDamage(projectileDamage);
            }
            
            else other.GetComponent<EnemyController>().TakeDamage(projectileDamage);

            Debug.Log("bóris");
            Destroy(gameObject);
            //    base.DealDamage(20, false, false);
        }
    }
}
