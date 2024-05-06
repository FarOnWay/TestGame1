using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float cycleDuration = 24f; // Duration of the whole cycle in minutes
    public Color dawnColor; // Color at dawn
    public Color dayColor; // Color during the day
    public Color duskColor; // Color at dusk
    public Color nightColor; // Color during the night
    bool isDay = true;

    private float currentTime; // Current time in minutes

    void Start()
    {
        // Set the initial time to 7 AM
        currentTime = cycleDuration * (7f / 24f);
    }

    void Update()
    {
        // Increment the current time
        currentTime += Time.deltaTime / 60f; // Convert seconds to minutes
        if (currentTime > cycleDuration) currentTime -= cycleDuration; // Wrap time

        // Calculate the fraction of the cycle that has passed
        float fraction = currentTime / cycleDuration;

        // Determine the current phase of the cycle (dawn, day, dusk, or night)
        Color color;
        if (fraction < 5f / 24f || fraction >= 21f / 24f)
        {
            // Night
            color = nightColor;
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
            color = Color.Lerp(dawnColor, dayColor, (fraction - 7f / 24f) / (10f / 24f));
        }
        else
        {
            // Dusk
           // Debug.Log("Its night, nigga");
            isDay = false;
            color = Color.Lerp(dayColor, duskColor, (fraction - 17f / 24f) / (4f / 24f));
        }

        // Set the ambient light color
        RenderSettings.ambientLight = color;

        // Display the current time
        int hours = (int)(currentTime / cycleDuration * 24f);
        int minutes = (int)(currentTime / cycleDuration * 1440f) % 60;
        //  Debug.Log(string.Format("{0:D2}:{1:D2}", hours, minutes));
        if (isDay)
        {
         //   Debug.Log("dia");
        }
        else
        {
           // Debug.Log("noite");
        }
    }
}