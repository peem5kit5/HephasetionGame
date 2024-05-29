using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAnimal : MonoBehaviour
{
    public AIPath path;
    public Rigidbody2D rb;

    public float currentCharge;
    public float maxCharge;
    public float ChargeSpeed;
    Animator anim;
    public float moveSpeed;

    public BoxCollider2D ChargeHitBox;
    CameraShake camShake;
    public float sense;
    public bool isChase;

    public float attackCooldown;
    public float maxAttackCooldown;

    public float wanderTime;
    public float maxWanderTime;
    public float attackRange;
    SpriteRenderer sprite;
    EnemyHP enemHP;
    public GameObject effectDeath;

    bool toggleVisiblity;
    public Player player;

    AudioSource audioSound;
    public AudioClip sound;
    bool isSounded;

    // Start is called before the first frame update
    void Start()
    {
        audioSound = GetComponent<AudioSource>();
        audioSound.volume = 0.5f;
        enemHP = GetComponent<EnemyHP>();
        camShake = FindObjectOfType<CameraShake>();
        path = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        SetVisibility(false);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckingPlayer();
        LogicWalk();
        AnimController();

        if (enemHP.currentHP <= 0)
        {
            Instantiate(effectDeath, this.transform.position, Quaternion.identity);
            DeathEffectInteract.Instance.KilledEffect();
            Destroy(gameObject, 0.01f);
        }
    }
    void LogicWalk()
    {
        if (isChase)
        {
            if (!isSounded)
            {
                audioSound.PlayOneShot(sound);
                isSounded = true;
            }
            Player player = FindObjectOfType<Player>();
            path.destination = player.transform.position;
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
            attackCooldown -= Time.deltaTime;
            if(attackCooldown <= 0)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, attackRange);
                foreach (Collider2D col in collider)
                {
                    if (col != null)
                    {
                        if (col.CompareTag("Player"))
                        {
                            anim.SetTrigger("Attack");
                            attackCooldown = maxAttackCooldown;
                        }
                    }
                }
            }
            
            
         
            
        }
        else
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
            wanderTime -= Time.deltaTime;
            if(wanderTime <= 0)
            {
                path.canMove = true;
                Vector2 randomWalk = Random.insideUnitCircle.normalized;
                Vector2 thisPos = transform.position;
                Vector2 randomWander = thisPos + randomWalk * 5;
                path.destination = randomWander;
                wanderTime = maxWanderTime;
            }
           
           
          
        }
    }
    public void SetVisibility(bool isSaw)
    {
        toggleVisiblity = isSaw;

        if (toggleVisiblity)
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            //SpriteRenderer[] GunHolder = gameObject.transform.Find("WeaponHolder").GetComponentsInChildren<SpriteRenderer>();
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.GetComponent<SpriteRenderer>().enabled = true;
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            colorNotVisible.a = 1;
         
            sprite.color = colorNotVisible;
            //Shadow.color = colorNotVisible;
        }
        else
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            //SpriteRenderer[] GunHolder = gameObject.transform.Find("WeaponHolder").GetComponentsInChildren<SpriteRenderer>();
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.GetComponent<SpriteRenderer>().enabled = false;
            colorNotVisible.a = 0;
            
            sprite.color = colorNotVisible;

        }
    }
    void AnimController()
    {
        if(path.velocity.magnitude > 0.01f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (path.velocity.x > 0.1f)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
    void CheckingPlayer()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, sense);
        foreach (Collider2D col in collider)
        {
            if (col != null)
            {
                if (col.CompareTag("Player"))
                {
                    isChase = true;
                }
            }
        }
    }

  
}
