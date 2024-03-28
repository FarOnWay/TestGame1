using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{

    public GameObject player;
    public Text coinText;

    CoinManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = player.GetComponent<CoinManager>();
    }

    void Update()
    {
        coinText.text = manager.coinCount.ToString();
    }

}
