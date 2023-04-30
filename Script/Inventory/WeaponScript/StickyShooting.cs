using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StickyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingpoint;
    public float bulletSpeed = 20f;
    public float fireRate;
    public float recoilAmount;
    public CameraShake camShake;
    public float shakeDuration;
    public float magnitude;
    public float offset;
    public AudioSource sound;
    public AudioClip Launcher;
    public bool isHavingBomb;
    public bool bombing;
    public float shakeBomb;
    public float magnitudeBomb;
    public int currentClip, maxClip = 5;
    public float reloadTime;
    public TextMeshProUGUI ammoText;
    public bool CanShoot;
    public Reload_Canvas reloading;
    private SpriteRenderer sprite;
    float ReadyForNextShot;
    private bool isReloading;
    public AudioClip GrenadeReload;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        isHavingBomb = false;
    }
    private void Update()
    {
        reloading.SetReload(reloadTime);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (rotZ < 89 && rotZ > -89)
        {
            Debug.Log("Facing right");
            sprite.flipY = false;
        }
        else
        {
            Debug.Log("Facing left");
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

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > ReadyForNextShot)
            {
                ReadyForNextShot = Time.time + 1 / fireRate;
                Shoot();

                


            }
        }
        if (isHavingBomb)
        {
            if (Input.GetMouseButtonDown(1) && CanShoot)
            {
                bombing = true;
            }
            else if(Input.GetMouseButtonDown(0) && CanShoot)
            {
                bombing = false;
            }
           if (bombing && CanShoot)
           {
               StartCoroutine(camShake.ShakePistol(shakeBomb,magnitudeBomb));
               isHavingBomb = false;
           }
           

        }
        if (isHavingBomb == false)
        {
            bombing = false;
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(ReloadTime());
        }
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            ChargeUI();
        }
        UpdateAmmoText();

    }

    private void Shoot()
    {
        if (currentClip > 0 && !isReloading && CanShoot)
        {
            StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootDirection = (mousePos - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);
            sound.PlayOneShot(Launcher);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = (shootDirection * bulletSpeed) * Time.deltaTime;
            isHavingBomb = true;
            currentClip--;
        }
        
        
    }
    
    public void Reload()
    {
        currentClip = maxClip;
    }
    
    IEnumerator ReloadTime()
    {
        while (currentClip < maxClip)
        {
            isReloading = true;
            reloading.Reloading();
            sound.clip = GrenadeReload;
            sound.Play();
            yield return new WaitForSeconds(reloadTime);
            currentClip++;
            isReloading = false;
            sound.Stop();
        }
    }
    public void UpdateAmmoText()
    {
        ammoText.text = $"{currentClip}";
    }
    public void ChargeUI()
    {
        CanShoot = !CanShoot;
    }
}
