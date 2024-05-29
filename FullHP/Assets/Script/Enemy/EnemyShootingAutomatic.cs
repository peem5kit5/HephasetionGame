using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingAutomatic : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingpoint;
    public float bulletSpeed = 20f;
    public float fireRate;
    public float recoilAmount;
    public AudioSource sound;
    public AudioClip pistol;
    public AudioClip reloadSound;
    public int currentClip, maxClip;
    public float reloadTime;
    public GameObject reloadNotice;

    public SpriteRenderer sprite;
    float ReadyForNextShot;
    GameObject player;
    private bool canShoot = true;
    public float maxRecoilAngle;
    public float maxSpreadAngle;

    
    private void Start()
    {
        currentClip = maxClip;
      
        sound = GetComponent<AudioSource>();
       
    }
    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
    }
    private void Update()
    {

    }

    private void Shoot()
    {
        Vector3 mousePos = player.transform.position;
        Vector3 shootDirection = (player.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);
        sound.PlayOneShot(pistol);
        float recoilAngle = Random.Range(-maxRecoilAngle, maxRecoilAngle);
        float spreadAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
        shootDirection = Quaternion.Euler(0, 0, recoilAngle) * shootDirection;
        shootDirection = Quaternion.Euler(0, 0, spreadAngle) * shootDirection;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletSpeed;
        Destroy(bullet, 3);
        currentClip--;
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

        if (Time.time > ReadyForNextShot && canShoot && currentClip > 0)
        {
            ReadyForNextShot = Time.time + 1 / fireRate;
            Shoot();

        }
        if(currentClip <= 0)
        {
            canShoot = false;
            StartCoroutine(Reloading());
        }
        
    }

    IEnumerator Reloading()
    {
      //  canShoot = false;
        sound.PlayOneShot(reloadSound);
        reloadNotice.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        reloadNotice.SetActive(false);
        Reload();
        canShoot = true;
    }
    void Reload()
    {
        currentClip = maxClip;
    }

    
}
