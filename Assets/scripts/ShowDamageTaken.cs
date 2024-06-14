using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamageTaken : MonoBehaviour
{
    public Text damageText;
    public Transform TargetPos;
    public Vector3 offset;


    void Start()
    {

    }

    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(TargetPos.position + offset);
        damageText.rectTransform.position = screenPosition;
    }

    public void ShowDamageTakenOnScreen(string damage, bool isCritical)
    {
        if (isCritical)
        {
            damage = "<color=red>" + damage + "</color>";
        }
        damage = "<color=yellow>" + damage + "</color>";

        damageText.text = damage;
        //   damageText.color = color;

        StartCoroutine(Cleardamage(2f));
    }

    private IEnumerator Cleardamage(float delay)
    {
        yield return new WaitForSeconds(delay);

        damageText.text = "";
    }
}
