using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 10f;
    public float damage;
    public GameObject boom;
    public float radius;
    public float followTime = 2f;

    private bool isFollowingPlayer = true;
    float followTimer = 0;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 playerPos = player.transform.position;
        Vector3 rotation = transform.position - playerPos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowingPlayer)
        {
            if(followTimer < followTime)
            {
                float t = followTimer / followTime;
                transform.position = Vector2.Lerp(transform.position, player.position, t);
                followTime += Time.deltaTime;
            }
        }
        else
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(boom, gameObject.transform.position, Quaternion.identity);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D collider in colliders)
            {
                PlayerHP playerHP = collider.gameObject.GetComponent<PlayerHP>();

                if (playerHP != null)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);

                    // Calculate the amount of damage to deal based on the distance
                    float damageMultiplier = 1f - Mathf.Clamp01(distance / radius);
                    float damageToDeal = damage * damageMultiplier;

                    playerHP.TakeDamage(damageToDeal);

                }
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Instantiate(boom, gameObject.transform.position, Quaternion.identity);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D collider in colliders)
            {
                PlayerHP playerHP = collider.gameObject.GetComponent<PlayerHP>();

                if (playerHP != null)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);

                    // Calculate the amount of damage to deal based on the distance
                    float damageMultiplier = 1f - Mathf.Clamp01(distance / radius);
                    float damageToDeal = damage * damageMultiplier;

                    playerHP.TakeDamage(damageToDeal);

                }
            }
            Destroy(gameObject);
        }
    }
}
