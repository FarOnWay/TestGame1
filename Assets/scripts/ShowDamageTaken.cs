using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamageTaken : MonoBehaviour
{
    public Text damageText;
    public Transform TargetPos;
    public Vector3 offset;
    private Vector3 originalPosition;
    private Vector3 screenPosition;

    void Start()
    {
        originalPosition = damageText.rectTransform.localPosition;
    }

    public void ShowDamageTakenOnScreen(string damage, bool isCritical)
    {
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
        StartCoroutine(ClearDamage(2f));
        StartCoroutine(ShakeText()); // Start the shake animation
    }

    private IEnumerator ClearDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        damageText.text = "";
    }

    private IEnumerator ShakeText()
    {
        float elapsed = 0.0f;
        float duration = 0.5f;
        float magnitude = 5f;
        float shakeFrequency = 0.1f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            damageText.rectTransform.position = screenPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(shakeFrequency);
        }
        damageText.rectTransform.position = screenPosition;
    }
}
