using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Nemesis : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform[] patrolPoints;

    Rigidbody2D rb;
    AIPath path;
    AudioSource audioSound;
    public AudioClip[] sounds;
    public GameObject WeaponHolder;
    EnemyShootingAutomatic shootingScript;
    public Animator anim;
    SpriteRenderer sprite;
    Player player;
    EnemyHP hp;

    Vector2 coverPos;
    Vector2 startPos;
    Vector2 lastseenPlayer;
    Vector2 lastPatrolPos;
     public  bool patrol;
    public bool sus;
    public bool isSaw;
    int posNum;
    public float stoppingDistance;
    public float retreatDistance;
    public float chaseRange;
    bool isRolling;
    public GameObject FlashObject;
    public GameObject DeathEffect;
    public Item item;
    public LineOfSightNemesis los;

    public float cooldownFlash;
    public float maxcooldownFlash;
    public int rateRandomFlash;

    bool toggleVisibility;
    public float cooldownRoll;
    public float maxcooldownRoll;
    public GameObject LightRed;
    float HpDiffer;
    public bool Stunned;
    public GameObject Bible;
    bool spawnBible;
    bool dropped;

    bool isChangedMusic;
    bool spawnedArrow;
    void Start()
    {
        spawnBible = false;
        patrol = true;
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();
        audioSound = GetComponent<AudioSource>();
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        anim = transform.Find("Sprite").GetComponent<Animator>();
        shootingScript = WeaponHolder.GetComponentInChildren<EnemyShootingAutomatic>();
        int randomPos = Random.Range(0, spawnPoints.Length);
        int randomPatrolPos = Random.Range(0, patrolPoints.Length);
        GetPosNum(randomPatrolPos);
        transform.position = spawnPoints[randomPos].position;
        hp = GetComponent<EnemyHP>();
        HpDiffer = hp.currentHP;
        ResetVisibility();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && hp.currentHP < HpDiffer && !isSaw)
        {
            isSaw = true;
            HpDiffer = hp.currentHP;
        }
    }
    Vector2 GetRandomDodgeDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        Vector2 dodgeDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        return dodgeDirection;
    }
    IEnumerator Roll()
    {
        cooldownRoll = maxcooldownRoll;
        isRolling = true;
        path.canMove = false;
        anim.SetTrigger("isRolling");
        rb.velocity = GetRandomDodgeDirection() * 7;
        yield return new WaitForSeconds(2f);
        path.canMove = true;
        isRolling = false;
    }
    private void Update()
    {
        if(this != null && !Stunned)
        {
            Logic();
            AnimLogic();
        }
        else
        {
            StartCoroutine(StunFlash());
            path.canMove = false;
            rb.velocity = Vector2.zero;
        }
    }
    int GetPosNum(int num)
    {
        posNum = num;
        return posNum;
    }
    void AnimLogic()
    {
       
        if (!isRolling)
        {
            if (path.velocity.magnitude > 0.01f)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);

            }
        }
        
       
    }

    public void SetVisibility(bool isSaw)
    {
        toggleVisibility = isSaw;

        if (toggleVisibility)
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            SpriteRenderer[] GunHolder = gameObject.transform.Find("WeaponHolder").GetComponentsInChildren<SpriteRenderer>();
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.SetActive(false);
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            colorNotVisible.a = 1;
            LightRed.SetActive(true);
            foreach (SpriteRenderer gunSprite in GunHolder)
            {
                gunSprite.color = colorNotVisible;
            }
            sprite.color = colorNotVisible;
            
        }
        else
        {
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
            SpriteRenderer[] GunHolder = gameObject.transform.Find("WeaponHolder").GetComponentsInChildren<SpriteRenderer>();
            GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
            Shadow.SetActive(false);
            LightRed.SetActive(false);
            colorNotVisible.a = 0;
            foreach (SpriteRenderer gunSprite in GunHolder)
            {
                gunSprite.color = colorNotVisible;
            }
            sprite.color = colorNotVisible;
        }
    }
    public void ResetVisibility()
    {
        SpriteRenderer sprite = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        Color colorNotVisible = sprite.GetComponent<SpriteRenderer>().color;
        SpriteRenderer[] GunHolder = gameObject.transform.Find("WeaponHolder").GetComponentsInChildren<SpriteRenderer>();
        GameObject Shadow = gameObject.transform.Find("Shadow").gameObject;
        Shadow.SetActive(false);
        colorNotVisible.a = 0;
        LightRed.SetActive(false);
        foreach (SpriteRenderer gunSprite in GunHolder)
        {
            gunSprite.color = colorNotVisible;
        }
        sprite.color = colorNotVisible;
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
    void FlashDirection()
    {
        Vector2 ThrowDirection = (transform.position - player.transform.position).normalized;
        GameObject FlashBang = Instantiate(FlashObject, shootingScript.shootingpoint.position, Quaternion.identity);
        Rigidbody2D FlashBangRB = FlashBang.GetComponent<Rigidbody2D>();
        FlashBangRB.velocity = ThrowDirection.normalized * 15f;

        Destroy(FlashBang, 2);
    }
    // Update is called once per frame
    void Logic()
    {
        foreach(Quest quest in QuestManager.Instance.activeQuests)
        {
            if(quest.goal.goalType == GoalType.BatholoQuest7)
            {
                if (!spawnedArrow)
                {
                     QuestDetecter questDetecter = FindObjectOfType<QuestDetecter>();
                    questDetecter.CreatedArrow(this.transform.position);
                    spawnedArrow = true;
                }
            }
        }
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 5f);
        foreach(Collider2D col in collider)
        {
            if(col != null)
            {
                BulletScript bullet = col.GetComponent<BulletScript>();
                if(bullet != null)
                {
                    isSaw = true;
                    sus = false;
                    patrol = false;
                  
                }
            }
        }
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
        cooldownRoll -= Time.deltaTime;
        cooldownFlash -= Time.deltaTime;
       
        if (Vector2.Distance(player.transform.position, transform.position) > chaseRange && isSaw)
        {
            if (!sus)
            {
                StartCoroutine(SusDuration());
            }


        }
        if (sus)
        {
            SusMove();
           
        }
        if (isSaw)
        {
            Chase();
            if (!isChangedMusic)
            {
                MapSound.Instance.PlayNewSound();
                isChangedMusic = true;
            }
        }
        if (patrol)
        {
            Patrol();
        }
        if(hp.currentHP <= 250 && !spawnBible)
        {
            if (QuestManager.Instance.activeQuests.Count > 0)
            {
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (quest.goal.goalType == GoalType.BatholoQuest7)
                    {
                        QuestDetecter questDetecter = FindObjectOfType<QuestDetecter>();
                        questDetecter.isFollowingArrow = false;
                        Instantiate(Bible, this.transform.position, Quaternion.identity);
                        spawnBible = true;
                    }
                }
            }
        }
        if (hp.currentHP <= 0)
        {



            if (!dropped)
            {
                ItemWorld.SpawnItemWorld(transform.position, item);
                dropped = true;
            }
          
            Instantiate(DeathEffect, gameObject.transform.position, Quaternion.identity);
            DeathEffectInteract.Instance.KilledEffect();
            Destroy(gameObject, 0.01f);
        }
    }

    public void Suspecting()
    {
        StartCoroutine(SusDuration());
    }
    Vector2 GetPlayerPosition()
    {
        return player.transform.position;
    }
    IEnumerator SusDuration()
    {
        sus = true;
        yield return new WaitForSeconds(10);
        sus = false;
        patrol = true;
    }
    void SusMove()
    {
        path.maxSpeed = 3.5f;
        los.enabled = true;
        Debug.Log("Sus!");
        los.aleartImage.SetActive(false);
        shootingScript.enabled = false;
        isSaw = false;
        patrol = false;
        path.destination = lastseenPlayer;
    }
    Vector2 ReturnLastPatrolPos(Vector2 lastPos)
    {
        lastPatrolPos = lastPos;
        return lastPatrolPos;
    }
    

    
    void Chase()
    {
        
        path.canMove = true;
        path.maxSpeed = 3.5f;
        los.aleartImage.SetActive(true);
        los.enabled = false;
        ReturnLastPatrolPos(patrolPoints[posNum].position);
        Vector3 playerdirection = player.transform.position - transform.position;
        transform.localScale = new Vector3(1, 1, 1);
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
        patrol = false;
        sus = false;
        path.destination = GetPlayerPosition();
        lastseenPlayer = GetPlayerPosition();
        WeaponHolder.SetActive(true);
        shootingScript.enabled = true;
        shootingScript.Shooting();
        if(cooldownFlash <= 0)
        {
            rateRandomFlash = Random.Range(0, 10);
            if(rateRandomFlash >= 5)
            {
                FlashDirection();
            }
            cooldownFlash = maxcooldownFlash;
        }
        if (Input.GetMouseButtonDown(0) && cooldownRoll <= 0)
        {
            StartCoroutine(Roll());
        }
        if (!isRolling)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
            {
                path.canMove = true;
                path.destination = player.transform.position;
                //path.maxSpeed = speed;
            }
            else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
            {
                path.canMove = false;
                transform.position = this.transform.position;
               // lastSeenPlayer = player.position;
 
            }
            else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
            {
                path.canMove = false;

                Vector2 random = Random.insideUnitCircle * retreatDistance;
                Vector2 targetPos = (Vector2)player.transform.position + random;
                rb.MovePosition(Vector2.MoveTowards(transform.position, targetPos, -3 * Time.deltaTime));
              
            }
        }
    }
    void Patrol()
    {
        path.maxSpeed = 2f;
        los.enabled = true;
        los.aleartImage.SetActive(false);
        path.destination = patrolPoints[posNum].position;
        WeaponHolder.SetActive(false);
        shootingScript.enabled = false;
        if (path.reachedEndOfPath)
        {
            int randomPatrolPos = Random.Range(0, patrolPoints.Length);
            GetPosNum(randomPatrolPos);
        }

    }
}
