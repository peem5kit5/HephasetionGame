using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class ShotgunShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingpoint;
    public float bulletSpeed;
    public float fireRate;
    public float recoilAmount;
    public TextMeshProUGUI totalAmmoText;
    public CameraShake camShake;
    public float shakeDuration;
    public float magnitude;
    public float offset;
    public float spreadAngle;
    public int bulletCount;
    public AudioSource sound;
    public AudioClip shotgunSound;
    public int currentClip, maxClip;
    public float reloadTime;
    public TextMeshProUGUI ammoText;
    public bool CanShoot;
    public AudioClip shotgunReload;
    public Reload_Canvas reloading;

    private SpriteRenderer sprite;
    float ReadyForNextShot;
    private bool isReloading;
    Player player;
    ToggleUI uiToggle;
    public float knockbackForce;
    public float AxisShootingPoint;
    public float FlipAxisShootingPoint;

    public float xPosShootingPoint;
    bool isAlreadyKnockBack;

    public float noise;
    public AudioMixer mixer;
    private void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
        uiToggle = FindObjectOfType<ToggleUI>();
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
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(this.transform.parent.position, noise);
        foreach (Collider2D col in collider2D)
        {
            if (col.CompareTag("Enemy"))
            {

                Enemy enemy = col.GetComponent<Enemy>();
                //  Nemesis nemesis = col.GetComponent<Nemesis>();

                if (enemy != null && !enemy.isSaw)
                {
                    enemy.currentDurationSus -= Time.deltaTime;
                    if (enemy.currentDurationSus <= 0)
                    {
                        enemy.Sus = false;
                        enemy.wander = true;
                    }
                }


            }
        }
        if (rotZ < 89 && rotZ > -89)
        {
            shootingpoint.transform.localPosition = new Vector3(xPosShootingPoint, AxisShootingPoint, 1);
            sprite.flipY = false;
        }
        else
        {
            shootingpoint.transform.localPosition = new Vector3(xPosShootingPoint, FlipAxisShootingPoint, 1);
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
        if (Time.time < ReadyForNextShot)
        {
            player.ShootingGun = false;
        }
        
        if (Input.GetMouseButtonDown(0))
        {

            if (Time.time > ReadyForNextShot)
            {
                player.KnockbackFromGun(direction * 2, knockbackForce);

                ReadyForNextShot = Time.time + 1 / fireRate;
                Shoot();
                


            }
            StartCoroutine(waitKnock());


        }
     
        if (Input.GetKeyDown(KeyCode.Tab) && !isReloading)
        {
            StartCoroutine(ReloadTime());
        }
        if (uiToggle.isLayout)
        {
            CanShoot = true;
        }
        else
        {

            CanShoot = false;
        }
        UpdateAmmoText();

    }

    private void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(this.transform.parent.position, noise);
        foreach (Collider2D col in collider2D)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                Nemesis nemesis = col.GetComponent<Nemesis>();

                if (enemy != null && !enemy.isSaw)
                {
                    enemy.currentDurationSus = enemy.maxDurationSus;
                    // enemy.wander = false;
                    enemy.wander = false;
                    enemy.Sus = true;
                }

                if (nemesis != null && !nemesis.isSaw)
                {
                    nemesis.Suspecting();
                }
            }
            else if (col.CompareTag("NPCShadow"))
            {
                NPCShadow npc = col.GetComponent<NPCShadow>();
                if (npc != null)
                {
                    npc.StartRunningAway();
                }
            }
        }
        if (currentClip > 0 && !isReloading && CanShoot)
        {

            player.KnockbackFromGun(direction, knockbackForce);
            player.ShootingGun = true;
            StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
            
            ShotGun();
            sound.PlayOneShot(shotgunSound);
         
            currentClip--;
        }
        
        
    }
    IEnumerator waitKnock()
    {
        yield return new WaitForSeconds(2f);
        player.ShootingGun = false;
    }
    void ShotGun()
    {
        //  Quaternion newRot = shootingpoint.rotation;
       
        for ( int i = 0; i < bulletCount; i++)
        {
           
            //player.ShootingGun = true;
            float addedOffset = (i - (bulletCount - 1 / 2) * spreadAngle);

            Quaternion newRot = Quaternion.Euler(shootingpoint.eulerAngles.x, shootingpoint.eulerAngles.y, shootingpoint.eulerAngles.z + addedOffset);

            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // Vector3 multiply = new Vector3(newRot.x, newRot.y, newRot.z);
            Vector3 shootDirection =  newRot * Vector3.up;
            GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);
           
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection.normalized * bulletSpeed;
            Destroy(bullet, 0.5f);
            i++;
            
        }
      

    }
    

    IEnumerator ReloadTime()
    {
        while (currentClip < maxClip && player.shotgunAmmo > 0)
        {
            reloading.Reloading();
            sound.clip = shotgunReload;
            isReloading = true;
            sound.Play();
            yield return new WaitForSeconds(reloadTime);
            currentClip++;
            player.shotgunAmmo--;
            sound.Stop();
            isReloading = false;
        }
      
        //  isReloading = false;
    }
    public void UpdateAmmoText()
    {
        ammoText.text = $"{currentClip}";
        totalAmmoText.text = $"{player.shotgunAmmo}";
    }
    public void ChargeUI()
    {
        CanShoot = !CanShoot;
    }
    private void OnDisable()
    {
        isReloading = false;
    }
}
