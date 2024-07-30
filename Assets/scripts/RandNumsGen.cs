using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandNumsGen : MonoBehaviour
{
    public static int Rand(int min, int max)
    {
        return Random.Range(min, max);
    }

}
