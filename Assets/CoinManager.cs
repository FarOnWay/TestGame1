using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinManager : MonoBehaviour
{
  public Text coinText;
    public int coinCount;
    public int enemyCoinCount;

    void  Update()
    {
        coinText.text = coinCount.ToString();
    }
   
}
