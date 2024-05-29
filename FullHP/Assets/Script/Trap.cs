using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject boomEffect;
    SpriteRenderer sprite;
    public float CountDown;
    public float MaxCountDown;
    bool objectinRange;
    CameraShake cam;
    // Start is called before the first frame update
    void Start()
    {
        objectinRange = false;
        CountDown = MaxCountDown;
        sprite = GetComponentInChildren<SpriteRenderer>();
        cam = FindObjectOfType<CameraShake>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectinRange = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (objectinRange)
        {
            sprite.color = Color.red;
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
            CountDown -= Time.deltaTime * 2;
        }
        if(CountDown <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);
            cam.ShakePistol(0.01f, 0.05f);
            Instantiate(boomEffect, gameObject.transform.position, Quaternion.identity);
            foreach (Collider2D collider in colliders)
            {
                PlayerHP playerHP = collider.gameObject.GetComponent<PlayerHP>();
                EnemyHP enemyHP = collider.gameObject.GetComponent<EnemyHP>();
                if (playerHP != null)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);

                    // Calculate the amount of damage to deal based on the distance
                    float damageMultiplier = 1f - Mathf.Clamp01(distance / 5);
                    float damageToDeal = 100 * damageMultiplier;

                    playerHP.TakeDamage(damageToDeal);

                }
                else if(enemyHP != null)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);

                    // Calculate the amount of damage to deal based on the distance
                    float damageMultiplier = 1f - Mathf.Clamp01(distance / 5);
                    float damageToDeal = 100 * damageMultiplier;

                    enemyHP.TakeDamage(5,damageToDeal,transform.position);
                }
                
            }
            Destroy(gameObject);
        }
    }
}
