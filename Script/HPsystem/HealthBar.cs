using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider sliderHp;

    

    [SerializeField]
    private float maxHP;

    public float damage;

    // Update is called once per frame
    public void SetMaxHealth(float health)
    {
        sliderHp.maxValue = health;
        sliderHp.value = health;
    }
    public void SetHealth(float health)
    {
        sliderHp.value = health;
    }
   

}
