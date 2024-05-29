using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.Audio;
public class SniperShooting : MonoBehaviour
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
    public AudioClip pistol;
    public int currentClip, maxClip = 10;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI TotalAmmoText;
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
    ToggleUI uiToggle;

    public float knockbackForce;
    public float xPosShootingPoint;


    public bool TakeAim;
    bool increasedFireRate;
    float fireRateStore;
    public AudioMixer mixer;
    public float noise;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        effect = Instantiate(effect, shootingpoint.position, Quaternion.identity);
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        uiToggle = FindObjectOfType<ToggleUI>();
        fireRateStore = fireRate;
    }
    private void Update()
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(this.transform.parent.position, noise);
        foreach (Collider2D col in collider2D)
        {
            if (col.CompareTag("Enemy"))
            {

                Enemy enemy = col.GetComponent<Enemy>();
                //  Nemesis nemesis = col.GetComponent<Nemesis>();

                if (enemy != null && !enemy.isSaw && !enemy.Sus)
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
            // Debug.Log("Facing right");
            fixShootingPoint.transform.localPosition = new Vector3(xPosShootingPoint, AxisShootingPoint, 1);
            sprite.flipY = false;
        }
        else
        {
            fixShootingPoint.transform.localPosition = new Vector3(xPosShootingPoint, FlipAxisShootingPoint, 1);
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
            else
            {
                player.ShootingGun = false;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            player.ShootingGun = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !isReloading && currentClip != maxClip)
        {
            sound.clip = reloadSound;
            sound.Play();

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
        }
        TakeAimLogic();
        UpdateAmmoText();

    }
    void TakeAimLogic()
    {

        if (TakeAim && !increasedFireRate)
        {
            fireRate = fireRate + 1.5f;
            increasedFireRate = true;
        }
        else if (!TakeAim)
        {
            fireRate = fireRateStore;
            increasedFireRate = false;
        }
    }
    private void Shoot()
    {
        if (currentClip > 0 && !isReloading && CanShoot)
        {
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
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootDirection = (mousePos - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, shootingpoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * bulletSpeed;
            player.ShootingGun = true;
            player.KnockbackFromGun(shootDirection, knockbackForce);
            Destroy(bullet, 3);
            // player.KnockbackFromGun(shootDirection, knockbackForce);


            sound.PlayOneShot(pistol);

            effect.transform.position = shootingpoint.position;
            effect.GetComponent<ParticleSystem>().Play();

            StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
            currentClip--;
            player.ShootingGun = false;


        }
        else if (currentClip <= 0 && !isReloading)
        {
            player.ShootingGun = false;

        }
    }
    public void Reload()
    {

        if (currentClip < maxClip && player.sniperAmmo > 0)
        {
            int ammoNeeded = maxClip - currentClip;
            int ammoToReload = Mathf.Min(ammoNeeded, player.sniperAmmo);

            currentClip += ammoToReload;
            player.sniperAmmo -= ammoToReload;
        }
        //  currentClip = maxClip;
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
        TotalAmmoText.text = $"{player.sniperAmmo}";
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