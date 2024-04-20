using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] public bool canSpawn = true;


    void Start()
    {
        StartCoroutine(Spawner());
        enemyPrefab.transform.position = new Vector3(999,999,999);
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            yield return wait;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
