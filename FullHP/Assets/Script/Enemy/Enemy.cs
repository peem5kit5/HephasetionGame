using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
public class Enemy : MonoBehaviour

{
    public float stoppingDistance;
    public float retreatDistance;
    public AudioSource audioManage;
    public AudioClip Gah;
    public AIPath path;
    public float speed;
    public float coverDetectRadius = 5f;
    public EnemyHP hp;
    public EnemiesType enemiesType;
    public Player playerQ;
    public Quest quest;
    public EnemyShooting shoot;
    public EnemyShootingAutomatic shootSMG;
    public EnemyShootingShotgun shootShotgun;
    public MeleeEnemy melee;
    public EnemyAnim enemyAnim;
    public bool isSaw = false;
    public GameObject gun;
    private SpriteRenderer sprite;
    Transform coverPos;
    public float wanderingTime;
    private float wanderingDuration;
    Vector2 startPos;
    private bool takingCover = false;
    public Transform player;
    LineOfSight lineofSight;
    public bool wander = true;
    public bool Stunned;
    public GameObject DeathEffect;
    private Rigidbody2D rb;
    bool toggleVisiblity;
    Vector2 areaToWalk;
    BoxCollider2D boxToPatrol;
    bool wanderSus;
    public bool Sus;
    public float chasingRange;
    Vector2 lastSeenPlayer;
    public float maxDurationSus;
    public float currentDurationSus;
    Animator anim;

    public float cooldownChasingMove;
    public float maxCooldownChasingMove;

    public float radiusForPlayerChase;
    public int moneyReward;
    public GameObject GFX;
    float HpDiffer;

    public Item itemPigman1;
    public Item itemPigman2;
    public Item itemPigman3;
    CircleCollider2D col;
    bool hurt;
    bool healed;
    Color originalColor;
    Color newColor;
    bool walked;
    public Item medicItem;
    public enum EnemiesType
    {
        Goblin,
        Soldier,
        MedicSoldier

    }
    public enum QuestHuntingEnemy
    {
        None,
        GoblinQuest,
        GoblinQuest2,
        GoblinQuestBatholo1,
        RoutineGoblin,
        GoblinPigman,
    }

    public QuestHuntingEnemy questingEnem;
    void Start()
    {
        HpDiffer = hp.currentHP;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        path = GetComponent<AIPath>();
        audioManage = GetComponent<AudioSource>();
        Transform child = transform.Find("LineOfSight");
        lineofSight = child.GetComponent<LineOfSight>();
        gun.SetActive(false);
        ReSetVisibility();
        //inv = FindObjectOfType<Inventory>();
        player = GameObject.Find("Player").transform;
        anim = GetComponentInChildren<Animator>();
        playerQ = GameObject.Find("Player").GetComponent<Player>();
        startPos = transform.position;
        originalColor = newColor;
        //  Stunning(2);

    }
    private void Awake()
    {
        
    }
    // Update is called once per frame

    
    void Update()
    {
        lastSeenPlayer = player.position;
        startPos = transform.position;
        if (!Stunned)
        {
            
                path.canMove = true;
                if (isSaw && !takingCover)
                {

                    gun.SetActive(true);
                    CheckEnemyBehaviour();

                }
                else if (isSaw && takingCover)
                {
                    gun.SetActive(true);
                    if (shoot != null)
                    {
                        shoot.Shooting();
                    }
                    else if (shootSMG != null)
                    {
                        shootSMG.Shooting();
                    }
                    else if (melee != null)
                    {
                        melee.Slasher();
                    }
                }
                else
                {
                    if (wander && !Sus)
                    {
                   
                        Wandering();
                        StartCoroutine(wanderTiming());

                    }
                    else if(!Sus)
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
                        SusMove();
                       currentDurationSus -= Time.deltaTime;
                        lineofSight.enabled = true;
                    lineofSight.aleartImage.GetComponent<SpriteRenderer>().color = Color.blue;
                         if (currentDurationSus <= 0)
                        {
                            
                            wander = true;
                            Sus = false;
                             
                        }
                       
                        
                    }
                }


            
        }
        else
        {
            enemyAnim.anim.SetTrigger("Alert");
            gun.SetActive(false);
            path.canMove = false;
            StartCoroutine(StunFlash());
            path.destination = startPos;
            rb.velocity = Vector3.zero;
        }
        
