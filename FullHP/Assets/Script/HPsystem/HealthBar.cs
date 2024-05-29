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
    private PlayerHP playerHP;
    public float damage;

    private void Awake()
    {
        playerHP = FindObjectOfType<PlayerHP>();
    }
    // Update is called once per frame
    public void SetMaxHealth(float health)
    {
        sliderHp.maxValue = health;
        sliderHp.value = health;
    }
    public void SetHealth()
    {
        StartCoroutine(SmoothHPChange());
    }
   
    IEnumerator SmoothHPChange()
    {
        float elapsedTime = 0f;
        float hpReduction = 1f;
        while(elapsedTime < hpReduction)
        {
            float newHP = Mathf.Lerp(sliderHp.value, playerHP.currentHealth, elapsedTime / hpReduction);
            sliderHp.value = newHP;
            yield return null;
            if (!DeathEffectInteract.Instance.Slowed)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

            }
            else
            {
                Time.timeScale = 0.3f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

            }
            elapsedTime += Time.deltaTime;
        }
        sliderHp.value = playerHP.currentHealth;
    }
   
}
