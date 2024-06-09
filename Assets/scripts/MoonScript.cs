using UnityEngine;
using UnityEngine.UI;

public class MoonScript : MonoBehaviour
{
    public float speed = 1.0f;
    public Sprite[] moonPhasesSprites;
    public static int activeCount = 0;
    public float bloodMoonChance;
    public GameObject nightColor, bloodMoon;
    public bool isBloodMoon = false;

    //    bloodMoonColor = new Color(0.5f, 0, 0);
    public Text gameMessages;
    public GameObject inGameMessages;

    public Color bloodMoonColor;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }

    public void RiseMoon()
    {
        int rnd = Random.Range(1, 101);
        transform.position = new Vector3(-65, 5.2f, 0);
        activeCount++;

        if (rnd <= bloodMoonChance)
        {
            Debug.Log("lua sangrenta");
            RiseBloodMoon();
            //   GetComponent<SpriteRenderer>().sprite = moonPhasesSprites[8];
            return;
        }

        else
        {
          //  Debug.Log("lua normal");
            int spriteIndex = activeCount % moonPhasesSprites.Length;
            if (spriteIndex == 8)
            {
                spriteIndex = 0;
            }
            GetComponent<SpriteRenderer>().sprite = moonPhasesSprites[spriteIndex];
        }
    }

    public void RiseBloodMoon()
    {
        isBloodMoon = true;
        inGameMessages.GetComponentInChildren<InGameMessages>().PrintMessage("A blood moon is rising!", Color.red);

        // if (inGameMessages != null)
        // {

        //     // gameMessages.text = "A blood moon is rising!";
        // }
        // else
        // {
        //     Debug.LogError("No Text component found on this GameObject.");
        // }

        GetComponent<SpriteRenderer>().sprite = moonPhasesSprites[8];
        nightColor.GetComponent<DayNightCycle>().nightColor = bloodMoonColor;
    }
}