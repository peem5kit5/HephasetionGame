using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float recoilForce = 10f;
    public float spreadAngle = 2f;
   // public float bulletSpeed = 10f;
    public float damage;


    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 playerPos = player.transform.position;
        Vector3 direction = playerPos - transform.position;
        Vector3 rotation = transform.position - playerPos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        float angle = Random.Range(-spreadAngle, spreadAngle);
       // Vector2 directionspread = Quaternion.Euler(0, 0, angle) * transform.up;



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
