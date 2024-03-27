using System.Collections;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{

    public int coinAmount;
    public GameObject coinPrefab; 


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            coinAmount++;
            Destroy(other);
        }
    }

   public void DropsCoins(int coinAmount)
    {
        for (int i = 0; i < coinAmount; i++)
        {
            Instantiate(coinPrefab, transform.position + Random.insideUnitSphere * 2f, Quaternion.identity);
        }
    }

}
