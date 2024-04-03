using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;


    float timer = 0;
    bool _HudOn = true;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    public void UpdateHealthBar(float currentLife, float maxLife)
    {
        slider.value = currentLife / maxLife;
        timer = 0;
    }

    // Update is called once per frame
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
