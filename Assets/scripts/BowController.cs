using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public static GameObject projectilePrefab;
    public float projectileSpeed;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void RangedAttack(GameObject projectile)
    {
        // Calculate the direction to launch the projectile
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 launchDirection = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle
        float rotationAngle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;

        // Play the shoot sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Instantiate the projectile at the player's position with the calculated rotation
        Quaternion rotation = Quaternion.Euler(0, 0, rotationAngle);
        projectile = Instantiate(projectile, transform.position, rotation);

        // Add force to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(launchDirection * projectileSpeed, ForceMode2D.Impulse);
        }
    }


    //consumes the projectile

    void usesProjectile()
    {

    }
}