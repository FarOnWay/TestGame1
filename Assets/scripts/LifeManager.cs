using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int lifeCount;
    public int maxLife;

    void  Start()
    {
       lifeCount = maxLife;
    }

}
