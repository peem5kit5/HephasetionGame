using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShootingSMG : MonoBehaviour
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
    public bool isShooting = false;
    public AudioSource sound;
    public AudioClip gunSound;
    public int currentClip, maxClip = 10, currentAmmo, maxAmmo = 100;
    public float reloadTime;
    public TextMeshProUGUI ammoText;
    public AudioClip reloadSound;

    public GameObject fixShootingPoint;
    public float FlipAxisShootingPoint;
    public float AxisShootingPoint;
    public GameObject effect;

    public Reload_Canvas reload;
    public bool CanShoot;

    private SpriteRenderer sprite;
    float ReadyForNextShot;
    private bool isReloading = false;



    private void Start()
    {
        effect = Instantiate(effect, shootingpoint.position, Quaternion.identity);
        sound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        
    }
    private void Update()
    {
        reload.SetReload(reloadTime);
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
            fixShootingPoint.transform.localPosition = new Vector3(1, AxisShootingPoint, 1);
            sprite.flipY = false;
        }
        else
        {
            fixShootingPoint.transform.localPosition = new Vector3(1, FlipAxisShootingPoint, 1);
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
            isShooting = true;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            sound.Stop();
        }
        if (isShooting)
        {
            if (Time.time > ReadyForNextShot)
            {
               

                ReadyForNextShot = Time.time + 1 / fireRate;
                Shoot();

                


            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentClip != maxClip)
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
        if(currentClip > 0 && !isReloading && CanShoot)
        {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = (mousePos - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);
        StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletSpeed;
        sound.clip = gunSound;
        UpdateAmmoText();
        sound.Play();
        currentClip--;
        effect.transform.position = shootingpoint.position;
        effect.GetComponent<ParticleSystem>().Play();
        }
        else if (currentClip <= 0 && !isReloading)
        {
            sound.Stop();
        }
    }
    public void Reload()
    {
        
        currentClip = maxClip;
        
       
    }
    IEnumerator ReloadTime()
    {
        sound.clip = reloadSound;
        sound.Play();
        reload.Reloading();
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        Reload();
        UpdateAmmoText();
        isReloading = false;
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
