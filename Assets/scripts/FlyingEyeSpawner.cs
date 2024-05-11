using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] public bool canSpawn = true;

    public DayNightCycle dayNight;


    void Start()
    {
        StartCoroutine(Spawner());
        enemyPrefab.transform.position = new Vector3(999, 999, 999);
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (true)
        {
            canSpawn = dayNight.isDay;
            if (!canSpawn)
            {
                yield return wait;
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                yield return null; // Wait for the next frame if it's not day
            }
        }
    }
}