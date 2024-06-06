using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPos;
    public float speed;
    public float stoppingDistance;
    public float heightOffset;

    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        // Debug.Log("Moving towards player");
        Vector3 targetPos = playerPos.position + new Vector3(0, heightOffset, -1);
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        if (distance > 15)
        {
            transform.position = targetPos;
        }
    }
}