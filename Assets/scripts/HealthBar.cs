using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    float timer = 0;
    bool _HudOn = true;
    [SerializeField] private Camera camera;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float currentLife, float maxLife)
    {
        // Debug.Log(currentLife);
        // Debug.Log(maxLife);
        //  Debug.Log("atualizando barra de vida");
       // Debug.Log(slider.value);

        slider.value = currentLife / maxLife;
        timer = 0;
    }

    void Update()
    {
        switchHud(timer <= 5);

        timer += Time.deltaTime;
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }

    void switchHud(bool enable)
    {
        if (enable == _HudOn) return;

        _HudOn = enable;
        var alo = GetComponentsInChildren<Image>();
        foreach (var image in alo)
        {
            image.enabled = enable;
        }
    }
}
