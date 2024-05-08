using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMessages : MonoBehaviour
{
    public static InGameMessages Instance { get; private set; } // singleton 
    public Text gameMessages;

    void Awake()
    {
        Instance = this;
        gameMessages = GetComponent<Text>();
    }

    public void PrintMessage(string message, Color color)
    {
        gameMessages.text = message;
        gameMessages.color = color;

        StartCoroutine(ClearMessage(5f));
    }

    private IEnumerator ClearMessage(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameMessages.text = "";
    }
}