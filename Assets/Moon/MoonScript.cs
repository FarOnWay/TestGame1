using UnityEngine;

public class MoonScript : MonoBehaviour
{
    public float speed = 1.0f;
    public Sprite[] moonPhasesSprites;
    public static int activeCount = 0;
    public float bloodMoonChance = 0.1f;
    public GameObject nightColor, bloodMoon;
    public bool isBloodMoon = false;

    public Color bloodMoonColor;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }

    public void RiseMoon()
    {
        int rnd = Random.Range(1, 101);
        transform.position = new Vector3(-65, 5.2f, 1);
        activeCount++;

        if (rnd <= 90)
        {
            Debug.Log("lua sangrenta");
            RiseBloodMoon();
            //   GetComponent<SpriteRenderer>().sprite = moonPhasesSprites[8];
            return;
        }

        else
        {
            Debug.Log("lua normal");
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
        GetComponent<SpriteRenderer>().sprite = moonPhasesSprites[8];
        nightColor.GetComponent<DayNightCycle>().nightColor = bloodMoonColor;
        // bloodMoon.GetComponent<SpriteRenderer>().enabled = true;
        // Instantiate(bloodMoon, new Vector3(-65, 5.2f, 1), Quaternion.identity);
        // bloodMoon.transform.Translate(speed * Time.deltaTime * Vector3.right);

    }
}