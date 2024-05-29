using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingShotgun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform shootingpoint;
    public float bulletSpeed = 20f;
    public float fireRate;
    public float recoilAmount;
    public AudioClip pistol;
 

    public SpriteRenderer sprite;
    float ReadyForNextShot;
    GameObject player;
    private bool canShoot = true;
    public float spreadAngle;
    public int bulletCount;
    public AudioSource audioGun;
    public AudioClip sound;
    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioGun = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Shoot()
    {
        audioGun.PlayOneShot(sound);
        for (int i = 0; i < bulletCount; i++)
        {

            //player.ShootingGun = true;
            float addedOffset = (i - (bulletCount - 1 / 2) * spreadAngle);

            Quaternion newRot = Quaternion.Euler(shootingpoint.eulerAngles.x, shootingpoint.eulerAngles.y, shootingpoint.eulerAngles.z + addedOffset);

            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector3 multiply = new Vector3(newRot.x, newRot.y, newRot.z);
            Vector3 shootDirection = newRot * Vector3.up;
            GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection.normalized * bulletSpeed;
            Destroy(bullet, 0.5f);
            i++;

        }
    }
    public void Shooting()
    {
        Vector3 playerPosition = player.transform.position - transform.position;
        Vector3 difference = playerPosition;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        Vector3 pos = player.transform.position;
        pos.z = 0;
        Vector3 direction = (pos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (rotZ < 90 && rotZ > -90)
        {
            // sprite.flipX = true;
            sprite.flipY = false;
        }
        else
        {
            //  sprite.flipX = false;
            sprite.flipY = true;
        }

        if (angle > 44 && angle <= 120)
        {

            sprite.sortingOrder = -1;

        }
        else
        {
            sprite.sortingOrder = 1;
        }

        if (Time.time > ReadyForNextShot && canShoot)
        {
            ReadyForNextShot = Time.time + 1 / fireRate;
            Shoot();

        }
       

    }
}
