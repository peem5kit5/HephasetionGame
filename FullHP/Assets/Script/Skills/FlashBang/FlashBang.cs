using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : MonoBehaviour
{
    public float TimeCount;
    public float radius;
    public float duration;
    public float decrement;
    public GameObject FlashEffect;
    bool sounded;
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

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D collider in colliders)
            {

                Player player = collider.gameObject.GetComponent<Player>();
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                Nemesis nemesis = collider.gameObject.GetComponent<Nemesis>();
                if (player != null)
                {

                    DeathEffectInteract.Instance.StunnedEffect(duration);
                    player.Stun(duration);

                }
                if (enemy != null)
                {
                    enemy.Stunning(duration);
                }
                if (nemesis != null)
                {
                    nemesis.Stunning(duration);
                }

            }
            if (!sounded)
            {
                SoundManager.Instance.FlashingSound();
                sounded = true;
            }
            Instantiate(FlashEffect, gameObject.transform.position, Quaternion.identity);
            //gun.isHavingBomb = false;
            Destroy(gameObject);
        }
    }
    public enum FlashBangType
    {
        FlashBangEagle,
        FlashBangPlayer,
        FlashBangEnemy,
    }
    public FlashBangType type;
    void CheckFlash(Collider2D col)
    {
        switch (type)
        {
            case FlashBangType.FlashBangEagle:
                if (col.CompareTag("Player"))
                {
                    TimeCount = 0;
                    if (!sounded)
                    {
                        SoundManager.Instance.FlashingSound();
                        sounded = true;
                    }
                    
                }
                break;
            case FlashBangType.FlashBangEnemy:
                if (col.CompareTag("Player"))
                {
                    TimeCount = 0;
                    if (!sounded)
                    {
                        SoundManager.Instance.FlashingSound();
                        sounded = true;
                    }
                }
                break;
            case FlashBangType.FlashBangPlayer:
                if (col.CompareTag("Enemy"))
                {
                    if (!sounded)
                    {
                        TimeCount = 0;
                        SoundManager.Instance.FlashingSound();
                        sounded = true;
                    }
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckFlash(collision);
        }
        else if (collision.CompareTag("Enemy"))
        {
            CheckFlash(collision);
        }
    }
}
