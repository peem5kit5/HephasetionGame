using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float maxHealth = 3;
    public float currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        //Debug.Log("Ouch");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Healing(float heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
        //Debug.Log("Ouch");
       
    }

    void Die()
    {
        // TODO: Handle player death
    }
}
