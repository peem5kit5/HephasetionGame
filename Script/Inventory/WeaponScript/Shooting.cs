using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject criticalBulletPrefab;
    public Transform shootingpoint;
    public float bulletSpeed = 20f;
    public float fireRate;
    public float recoilAmount;
    public CameraShake camShake;
    public float shakeDuration;
    public float magnitude;
    public float offset;
    public AudioSource sound;
    public AudioClip pistol;
    public int currentClip, maxClip = 10;
    public TextMeshProUGUI ammoText;
    public float reloadTime;
    public AudioClip reloadSound;
   // public GameObject reloadUI;
   public Reload_Canvas reloading;
    public GameObject effect;
    
    public GameObject fixShootingPoint;
    public float FlipAxisShootingPoint;
    public float AxisShootingPoint;

    public bool CanShoot;
    private SpriteRenderer sprite;
    float ReadyForNextShot;
    private bool isReloading = false;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        effect = Instantiate(effect, shootingpoint.position, Quaternion.identity);
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
        reloading.SetReload(reloadTime);   
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y ,difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ );

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (rotZ < 89 && rotZ > -89)
        {
           // Debug.Log("Facing right");
            fixShootingPoint.transform.localPosition = new Vector3(1, AxisShootingPoint, 1);
            sprite.flipY = false;  
        }
        else
        {
            fixShootingPoint.transform.localPosition = new Vector3(1, FlipAxisShootingPoint, 1);
            //Debug.Log("Facing left");
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
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentClip != maxClip)
        {
            sound.clip = reloadSound;
            sound.Play();
            
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
        if (currentClip > 0 && !isReloading && CanShoot )
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootDirection = (mousePos - transform.position).normalized;
            if (!player.cri)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = shootDirection * bulletSpeed;
                Destroy(bullet, 3);

            }
            else if (player.cri)
            {
               
                GameObject bullet = Instantiate(criticalBulletPrefab, shootingpoint.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = shootDirection * bulletSpeed;
                Destroy(bullet, 3);
            }
            sound.PlayOneShot(pistol);
            
            effect.transform.position = shootingpoint.position;
            effect.GetComponent<ParticleSystem>().Play();
            
            StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
            currentClip--;
           
        }
    }
    public void Reload()
    {
        currentClip = maxClip;
    }
    
    IEnumerator ReloadTime()
    {
       
        isReloading = true;
        //reload.Reloading();
        reloading.Reloading();
        yield return new WaitForSeconds(reloadTime);
        Reload();
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

