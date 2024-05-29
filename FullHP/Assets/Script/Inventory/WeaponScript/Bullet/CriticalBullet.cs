using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CriticalBullet : MonoBehaviour
{
    public float recoilForce = 10f;
    public float spreadAngle = 2f;
    public float bulletSpeed = 10f;
    public float damage;
    public float critChance;
    public float criticalModDamage;
    public float force;
    public GameObject damagePopUp;
    public GameObject bulletParticle;
    public GameObject critParticle;

    private SpriteRenderer sprite;
    private GameObject child;
    Vector2 pos;
    private Vector3 mousePos;
    private Camera mainCam;
    float random;
    void Start()
    {

        child = transform.Find("sprite").gameObject;
        sprite = child.GetComponent<SpriteRenderer>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        float angle = Random.Range(-spreadAngle, spreadAngle);
        Vector2 directionspread = Quaternion.Euler(0, 0, angle) * transform.up;



    }
    private void Update()
    {
        pos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHP>().TakeDamage(force,damage, pos);
            CalculateDamage(damage);
            SoundManager.Instance.EnemyHitSound();
            Destroy(gameObject);

        }
        else if (collision.CompareTag("Wall"))
        {
            SoundManager.Instance.HitSound();
            Destroy(gameObject);
        }
    }
    private float CalculateDamage(float damage)
    {

        random = Random.Range(0f, 1f);
        if (random < critChance)
        {
            damage *= criticalModDamage;
            GameObject popUp = Instantiate(damagePopUp, gameObject.transform.position, Quaternion.identity);
            Instantiate(critParticle, gameObject.transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
            popUp.GetComponentInChildren<TextMeshPro>().color = Color.red;
            sprite.color = Color.red;
            return damage;
        }
        else
        {
            sprite.color = Color.white;
            Instantiate(bulletParticle, gameObject.transform.position, Quaternion.identity);
            GameObject popUp = Instantiate(damagePopUp, gameObject.transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
            popUp.GetComponentInChildren<TextMeshPro>().color = Color.white;
            return damage;
        }
    }
}
