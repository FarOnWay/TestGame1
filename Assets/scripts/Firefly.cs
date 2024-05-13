using System.Collections;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public float speed = 1f;
    public float directionChangeInterval = 1f;
    public float maxDistanceFromPlayer = 5f;
    public Transform player;
    public GameObject lightPrefab; // Add this field

    private Vector3 direction;

    void Start()
    {
        StartCoroutine(ChangeDirection());
        StartCoroutine(ToggleLight());
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, player.position) < maxDistanceFromPlayer)
        {
            direction = (transform.position - player.position).normalized;
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    IEnumerator ToggleLight()
    {
        while (true)
        {
            lightPrefab.SetActive(!lightPrefab.activeSelf);
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
}