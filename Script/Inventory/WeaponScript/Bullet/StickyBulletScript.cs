using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBulletScript : MonoBehaviour
{
    public float recoilForce = 10f;
    public float spreadAngle = 2f;
    public float bulletSpeed = 10f;
    public float damage;
    public GameObject effect;
    public float force;
    public float radius = 5f;
    //public StickyShooting gun;
    Vector2 pos;



    private Vector3 mousePos;
    private Camera mainCam;
    void Start()
    {

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        float angle = Random.Range(-spreadAngle, spreadAngle);
        Vector2 directionspread = Quaternion.Euler(0, 0, angle) * transform.up;
        //gun = FindObjectOfType<StickyShooting>();


    }
    private void Update()
    {
        pos = transform.position;
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach(Collider2D collider in colliders)
            {
                PlayerHP playerHP = collider.gameObject.GetComponent<PlayerHP>();
                EnemyHP enemyHP = collider.gameObject.GetComponent<EnemyHP>();
                if(playerHP != null)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);

                    // Calculate the amount of damage to deal based on the distance
                    float damageMultiplier = 1f - Mathf.Clamp01(distance / radius);
                    float damageToDeal = damage * damageMultiplier;

                    playerHP.TakeDamage(damageToDeal);
                   
                }
                if(enemyHP != null)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);

                    // Calculate the amount of damage to deal based on the distance
                    float damageMultiplier = 1f - Mathf.Clamp01(distance / radius);
                    float damageToDeal = damage * damageMultiplier;

                    enemyHP.TakeDamage(force,damageToDeal, pos);
                   
                }

            }
            //gun.isHavingBomb = false;
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
