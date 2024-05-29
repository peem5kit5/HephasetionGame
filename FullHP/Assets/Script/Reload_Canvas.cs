using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload_Canvas : MonoBehaviour
{
    public float reloadTime;
    private float reloadTimer = 0.0f;
    public Slider slider;
   

    
    // Update is called once per frame
    void Update()
    {
        reloadTimer -= Time.deltaTime;
        slider.value = reloadTimer / reloadTime;
        
        if (reloadTimer <= 0)
        {
            slider.gameObject.SetActive(false);
        }
    }

    public void Reloading()
    {
        slider.gameObject.SetActive(true);
        reloadTimer = reloadTime;
    }

    public float SetReload(float reload)
    {
        slider.maxValue = reload / reloadTime;
        reloadTime = reload;
        return reloadTime;
    }
}
