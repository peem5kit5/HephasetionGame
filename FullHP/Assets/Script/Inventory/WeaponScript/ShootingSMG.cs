using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    public bool isShooting;
    public AudioSource sound;
    public AudioClip gunSound;
    public int currentClip, maxClip = 10;
    public TextMeshProUGUI TotalAmmoText;
    Player player;
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
    private bool isReloading;
    public GameObject LightMuzzle;
    ToggleUI uiToggle;
    public float maxRecoilAngle;
    public float maxSpreadAngle;
    public float knockbackForce;

    public float maxRecoilAngleStore;
    public float maxSpreadAngleStore;

    public float xPosShootingPoint;

    bool decreasedRecoil;
    public bool TakeAim;

    public float noise;
    public AudioMixer mixer;
    private void Start()
    {
        isShooting = false;
        isReloading = false;
        effect = Instantiate(effect, shootingpoint.position, Quaternion.identity);
        sound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        uiToggle = FindObjectOfType<ToggleUI>();
        maxRecoilAngleStore = maxRecoilAngle;
        maxSpreadAngleStore = maxSpreadAngle;
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

        if (!DeathEffectInteract.Instance.Slowed)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

        }
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(this.transform.parent.position, noise);
        foreach (Collider2D col in collider2D)
        {
            if (col.CompareTag("Enemy"))
            {

                Enemy enemy = col.GetComponent<Enemy>();
                //  Nemesis nemesis = col.GetComponent<Nemesis>();

                if (enemy != null && !enemy.isSaw && enemy.Sus)
                {
                    enemy.currentDurationSus -= Time.deltaTime;
                    if(enemy.currentDurationSus <= 0)
                    {
                        enemy.Sus = false;
                        enemy.wander = true;
                    }
                }


            }
        }
        if (rotZ < 89 && rotZ > -89)
        {
            fixShootingPoint.transform.localPosition = new Vector3(xPosShootingPoint, AxisShootingPoint, 1);
            sprite.flipY = false;
        }
        else
        {
            fixShootingPoint.transform.localPosition = new Vector3(xPosShootingPoint, FlipAxisShootingPoint, 1);
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
            LightMuzzle.SetActive(false);
            player.ShootingGun = false;
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
        if (Input.GetKeyDown(KeyCode.Tab) && !isReloading && currentClip != maxClip)
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
            player.ShootingGun = false;
            isShooting = false;
            LightMuzzle.SetActive(false);
            sound.Stop();
        }
        TakeAimSkillLogic();
        UpdateAmmoText();

    }
    void TakeAimSkillLogic()
    {
        if(TakeAim && !decreasedRecoil)
        {
            maxRecoilAngle = 0;
            maxSpreadAngle = 0;
            decreasedRecoil = true;
        }
        else if (!TakeAim)
        {
            maxRecoilAngle = maxRecoilAngleStore;
            maxSpreadAngle = maxSpreadAngleStore;
            decreasedRecoil = false;
        }
    }
    private void Shoot()
    {
        if(currentClip > 0 && !isReloading && CanShoot)
        {
            if(knockbackForce > 0)
            {
                player.ShootingGun = true;
            }
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
                    if(npc != null)
                    {
                        npc.StartRunningAway();
                    }
                }
            }
                LightMuzzle.SetActive(true);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3 shootDirection = (mousePos - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);
            StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
            float recoilAngle = Random.Range(-maxRecoilAngle, maxRecoilAngle);
            float spreadAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            shootDirection = Quaternion.Euler(0, 0, recoilAngle) * shootDirection;
            shootDirection = Quaternion.Euler(0, 0, spreadAngle) * shootDirection;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * bulletSpeed;
            player.KnockbackFromGun(shootDirection, knockbackForce);
            
            sound.clip = gunSound;
        UpdateAmmoText();
            
                sound.Play();
            
       
        currentClip--;
        effect.transform.position = shootingpoint.position;
        effect.GetComponent<ParticleSystem>().Play();
        }
        else if (currentClip <= 0 && !isReloading)
        {
            player.ShootingGun = false;
            LightMuzzle.SetActive(false);
            sound.Stop();
        }
    }
    public void Reload()
    {

        if (currentClip < maxClip && player.automaticAmmo > 0)
        {
            int ammoNeeded = maxClip - currentClip;
            int ammoToReload = Mathf.Min(ammoNeeded, player.automaticAmmo);

            currentClip += ammoToReload;
            player.automaticAmmo -= ammoToReload;
        }


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
        TotalAmmoText.text = $"{player.automaticAmmo}";
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
