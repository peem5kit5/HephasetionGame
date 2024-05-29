using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ember : MonoBehaviour
{
    public float TimeCount;
    public float radius;
    public GameObject FireDamageEffect;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
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
        TimeCount -= Time.deltaTime;
        float rotationAmount = 20 * Time.deltaTime;
        float newRotation = rb.rotation - rotationAmount;

        rb.rotation = newRotation;
        //rb.velocity *= decrement;
        if (TimeCount <= 0)
        {

            SoundManager.Instance.MolotovingSound();
            Instantiate(FireDamageEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            //gun.isHavingBomb = false;
            // Destroy(gameObject);
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {

            if (collider.CompareTag("Enemy"))
            {
                SoundManager.Instance.MolotovingSound();
                Instantiate(FireDamageEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else if (collider.CompareTag("Wall"))
            {
                SoundManager.Instance.MolotovingSound();
                Instantiate(FireDamageEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }

        }
    }
}
