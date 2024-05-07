using UnityEngine;

public class MoonScript : MonoBehaviour
{
    public float speed = 1.0f;
    public Sprite[] moonPhasesSprites;

    public GameObject bloodMoon;
    public static int activeCount = 0;
    public float bloodMoonChance = 0.1f;





    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }



public void RiseMoon()
{
    transform.position = new Vector3(-65, 5.2f, 1);
    activeCount++;

    // Calculate the index of the sprite to display
    int spriteIndex = activeCount % moonPhasesSprites.Length;

    // Get the SpriteRenderer component
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

    // Generate a random number between 0 and 1
    float randomNumber = Random.value;

    if (randomNumber < bloodMoonChance)
    {
        // If the random number is less than the blood moon chance, activate the blood moon
        bloodMoon.SetActive(true);
    }
    else
    {
        // Otherwise, set the sprite to the regular moon sprite and deactivate the blood moon
        spriteRenderer.sprite = moonPhasesSprites[spriteIndex];
        bloodMoon.SetActive(false);
    }
}
}