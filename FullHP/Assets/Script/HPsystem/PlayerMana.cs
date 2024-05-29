using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    // Define the maximum mana and mana regeneration rate
    public float maxMana = 100f;
    public float manaRegenRate = 1f;
    [SerializeField]
    private Slider sliderMp;
    // Initialize the current mana to the maximum value
    public float currentMana;

    void Start()
    {
      //  currentMana = maxMana;
        StartCoroutine(ManaRegen());
        //SetMana(currentMana);
        // SetMaxMP(maxMana);
    }
    private void Update()
    {
        SetMana(currentMana);
    }

    // Subtract mana if a spell or ability is used
    public void UseMana(float manaCost)
    {
        currentMana -= manaCost;
    }
    public void SetMaxMP(float mp)
    {
        sliderMp.maxValue = mp;
        sliderMp.value = mp;
    }

    public void SetMana(float mana)
    {
        sliderMp.value = mana;
    }

    // Coroutine to regenerate mana over time
    IEnumerator ManaRegen()
    {
        while (true)
        {
            if (currentMana < maxMana)
            {
                currentMana += manaRegenRate * Time.deltaTime;
            }
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            yield return null;
        }
    }
}
