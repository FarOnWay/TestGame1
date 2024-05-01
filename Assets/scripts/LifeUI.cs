using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeUI : MonoBehaviour
{
    public GameObject player;
    public Text lifeText;

    LifeManager manager;

    void Start()
    {
        manager = player.GetComponent<LifeManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // too bad optimized. This should be called when i take damage or heal, not every frame
        lifeText.text = manager.lifeCount.ToString();

    }
}
