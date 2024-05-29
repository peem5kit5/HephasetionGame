using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float Range;
    public int damage;
    // public float recoilAmount;
    AudioSource sound;
    // public int maxClip, currentClip;
    public LayerMask playerMask;
    public SpriteRenderer sprite;
    public float cooldownSlash;
    Animator anim;
    private GameObject player;
    private PlayerHP playerHP;
    Transform transforming;
    Enemy enemy;
    private void Start()
    {
      //  sprite = GetComponentInChildren<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        transforming = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        enemy = GetComponentInParent<Enemy>();
    }
    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
        playerHP = FindObjectOfType<PlayerHP>();
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
        cooldownSlash -= Time.fixedDeltaTime;
        Vector2 direction = transform.position - player.transform.position;
        //Vector2 Size = new Vector2(RangeX, RangeY);
        float angle = Vector2.Angle(transform.up, direction);
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Range, playerMask);
        foreach(Collider2D collider in colliders)
        {
            PlayerHP playerHP = collider.GetComponent<PlayerHP>();
            if (playerHP != null)
            {

               
                if (cooldownSlash <= 0)
                {
                    playerHP.TakeDamage(damage);
                    Attack();
                    cooldownSlash = 3;
                }
            }
        }
    }
    private void Attack()
    {
        
        
            anim.SetTrigger("Slash");
       
        //Vector3 shootDirection = (player.transform.position - transform.position).normalized;
      
        //sound.PlayOneShot(pistol);
       // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
      //  rb.velocity = shootDirection * bulletSpeed;
      //  Destroy(bullet, 3);
    }

    public void Slasher()
    {
        if(this != null)
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
                if(sprite != null)
                {
                    sprite.flipY = false;
                }
                else
                {
                    Debug.Log("InitMelee");
                }
                //sprite.flipX = true;
                //transform.localScale = new Vector3(1, 1, 1);
                
            }
            else
            {
                //sprite.flipX = false;
                //  transform.localScale = new Vector3(1, 1, 1);
                if(sprite != null)
                {
                    sprite.flipY = true;
                }
                else
                {
                    Debug.Log("MeleeInit");
                }
            }
            if (angle > 44 && angle <= 120)
            {

                sprite.sortingOrder = -1;

            }
            else
            {
                sprite.sortingOrder = 1;
            }
        }
       

       
       

       
       
    }
   
}
