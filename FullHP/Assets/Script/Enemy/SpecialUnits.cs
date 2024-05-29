using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
public class SpecialUnits : MonoBehaviour
{
    public AIPath path;
    public Animator anim;
    public Transform player;
    public bool isSaw;
    public bool canMove;
    private EnemyHP hp;
    Transform shootingPoint;
    Rigidbody2D rb;
    public float DashSpeed;
   // public Image FlashEffect;
    public GameObject DeathEffect;
    CameraShake camShake;
    //MechaGoblin
    public GameObject Missile;
    int missile;
    int maxmissile;
    public float fireRate;
    public bool special;
    public bool armed;
    public bool chainSaw;
    public GameObject damagingArea;

    #region Trapper

    public int maxTraps;
    public int currentTraps;
    public float cooldownTrap;
    public float maxCooldownTrap;
    bool isDashing;
    Vector2 areaToWalk;
    public float chasingRange;
    SpriteRenderer sprite;
    public GameObject TrapObject;
    public GameObject boom;
    #endregion

    //
    public float wanderingTime;
    public float wanderingDuration;
    public BoxCollider2D boxToPatrol;
    Vector2 startPos;
    Vector3 lastPos;
    public float cooldownWander;
    public float maxCooldownWander;

    public float speed;
    bool wander;
    bool toggleVisiblity;

    #region SuperSoldier
    public float maxCooldownRush;
    public float currentRush;
    public float maxCooldownJump;
    public float currentJump;
    public GameObject targetDrop;
    bool follwingTarget;

    AudioSource audioSound;
    public AudioClip ChainSawSound;
    public AudioClip ChainSawStart;

    public AudioClip Soldier_Jump;
    public AudioClip Soldier_Down;
    public AudioClip Soldier_Charge;

    public AudioClip Trap_Sound;
    public GameObject IDCard;
    public void PlayStartChainSawSound()
    {
        audioSound.PlayOneShot(ChainSawStart);
    }
    public void PlayChainSawSound()
    {
        audioSound.PlayOneShot(ChainSawSound);
    }
    public void PlaySoldier_JumpSound()
    {
        audioSound.PlayOneShot(Soldier_Jump);
    }
    public void PlaySoldier_DownSound()
    {
        audioSound.PlayOneShot(Soldier_Down);
    }
    public void PlaySoldier_ChargeSound()
    {
        audioSound.PlayOneShot(Soldier_Charge);
    }
    public void PlayTrap_Sound()
    {
        audioSound.PlayOneShot(Trap_Sound);
    }


    #endregion
    public enum SpecialUnitType
    {
        MechaGoblin,
        Trapper,
        SuperSoldier,
    }
    public enum QuestingType
    {
        None,
        MechaGoblinQuest,
        TrapperQuest,
        SuperSoliderQuest

        
    }
    public SpecialUnitType unit;
    public int moneyReward;
    void Start()
    {
        audioSound = GetComponent<AudioSource>();
        audioSound.volume = 0.5f;
        isSaw = false;
        canMove = false;
        missile = 0;
        armed = true;
        chainSaw = false;
        isDashing = false;
        maxmissile = 4;
        camShake = FindObjectOfType<CameraShake>();
     //   FlashEffect.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();
        lastPos = transform.position;
        hp = this.GetComponent<EnemyHP>();
        anim = transform.GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        shootingPoint = transform.Find("shootingPoint");
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        wander = true;
        cooldownTrap = 0;
        ReSetVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.position;
        EnemyStartLogic();
        if (isSaw)
        {
            //anim.SetTrigger("Alert");
            EnemiesBehaviour();
        }
        else
        {
            EnemyWanderLogic();
        }
        if(hp.currentHP <= 0)
        {
            HPOut();
        }
    }
    public Vector2 GetBoxToWander(BoxCollider2D box)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        areaToWalk = startPos + randomDirection * 5;
        Vector2 minBound = box.bounds.min;
        Vector2 maxBound = box.bounds.max;

