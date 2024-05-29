using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    //public GameObject HurtEffect;


    CameraShake camShake;
    bool isHealing;
    void Start()
    {
        maxHealth = 100;
        isHealing = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
        camShake = FindObjectOfType<CameraShake>();
    }
    public void UsingPotion(int duration, int Heal)
    {
        if (!isHealing)
        {
            isHealing = true;
            StartCoroutine(Healing(duration, Heal));
        }
    }
    public IEnumerator Healing(int tick, int heal)
    {
        float TimeHeal = 0f;
        while(TimeHeal < tick)
        {
            Heal(heal);
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
            TimeHeal += Time.deltaTime;
            yield return null;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth();
        SoundManager.Instance.PlayerHurtSound();
        StartCoroutine(Hurt());
        //Debug.Log("Ouch");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int heal)
    {
        currentHealth += heal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth();
        //Debug.Log("Ouch");
       
    }
    IEnumerator Hurt()
    {
       // HurtEffect.SetActive(true);
        StartCoroutine(camShake.ShakePistol(0.15f, 0.1f));
        yield return new WaitForSeconds(0.5f);
    //    HurtEffect.SetActive(false);
    }
    void Die()
    {
        // TODO: Handle player death
        PlayerDeath.Instance.Death();
    }
}
