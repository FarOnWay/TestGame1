using UnityEngine;

public class DarkCrown : MonoBehaviour
{
    public float moveSpeed = 1f;

    float projectSpeed = 10f;
    public int damage;
    public float minY = -1f;
    public float maxY = 1.5f;
    public GameObject objectToInstantiate;
    public Transform player;
    public HeroKnight hero;



    public float attackSpeed = 1f;
    private float nextAttackTime = 0.0f;

    void Start()
    {
        player = hero.GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 directionToPlayer = (player.position  - transform.position).normalized;

        Vector3 targetPosition = player.position - directionToPlayer * 3;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        if (Time.time >= nextAttackTime)
        {
            Attack(damage);

            nextAttackTime = Time.time + 3;
        }
    }

    void Attack(int damage)
    {

        Vector3 directionToPlayer = (player.position + new Vector3(0, 1) - transform.position).normalized;



        GameObject instantiatedObject = Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
        instantiatedObject.GetComponent<ProjectScript>().owner = gameObject;
        instantiatedObject.GetComponent<Rigidbody2D>().velocity = directionToPlayer * projectSpeed;
    }
}
