using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamageTaken : MonoBehaviour
{
    public Text damageText;
    public Transform TargetPos;
    public Vector3 offset;
    Animator animator;
    private Vector3 originalPosition;
    private Vector3 screenPosition;
    private bool isAnimationPlaying = false;

    void Start()
    {
        // Save the original position
        originalPosition = damageText.rectTransform.localPosition;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Handle any updates here if needed
    }

    public void ShowDamageTakenOnScreen(string damage, bool isCritical)
    {
        // Convert the world position of the target to a screen position
        screenPosition = Camera.main.WorldToScreenPoint(TargetPos.position + offset);
        damageText.rectTransform.position = screenPosition;

        if (isCritical)
        {
            damage = "<color=red>" + damage + "</color>";
        }
        else
        {
            damage = "<color=yellow>" + damage + "</color>";
        }

        damageText.text = damage;
        if (animator != null && !isAnimationPlaying)
        {
            // Set the boolean parameter to trigger the animation
            animator.SetBool("isBeeingAttacked", true);
            isAnimationPlaying = true;
        }

        StartCoroutine(ResetDamageAnimation(0.0f));


        StartCoroutine(ClearDamage(2f));
        StartCoroutine(ShakeText()); // Start the shake animation
    }

    private IEnumerator ClearDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        damageText.text = "";
    }
    private IEnumerator ResetDamageAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isBeeingAttacked", false);
        isAnimationPlaying = false;
    }

    private IEnumerator ShakeText()
    {
        float elapsed = 0.0f;
        float duration = 0.5f; // Duration of the shake effect
        float magnitude = 5f; // Magnitude of the shake effect
        float shakeFrequency = 0.1f; // Frequency of the shake updates, increased to slow down the shake speed

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply the shake effect to the original screen position
            damageText.rectTransform.position = screenPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(shakeFrequency); // Control the shake update frequency
        }

        // Reset to original position (in screen space)
        damageText.rectTransform.position = screenPosition;
    }
}