        if (hp.currentHP <= 0)
        {
            HPout();
            if(this.enemiesType == EnemiesType.MedicSoldier)
            {
                ItemWorld.SpawnItemWorld(this.transform.position, medicItem);
;            }
        }


    }
    void CheckEnemyBehaviour()
    {
        switch (enemiesType)
        {
            case EnemiesType.Goblin:
                    AlertNonTactic();
                break;
            case EnemiesType.Soldier:
                AlertTactic();
                break;
            case EnemiesType.MedicSoldier:
                AlertMedic();
                break;


        }
    }
    public void SetVisibility(bool isSaw)
    {
        toggleVisiblity = isSaw;
        
        if (toggleVisiblity)
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            SpriteRenderer[] GunHolder = gameObject.transform.Find("WeaponHolder").GetComponentsInChildren<SpriteRenderer>();
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.SetActive(true);
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            colorNotVisible.a  = 1;
            foreach(SpriteRenderer gunSprite in GunHolder)
            {
                gunSprite.color = colorNotVisible;
            }
            sprite.color = colorNotVisible;
            //Shadow.color = colorNotVisible;
        }
        else
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            SpriteRenderer[] GunHolder = gameObject.transform.Find("WeaponHolder").GetComponentsInChildren<SpriteRenderer>();
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.SetActive(false);
            colorNotVisible.a = 0;
            foreach (SpriteRenderer gunSprite in GunHolder)
            {
                gunSprite.color = colorNotVisible;
            }
            sprite.color = colorNotVisible;
            
        }
    }
    public void ReSetVisibility()
    {
        SetVisibility(false);
    }
    public IEnumerator StunFlash()
    {
        SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        sprite.color = Color.yellow;
        Color colorFade = sprite.GetComponent<SpriteRenderer>().color;
        colorFade.a = 0.1f;
        sprite.color = colorFade;
        yield return new WaitForSeconds(0.2f);
        colorFade.a = 1f;
        sprite.color = colorFade;
        sprite.color = hp.originalColor;
        yield return new WaitForSeconds(0.2f);
        colorFade.a = 0.1f;
        sprite.color = colorFade;
        sprite.color = Color.yellow;
        yield return new WaitForSeconds(0.2f);
        colorFade.a = 1f;
        sprite.color = colorFade;
        sprite.color = hp.originalColor;
        yield return new WaitForSeconds(0.2f);
        colorFade.a = 0.1f;
        sprite.color = colorFade;
        sprite.color = Color.yellow;
        yield return new WaitForSeconds(0.2f);
        colorFade.a = 1f;
        sprite.color = colorFade;
        sprite.color = hp.originalColor;
    }
    public void Stunning(float duration)
    {
        StartCoroutine(Stun(duration));
    }
    IEnumerator Stun(float duration)
    {
        Stunned = true;
        yield return new WaitForSeconds(duration);
        Stunned = false;
    }
    IEnumerator wanderTiming()
    {
        wander = false;
        yield return new WaitForSeconds(5);
        wander = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && hp.currentHP < HpDiffer && !isSaw)
        {
            enemyAnim.anim.SetTrigger("Alert");
            HpDiffer = hp.currentHP;
        }
    }
    
    void Wandering()
    {
        wanderingTime += Time.deltaTime;
        if(wanderingTime >= wanderingDuration && !isSaw)
        {
            
            path.destination = areaToWalk;
            path.maxSpeed = speed - 0.5f;
            wanderingTime = 0;
            GetBoxToWander(boxToPatrol);
            
        }
    }
    public BoxCollider2D GetBox(BoxCollider2D patrolBox)
    {
        return patrolBox;
    }
    void GFXToggle()
    {
        switch (questingEnem)
        {
            default:
            case QuestHuntingEnemy.None:
                return;
            case QuestHuntingEnemy.GoblinQuest:
                GFX.SetActive(true);
                break;
            case QuestHuntingEnemy.GoblinQuest2:
                GFX.SetActive(false);
                break;
        }
    }
    void HPout()
    {
        playerQ.moneyCount += moneyReward;
        Instantiate(DeathEffect, gameObject.transform.position, Quaternion.identity);
        DeathEffectInteract.Instance.KilledEffect();
        switch (questingEnem)
        {
            default:
            case QuestHuntingEnemy.None:
            //    playerQ.moneyCount += moneyReward;
                Destroy(gameObject,0.01f);
                return;
            case QuestHuntingEnemy.GoblinQuest:
              //  playerQ.moneyCount += moneyReward;
                if (QuestManager.Instance.activeQuests.Count > 0)
                {
                    List<Quest> questing1 = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.activeQuests)
                    {

                        if (quest.goal.goalType == GoalType.KillGoblin)
                        {
                            quest.goal.QuestGoblinKilled();
                            if (quest.goal.IsReached())
                            {

                                questing1.Add(quest);
                                QuestLogUI.Instance.QuestUpdated(quest);
                                //inv.AddItem(npcQuest.itemReward);
                            }
                        }
                    }
                   
                        foreach (Quest quest in questing1)
                        {
                            QuestManager.Instance.CompletedQuests(quest);
                        }
                    
                }
                Destroy(gameObject,0.01f);

                // Destroy(gameObject);
                break;
            case QuestHuntingEnemy.GoblinQuest2:
               // playerQ.moneyCount += moneyReward;

                if (QuestManager.Instance.activeQuests.Count > 0)
                {
                    List<Quest> questing2 = new List<Quest>();
                    foreach (Quest quest in QuestManager.Instance.activeQuests)
                    {

                        if (quest.goal.goalType == GoalType.KillGoblin2)
                        {

                            quest.goal.QuestGoblinKilled2();
                            if (quest.goal.IsReached())
                            {

                                questing2.Add(quest);
                                //inv.AddItem(npcQuest.itemReward);
                            }


                        }

                    }
                    {
                        foreach (Quest quest in questing2)
                        {
                            QuestManager.Instance.CompletedQuests(quest);
                        }
                    }

                }
                Destroy(gameObject,0.01f);
                break;
            case QuestHuntingEnemy.GoblinPigman:
                int random = Random.Range(0, 2);
                if (random == 0)
                {
                    ItemWorld.SpawnItemWorld(this.transform.position, itemPigman1);
                }
                else if (random == 1)
                {
                    ItemWorld.SpawnItemWorld(this.transform.position, itemPigman2);
                }
                else if (random == 2)
                {
                    ItemWorld.SpawnItemWorld(this.transform.position, itemPigman3);
                }
                Destroy(gameObject, 0.01f);
                break;
       

        }
        
        
    }
    public Vector2 GetBoxToWander(BoxCollider2D box)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        areaToWalk = startPos + randomDirection * coverDetectRadius;
        Vector2 minBound = box.bounds.min;
        Vector2 maxBound = box.bounds.max;

        areaToWalk.x = Mathf.Clamp(areaToWalk.x, minBound.x, maxBound.x);
        areaToWalk.y = Mathf.Clamp(areaToWalk.y, minBound.y, maxBound.y);
        boxToPatrol = box;
        return areaToWalk;
    }
  
    void SusMove()
    {   

        if (!wanderSus)
        {
           
            StartCoroutine(WanderforSus());
           
            path.destination = player.position;
        }
    }
    IEnumerator WanderforSus()
    {
        wanderSus = true;
        yield return new WaitForSeconds(3f);
        wanderSus = false;
    }
    IEnumerator Barrage()
    {
        walked = false;
        path.destination = this.transform.position * Random.insideUnitCircle * 1.5f;
        yield return new WaitForSeconds(1f);
        walked = true;
    }

    public void AlertMedic()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, 10f);
        foreach (Collider2D collider in col)
        {
            if (collider != null)
            {
                if (collider.CompareTag("Enemy"))
                {
                    if (collider.GetComponent<EnemyHP>().currentHP <= collider.GetComponent<EnemyHP>().maxHP / 2)
                    {
                        if (collider.GetComponent<Enemy>().enemiesType == EnemiesType.Soldier)
                        {
                            path.destination = collider.transform.position;
                          
                        }
                    }

                }
             
            }

        }
        Vector3 playerdirection = player.position - transform.position;

        if (playerdirection.x > 0)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            sprite.flipX = true;
        }
        else
        {
            //  transform.localScale = new Vector3(1, 1, 1);
            sprite.flipX = false;
        }
          transform.localScale = new Vector3(1, 1, 1);
    }
    public void AlertTactic()
    {
        if (walked)
        {
            StartCoroutine(Barrage());
        }
     
        if (shoot != null)
        {
            shoot.Shooting();
        }
        else if(shootSMG != null)
        {
            shootSMG.Shooting();
        }
        else if(shootShotgun != null)
        {
            shootShotgun.Shooting();
        }
        Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, 10f);
        foreach (Collider2D collider in col)
        {
            if(collider != null)
            {
                if (collider.CompareTag("Enemy"))
                {
                    if(hp.currentHP <= hp.maxHP / 2)
                    {
                        if (collider.GetComponent<Enemy>().enemiesType == EnemiesType.MedicSoldier && !hurt)
                        {

                            path.destination = collider.transform.position;
                            hurt = true;
                            walked = false;
                        }
                    }
                   
                }
               
            }
            
        }
        Collider2D[] collide = Physics2D.OverlapCircleAll(this.transform.position, 5);
        foreach (Collider2D colliding in collide)
        {
            if (colliding != null)
            {
                if (colliding.CompareTag("Enemy"))
                {
                    if (hp.currentHP <= hp.maxHP / 2)
                    {
                        if (colliding.GetComponent<Enemy>().enemiesType == EnemiesType.MedicSoldier && hurt && !healed)
                        {
                            StartCoroutine(Healing());
                            healed = true;
                            walked = false;
                        }
                     
                    }
                    else if (healed)
                    {
                        path.destination = colliding.transform.position * Random.insideUnitCircle * 0.5f;
                    }

                }
            }
        }
        Vector3 playerdirection = player.position - transform.position;

        if (playerdirection.x > 0)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            sprite.flipX = true;
        }
        else
        {
            //  transform.localScale = new Vector3(1, 1, 1);
            sprite.flipX = false;
        }
          transform.localScale = new Vector3(1, 1, 1);
    }
    public IEnumerator Healing()
    {
        SpriteRenderer spriteRen = GetComponentInChildren<SpriteRenderer>();
        spriteRen.color = hp.originalColor;
        yield return new WaitForSeconds(0.5f);
        spriteRen.color = Color.green;
        //yield return new WaitForSeconds(0.1f);
    
        hp.currentHP += 10;
        yield return new WaitForSeconds(0.5f);
        spriteRen.color = hp.originalColor;
        hp.currentHP += 10;
        yield return new WaitForSeconds(0.5f);
        spriteRen.color = Color.green;
        hp.currentHP += 10;
        yield return new WaitForSeconds(0.5f);
        spriteRen.color = hp.originalColor;
    }
    public void AlertNonTactic()
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
        Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, 10f);
        foreach (Collider2D collider in col)
        {
            NPCShadow npc = collider.GetComponent<NPCShadow>();

            if (collider.CompareTag("NPCShadow"))
            {
                if (npc != null)
                {
                    npc.StartRunningAway();
                }
            }
        }
        cooldownChasingMove -= Time.deltaTime;
        // Vector2 direction = (player.position - transform.position).normalized;
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, radius, LayerMask.GetMask("BlockPath"));
        transform.localScale = new Vector3(1, 1, 1);
        Sus = false;
        lineofSight.aleartImage.GetComponent<SpriteRenderer>().color = Color.red;

        Vector3 playerdirection = player.position - transform.position;

        if (playerdirection.x > 0)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            sprite.flipX = true;
        }
        else
        {
            //  transform.localScale = new Vector3(1, 1, 1);
            sprite.flipX = false;
        }

           

        if (shoot != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                if(cooldownChasingMove <= 0)
                {
                    Vector2 randomPlayer = Random.insideUnitCircle.normalized;
                    Vector2 playerPosition = player.position;
                    areaToWalk = playerPosition + randomPlayer * radiusForPlayerChase;
                    path.destination = areaToWalk;
                    cooldownChasingMove = maxCooldownChasingMove;
                }
                path.maxSpeed = speed;
                shoot.Shooting();
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                path.canMove = true;
                if (cooldownChasingMove <= 0)
                {
                    Vector2 randomPlayer = Random.insideUnitCircle.normalized;
                    Vector2 playerPosition = player.position;
                    areaToWalk = playerPosition + randomPlayer * radiusForPlayerChase;
                    path.destination = areaToWalk;
                    cooldownChasingMove = maxCooldownChasingMove;
                }
                path.maxSpeed = speed;
                // anim.SetBool("isMoving", false);
                shoot.Shooting();
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                path.canMove = false;
                lastSeenPlayer = player.position;
                rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime));
               // anim.SetBool("isMoving", true);
                shoot.Shooting();
            }
          



        }
        else if (shootSMG != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                path.canMove = true;
                if (cooldownChasingMove <= 0)
                {
                    Vector2 randomPlayer = Random.insideUnitCircle.normalized;
                    Vector2 playerPosition = player.position;
                    areaToWalk = playerPosition + randomPlayer * radiusForPlayerChase;
                    path.destination = areaToWalk;
                    cooldownChasingMove = maxCooldownChasingMove;
                }
                path.maxSpeed = speed;
                shootSMG.Shooting();
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                path.canMove = true;
                if (cooldownChasingMove <= 0)
                {
                    Vector2 randomPlayer = Random.insideUnitCircle.normalized;
                    Vector2 playerPosition = player.position;
                    areaToWalk = playerPosition + randomPlayer * radiusForPlayerChase;
                    path.destination = areaToWalk;
                    cooldownChasingMove = maxCooldownChasingMove;
                }
                lastSeenPlayer = player.position;
                shootSMG.Shooting();
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                path.canMove = true;
                path.destination = player.position;
                lastSeenPlayer = player.position;
                rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime));
                shootSMG.Shooting();
            }
         


                
        }
        else if (shootShotgun != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                path.canMove = true;
                if (cooldownChasingMove <= 0)
                {
                    Vector2 randomPlayer = Random.insideUnitCircle.normalized;
                    Vector2 playerPosition = player.position;
                    areaToWalk = playerPosition + randomPlayer * radiusForPlayerChase;
                    path.destination = areaToWalk;
                    cooldownChasingMove = maxCooldownChasingMove;
                }
                path.maxSpeed = speed;
                shootShotgun.Shooting();
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                path.canMove = true;
                if (cooldownChasingMove <= 0)
                {
                    Vector2 randomPlayer = Random.insideUnitCircle.normalized;
                    Vector2 playerPosition = player.position;
                    areaToWalk = playerPosition + randomPlayer * radiusForPlayerChase;
                    path.destination = areaToWalk;
                    cooldownChasingMove = maxCooldownChasingMove;
                }
                lastSeenPlayer = player.position;
                shootShotgun.Shooting();
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                path.canMove = true;
                path.destination = player.position;
                lastSeenPlayer = player.position;
                rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime));
                shootShotgun.Shooting();
            }
        }
        else if (melee != null)
        {
            path.canMove = true;
            path.destination = player.position;
            melee.Slasher();
        }

         if (Vector2.Distance(transform.position, player.position) >= chasingRange) 
        {
            currentDurationSus = maxDurationSus;
            Sus = true;
            isSaw = false;
            takingCover = false;
            
        }


    }
    

    
}


