using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
public class ShootingDPS : MonoBehaviour
{
  //  public GameObject bulletPrefab;
 //   public Transform shootingpoint;
  //  public float bulletSpeed = 20f;
    public float DamageRate;
    public float MaxInterval;
  //  public float recoilAmount;
    public CameraShake camShake;
    public float shakeDuration;
    public float magnitude;
    public float offset;
    public bool isShooting = false;
    public AudioSource sound;
    public AudioClip gunSound;
    public int currentClip, maxClip;
    public TextMeshProUGUI TotalAmmoText;
    public float reloadTime;
    public TextMeshProUGUI ammoText;
    public AudioClip reloadSound;

    Player player;
    public LayerMask layerMask;
    public AudioMixer mixer;




    public Reload_Canvas reload;
    public bool CanShoot;
    //  public GameObject laserBeam;
    public GameObject lineOfBeam;

    private SpriteRenderer sprite;
    float ReadyForNextShot;
    private bool isReloading;

    public int damage;
    public GameObject damagePopUp;
    public float laserRange;
    bool Beaming;
    public Transform shootingPoint;
    Vector3 directionForBeam;
    ToggleUI uiToggle;
    public float AxisShootingPoint;
    public float FlipAxisShootingPoint;
    //  public LayerMask ThisPlayerLayer;
    public float noise;
    bool sounded;
    private void Start()
    {

        Beaming = false;
        isShooting = false;
        CanShoot = false;
        isReloading = false;
      //  effect = Instantiate(effect, shootingpoint.position, Quaternion.identity);
        sound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        uiToggle = FindObjectOfType<ToggleUI>();
      //  laserBeamCollision.enabled = false;
      //  laserBeamLight1.SetActive(false);
      //  laserBeamLight2.SetActive(false);
      //  laserBeamSprite.enabled = false;
    }
     Vector3 ReturnDirection()
    {
        return directionForBeam;
    }
    private void Update()
    {
        
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
                    if (enemy.currentDurationSus <= 0)
                    {
                        enemy.Sus = false;
                        enemy.wander = true;
                    }
                }


            }

        }
        DamageRate -= Time.deltaTime;
       // ThisPlayerLayer = ~(1 << LayerMask.NameToLayer("Theirself"));
        reload.SetReload(reloadTime);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        //shootingPoint.rotation = Quaternion.Euler(0f, 0f, rotZ);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;

      //  directionForBeam = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (rotZ < 89 && rotZ > -89)
        {
            // fixShootingPoint.transform.localPosition = new Vector3(1, AxisShootingPoint, 1);
            //shootingPoint.transform.localPosition = new Vector3(1, AxisShootingPoint, 1);
          //  lineOfBeam.transform.localPosition = new Vector3(1, AxisShootingPoint, 1);
            sprite.flipY = false;
        }
        else
        {
            //  fixShootingPoint.transform.localPosition = new Vector3(1, FlipAxisShootingPoint, 1);
          //  shootingPoint.transform.localPosition = new Vector3(1, FlipAxisShootingPoint, 1);
           // lineOfBeam.transform.localPosition = new Vector3(1, FlipAxisShootingPoint, 1);
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
       
        if (isShooting && currentClip > 0)
        {
            if (Time.time > ReadyForNextShot)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = gunSound;
                    sound.Play();
                }
               
                //ReadyForNextShot = Time.time + 1 / fireRate;
                Shoot();
           //     StartCoroutine(ApplyDPS());



            }
        }
        else
        {
            Beaming = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Tab) && !isReloading && currentClip != maxClip)
        {
       //     currentClip = maxClip;
            StartCoroutine(ReloadTime());

            //StopCoroutine(ApplyDPS());

        }
        if (uiToggle.isLayout)
        {
            CanShoot = true;
        }
        else
        {
            CanShoot = false;
        }
        if (Beaming && !isReloading && currentClip > 0)
        {
            DealingDamage();
            lineOfBeam.SetActive(true);
        }
        else
        {
            lineOfBeam.SetActive(false);
        }
        
       
        UpdateAmmoText();
        
    }

    private void Shoot()
    {
        if (currentClip > 0 && !isReloading && CanShoot && !Beaming)
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
            // lineOfBeam.SetActive(true);
            Beaming = true;
            //  laserBeamCollision.enabled = true;
            //laserBeamLight1.SetActive(true);
            // laserBeamLight2.SetActive(true);
            //  laserBeamSprite.enabled = true;
            //  laserBeam.SetActive(true);


        }
        else if (currentClip <= 0 && !isReloading && CanShoot && Beaming)
        {
           
            // lineOfBeam.SetActive(false);
            Beaming = false;
            
           // StopCoroutine(ApplyDPS());
            // laserBeamCollision.enabled = false;
            // laserBeamLight1.SetActive(false);
            // laserBeamLight2.SetActive(false);
            // laserBeamSprite.enabled = false;
         //   laserBeam.SetActive(false);
        }
    }
    void DealingDamage()
    {
        

        StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
        
        

        //   wallLayer = ~(1 << LayerMask.NameToLayer("BlockPath"));
        //  LayerMask hitLayerMask = LayerMask.GetMask("Theirself", "BlockPath");
        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(shootingPoint.transform.position, shootingPoint.transform.right , laserRange, layerMask);
        //Physics2D.IgnoreLayerCollision(hit.collider.gameObject.layer, playerLayer);
        //   lineOfBeam.SetPosition(0, shootingPoint.transform.position);
        //  lineOfBeam.SetPosition(1, hit.point);
        Debug.DrawLine(shootingPoint.transform.position, hit.point, Color.red, 1.0f);

        //Debug.DrawLine(hit, Color.red, 1.0f);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
            {
                
                EnemyHP enemyHP = hit.collider.GetComponent<EnemyHP>();
            if(enemyHP != null)
            {
                if (DamageRate <= 0)
                {
                    enemyHP.TakeDamage(1, damage, gameObject.transform.position);
                    GameObject popUp = Instantiate(damagePopUp, gameObject.transform.position, Quaternion.identity);
                    popUp.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
                    DamageRate = MaxInterval;
                    currentClip--;
                }
            }
                
               
               

            }
       else if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall"))
        {
            return;
        }
        else
        {
            return;
        }

           
        //else
        //  {
        //   return;
        //  }









    }
   

   
    public void Reload()
    {
        if(currentClip < maxClip && player.batteryAmmo > 0)
        {
            int ammoNeeded = maxClip - currentClip;
            int ammoToReload = Mathf.Min(ammoNeeded, player.batteryAmmo);
            currentClip += ammoToReload;
            player.batteryAmmo -= ammoToReload;
        }
       


    }
    IEnumerator ReloadTime()
    {
        sound.clip = reloadSound;
    //    sound.Play();
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
        TotalAmmoText.text = $"{player.batteryAmmo}";
    }
   
    private void OnDisable()
    {
        isReloading = false;
       // StopCoroutine(ApplyDPS());
    }

}
