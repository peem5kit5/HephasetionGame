using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotgunShooting : MonoBehaviour
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
    public float spreadAngle = 30.0f;
    public int bulletCount = 3;
    public AudioSource sound;
    public AudioClip shotgunSound;
    public int currentClip, maxClip = 5;
    public float reloadTime;
    public TextMeshProUGUI ammoText;
    public bool CanShoot;
    public AudioClip shotgunReload;
    public Reload_Canvas reloading;

    private SpriteRenderer sprite;
    float ReadyForNextShot;
    private bool isReloading;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
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
            sprite.flipY = false;
        }
        else
        {
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
            Quaternion newRot = shootingpoint.rotation;
            sound.PlayOneShot(shotgunSound);
            for (int i = 0; i < bulletCount; i++)
            {
                float addedOffset = (i - (bulletCount / 2) * spreadAngle);

                newRot = Quaternion.Euler(shootingpoint.eulerAngles.x, shootingpoint.eulerAngles.y + addedOffset, shootingpoint.eulerAngles.z + addedOffset);

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 shootDirection = (mousePos - transform.position).normalized;
                GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, newRot);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = shootDirection * bulletSpeed;
                Destroy(bullet, 3);
                
            }
            currentClip--;
        }
        
    }
    

    IEnumerator ReloadTime()
    {
        while (currentClip < maxClip)
        {
            reloading.Reloading();
            sound.clip = shotgunReload;
            isReloading = true;
            sound.Play();
            yield return new WaitForSeconds(reloadTime);
            currentClip++;
            sound.Stop();
            isReloading = false;
        }
        //  isReloading = false;
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
