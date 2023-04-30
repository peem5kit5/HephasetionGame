using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BulletScript : MonoBehaviour
{
    public float recoilForce = 10f;
    public float spreadAngle = 2f;
    public float bulletSpeed = 10f;
    public float damage;
    public float force;
    public GameObject damagePopUp;

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
       



    }
    private void Update()
    {
        pos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            
            collision.GetComponent<EnemyHP>().TakeDamage(force,damage, pos );
            
            GameObject popUp = Instantiate(damagePopUp, gameObject.transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
            Destroy(gameObject);
            
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }





}