        areaToWalk.x = Mathf.Clamp(areaToWalk.x, minBound.x, maxBound.x);
        areaToWalk.y = Mathf.Clamp(areaToWalk.y, minBound.y, maxBound.y);
        boxToPatrol = box;
        return areaToWalk;
    }
    void EnemyWanderLogic()
    {
        switch (unit)
        {
            default:
            case SpecialUnitType.MechaGoblin:
                return;
            case SpecialUnitType.Trapper:
                Wandering();
                StartCoroutine(wanderTiming());
                if(path.velocity.magnitude > 0.01f)
                {
                    anim.SetBool("isMoving", true);
                }
                else
                {
                    anim.SetBool("isMoving", false);
                }
                break;
            case SpecialUnitType.SuperSoldier:
                return;
                
        }
    }
    void EnemyStartLogic()
    {
        switch (unit)
        {
            default:
            case SpecialUnitType.MechaGoblin:
                if (hp.currentHP < hp.maxHP && !isSaw)
                {
                    anim.SetTrigger("Alert");

                }
                break;
            case SpecialUnitType.Trapper:
                if (hp.currentHP < hp.maxHP && !isSaw)
                {
                    isSaw = true;

                }
                break;
            case SpecialUnitType.SuperSoldier:
                if (hp.currentHP < hp.maxHP && !isSaw)
                {
                    isSaw = true;

                }
                break;
        }
    }
    IEnumerator wanderTiming()
    {
        wander = false;
        yield return new WaitForSeconds(5);
        wander = true;
    }
    void Wandering()
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
        wanderingTime += Time.deltaTime;
        if (wanderingTime >= wanderingDuration && !isSaw)
        {

            path.maxSpeed = speed - 0.5f;
            wanderingTime = 0;
            GetBoxToWander(boxToPatrol);
            path.destination = areaToWalk;
          //  Debug.Log(transform.position);

        }
    }
    void EnemiesBehaviour()
    {
        switch (unit)
        {
            default:
            case SpecialUnitType.MechaGoblin:

                if (special && armed && !canMove)
                {
                    ShootingMissile();
                    if (missile >= maxmissile)
                    {
                        special = false;
                        armed = false;
                        canMove = true;
                    }
                }
                if (canMove)
                {
                    anim.SetBool("isMoving", true);
                    anim.SetBool("Walk", true);
                    Vector3 playerdirection = player.position - transform.position;

                    if (playerdirection.x > 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }

                }
                else if (!canMove)
                {
                    anim.SetBool("isMoving", false);
                    anim.SetBool("Walk", false);
                }
                if (canMove)
                {
                    damagingArea.SetActive(false);
                    path.destination = player.position;
                    path.canMove = true;
                }
                else if (!canMove && !armed && !special && !chainSaw)
                {

                    isDashing = false;
                    damagingArea.SetActive(false);
                    path.canMove = false;
                }
                else if (!canMove && !armed && !special && chainSaw)
                {
                    if (!isDashing)
                    {


                        isDashing = true;

                    }
                    else
                    {
                        rb.velocity = Vector3.zero;
                    }
                    damagingArea.SetActive(true);

                }
                if (isDashing)
                {
                    Vector3 dash = (player.transform.position - transform.position).normalized * DashSpeed;
                    rb.velocity = dash * DashSpeed * Time.deltaTime;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }

                break;
            case SpecialUnitType.Trapper:

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
                cooldownTrap -= Time.deltaTime;
                cooldownWander -= Time.deltaTime;

                Vector2 playerPos = player.position;
                if (cooldownWander <= 0)
                {
                    Vector2 randomPlayer = Random.insideUnitCircle.normalized * 5;
                    Vector2 playerPosition = player.position;
                    areaToWalk = playerPosition + randomPlayer * 2;
                    path.destination = areaToWalk;
                    cooldownWander = maxCooldownWander;
                }
          
                
                if (path.velocity.magnitude > 0.1f)
                {
                    anim.SetBool("isMoving", true);
                }
                else
                {
                    anim.SetBool("isMoving", false);
                }
                transform.localScale = new Vector3(1, 1, 1);
                Vector2 playerDirection = player.transform.position - transform.position;
                if (playerDirection.x > 0)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
               
                if (cooldownTrap <= 0 && currentTraps < maxTraps)
                {
                    
                    GameObject Traps = Instantiate(TrapObject, transform.position, Quaternion.identity);
                    Destroy(Traps, 60);
                    cooldownTrap = maxCooldownTrap;
                    PlayTrap_Sound();
                    currentTraps++;
                }
                if(Vector2.Distance(playerPos, transform.position) > chasingRange)
                {
                    isSaw = false;

                }
                
                break;
            case SpecialUnitType.SuperSoldier:
               
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
                currentJump -= Time.deltaTime;
                currentRush -= Time.deltaTime;
                if (currentRush <= 0)
                {
                    anim.SetTrigger("Rush");
                    currentRush = maxCooldownRush;
                }
                if (currentJump <= 0)
                {
                    anim.SetTrigger("Jump");
                    currentJump = maxCooldownJump;
                   
                }
                if (follwingTarget)
                {
                    TargetForSuperSoldier[] target = FindObjectsOfType<TargetForSuperSoldier>();
                    foreach (TargetForSuperSoldier tar in target)
                    {
                        this.transform.position = Vector3.Lerp(this.transform.position, tar.transform.position, 1.5f);
                    }
                }
                path.destination = player.position;
                transform.localScale = new Vector3(1, 1, 1);
                Vector2 playerDirectionSoldier = transform.position - player.transform.position;
                if (playerDirectionSoldier.x > 0)
                {
                    sprite.flipX = false;
                }
                else
                {
                    sprite.flipX = true;
                }
                if (path.velocity.magnitude >= 0.01f)
                {
                    anim.SetBool("isMoving", true);
                }
                else
                {
                    anim.SetBool("isMoving", false);
                }
                break;

        }
    }
    public void SetVisibility(bool isSaw)
    {

        toggleVisiblity = isSaw;

        if (toggleVisiblity)
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.SetActive(true);
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            colorNotVisible.a = 1;
        
            sprite.color = colorNotVisible;
           
        }
        else
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.SetActive(false);
            colorNotVisible.a = 0;
            sprite.color = colorNotVisible;
        
        }
    }
    public void ReSetVisibility()
    {
        SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
        colorNotVisible.a = 0f;
        sprite.color = colorNotVisible;
        GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
        Shadow.SetActive(false);
    }
    public void spawnTarget()
    {
        GameObject obj = Instantiate(targetDrop, this.transform.position, Quaternion.identity);
        Destroy(obj, 1.6f);
    }
    void HPOut()
    {
        switch (unit)
        {
            default:
            case SpecialUnitType.MechaGoblin:
                DeathEffectInteract.Instance.KilledEffect();
                Instantiate(DeathEffect, gameObject.transform.position, Quaternion.identity);
                SoundManager.Instance.EnemyDeadSound();
                camShake.ShakePistol(0.5f, 40);
                Destroy(gameObject, 0.01f);
                break;
            case SpecialUnitType.Trapper:
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);
                Instantiate(boom, gameObject.transform.position, Quaternion.identity);
                camShake.ShakePistol(0.02f, 0.08f);
                foreach (Collider2D collider in colliders)
                {
                    PlayerHP playerHP = collider.gameObject.GetComponent<PlayerHP>();

                    if (playerHP != null)
                    {
                        float distance = Vector2.Distance(transform.position, collider.transform.position);

                        // Calculate the amount of damage to deal based on the distance
                        float damageMultiplier = 1f - Mathf.Clamp01(distance / 5);
                        float damageToDeal = 100 * damageMultiplier;

                        playerHP.TakeDamage(damageToDeal);

                    }
                }
                Destroy(gameObject, 0.01f);
                break;
            case SpecialUnitType.SuperSoldier:
                Instantiate(DeathEffect, gameObject.transform.position, Quaternion.identity);

                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {

                    if (quest.goal.goalType == GoalType.BatholoQuest9)
                    {

                        Instantiate(IDCard, this.transform.position, Quaternion.identity);
                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
               
                camShake.ShakePistol(0.02f, 0.08f);
                Destroy(gameObject, 0.01f);
                break;
        }
      
    }
  
    public void SuperSoldierLogic()
    {
       
    }
    public void FollowTarget()
    {

        StartCoroutine(Following());
    }
    IEnumerator Following()
    {
        follwingTarget = true;
       
        yield return new WaitForSeconds(1.5f);
        follwingTarget = false;


    }
    public void SuperSoldierAnimLogic()
    {
      
    }
    public void ShootingMissile()
    {
        StartCoroutine(ReleaseMissile());
    }
   
    IEnumerator ReleaseMissile()
    {
        while (missile < maxmissile)
        {
            Vector3 shootingDirection = (player.position - transform.position).normalized;
            GameObject bullet = Instantiate(Missile, shootingPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootingDirection * 5;
            armed = false;
            yield return new WaitForSeconds(fireRate);
            armed = true;
            missile++;


        }

    }
    

}
