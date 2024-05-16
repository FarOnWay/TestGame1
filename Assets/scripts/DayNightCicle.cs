using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // duration of the cycle in minutes, means that a full cycle (day/night)will take 24 minutes
    public float cycleDuration = 24f; 
    public Color dawnColor; // color at dawn
    public Color dayColor; // color during the day
    public Color duskColor; // color at dusk
    public Color nightColor; // color during the night
    public GameObject moon; 
    public bool isDay = true;
    private float currentTime; 

    void Start()
    {
        // the game starts at 7am
        currentTime = cycleDuration * (7f / 24f);
    }

    void Update()
    {
        // increment the current time
        currentTime += Time.deltaTime / 60f; 
        if (currentTime > cycleDuration) currentTime -= cycleDuration; 
        // calculate the fraction of the cycle that has passed
        float fraction = currentTime / cycleDuration;
        // determine the current phase of the cycle (dawn, day, dusk, or night)
        Color color;
        if (fraction < 5f / 24f || fraction >= 21f / 24f)
        {
            // Night
            color = nightColor;
            if (!moon.activeInHierarchy)
            {
                moon.SetActive(true);
                moon.GetComponent<MoonScript>().RiseMoon();
                // MoonScript.activeCount++;
                Debug.Log("A lua apareceu: " + MoonScript.activeCount);
            }
        }
        else if (fraction < 7f / 24f)
        {
            // Dawn
            // Debug.Log("Its day, nigga");
            isDay = true;
            color = Color.Lerp(nightColor, dawnColor, (fraction - 5f / 24f) / (2f / 24f));
        }
        else if (fraction < 17f / 24f)
        {
            // Day
            moon.SetActive(false);
            color = Color.Lerp(dawnColor, dayColor, (fraction - 7f / 24f) / (10f / 24f));
        }
        else
        {
            // Dusk
            // Debug.Log("Its night, nigga");
            isDay = false;
            color = Color.Lerp(dayColor, duskColor, (fraction - 17f / 24f) / (4f / 24f));
        }
        // light
        RenderSettings.ambientLight = color;
        // display the current time
        int hours = (int)(currentTime / cycleDuration * 24f);
        int minutes = (int)(currentTime / cycleDuration * 1440f) % 60;
        //  Debug.Log(string.Format("{0:D2}:{1:D2}", hours, minutes));
    }
}