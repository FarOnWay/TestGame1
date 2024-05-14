using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] public bool canSpawn = true;

    public DayNightCycle dayNight;
    private List<GameObject> enemyInstances = new List<GameObject>();



    void Start()
    {
        StartCoroutine(Spawner());
        enemyPrefab.transform.position = new Vector3(999, 999, 999);
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        // while (true)
        //{
            canSpawn = dayNight.isDay;
            if (!canSpawn)
            {
                yield return wait;
                GameObject enemyInstance = Instantiate(enemyPrefab);
                enemyInstances.Add(enemyInstance);

            }
            // else
            // {
            //     foreach (GameObject enemyInstance in enemyInstances)
            //     {
            //         Destroy(enemyInstance);
            //     }
            //     enemyInstances.Clear();
            // }
      //  }
    }

    // destroy the enemies that spawns at night if its day
    void DestroyIfItsDay()
    {

    }
}
