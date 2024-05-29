using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss_Pigman : MonoBehaviour
{
    public Animator anim;
    EnemyHP enemyHP;
    public Transform player;
    public AIPath path;


    public GameObject SummonEffect;

    public GameObject[] Goblins;
    List<GameObject> SawGoblins = new List<GameObject>();
    public BoxCollider2D ThisCollider;

    public Rigidbody2D rb;
    public float cooldownInvincible;
    public float MaxcooldownInvincible;
    public bool canInvincible = false;
    public bool Invincibled;

    public float cooldownRoaring;
    public float MaxcooldownRoaring;
    public bool canRoar = false;
    public bool Roared;

    public int GoblinsSummonCount = 3;
    public int GoblinCurrent = 0;

    public bool isDead;

    public bool playerInRange;

    public bool ranged;
    public float cooldownRanged;
    public float MaxcooldownRanged;
    public bool canRanged = false;

    bool canMove;

    public float bulletCount;
    public float bulletRadius;

    public GameObject bulletPrefab;
    public BoxCollider2D boxToPatrol;

    bool punched;
    float maxSceneDead;
    float currentSceneDead;
    public GameObject deathEffect;
    bool death;

    AudioSource audioSound;
    public AudioClip MinigunSound;
    public AudioClip Roar;
    public AudioClip Invincible;
    public AudioClip Punch;
    BoxCollider2D box;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        anim = gameObject.transform.Find("Sprite").GetComponent<Animator>();
        enemyHP = GetComponent<EnemyHP>();
        player = FindObjectOfType<Player>().transform;
        path = GetComponent<AIPath>();
        audioSound = GetComponent<AudioSource>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    IEnumerator Ending()
    {
        box.enabled = false;
        box.enabled = false;
        GameManager.Instance.PigmanDefeated = true;
        List<Quest> questing = new List<Quest>();

        foreach (Quest quest in QuestManager.Instance.activeQuests)
        {

            if (quest.goal.goalType == GoalType.BatholoQuest4)
            {


                questing.Add(quest);
                //inv.AddItem(npcQuest.itemReward);

            }
        }
        foreach (Quest quest in questing)
        {
            QuestManager.Instance.CompletedQuests(quest);
        }

        yield return new WaitForSeconds(3.5f);

        //GameManager.Instance.Pigman = false;
        Instantiate(deathEffect, this.transform.position, Quaternion.identity);
        Destroy(gameObject, 0.01f);

        SaveManager.Instance.SaveAll();
        SaveManager.Instance.OutSide_Pigman();
        SaveManager.Instance.LoadToScene();
       
    }
    void Update()
    {
        if (!GameManager.Instance.PigmanDefeated)
        {
            if (canMove && !Roared && !Invincibled && !playerInRange && !ranged)
            {
                // path.canMove = true;
                anim.SetBool("isWalking", true);
            }
            else
            {
                // path.canMove = false;
                anim.SetBool("isWalking", false);
            }
            if (enemyHP.currentHP <= 1500)
            {
                RoaringLogic();
            }
            if (enemyHP.currentHP <= 2000)
            {
                InvincibleLogic();
            }
            if (Roared)
            {
                anim.SetBool("Roaring", true);
            }
            else
            {
                anim.SetBool("Roaring", false);
            }
            if (enemyHP.currentHP <= 0 && !GameManager.Instance.PigmanDefeated)
            {
                StartCoroutine(Ending());
            }

            if (Invincibled)
            {
                anim.SetBool("Invincible", true);
            }
            else
            {
                anim.SetBool("Invincible", false);
            }
            RangedLogic();
            if (ranged)
            {
                anim.SetBool("Ranged", true);
            }
            else
            {
                anim.SetBool("Ranged", false);
            }
            if (playerInRange)
            {
                if (!punched)
                {
                    StartCoroutine(Punching());
                }

                anim.SetBool("Melee", true);
            }
            else
            {
                anim.SetBool("Melee", false);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("Melee", false);
            anim.SetBool("Ranged", false);
            anim.SetBool("Invincible", false);
            anim.SetBool("Roaring", false);
            Roared = false;
            Invincibled = false;
            punched = true;
            playerInRange = false;
            ranged = false;
            path.canMove = false;
            rb.velocity = Vector2.zero;
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
            maxSceneDead = 1.5f;
            currentSceneDead -= Time.deltaTime;
            if(currentSceneDead <= 0)
            {

                DeathEffectInteract.Instance.KilledEffect();
                currentSceneDead = maxSceneDead;
            }
           
        }
      
    }

    public void Minigun()
    {
       
        float angleStep = 360f / bulletCount;
        for(int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector2 spawnPos = bulletCircle(angle);
           
            GameObject obj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.up;
            obj.GetComponent<Rigidbody2D>().velocity = bulletDirection * 6;
        }
       

    }
   
    public void RoarSound()
    {
        audioSound.clip = Roar;
        if (!audioSound.isPlaying)
        {
            audioSound.Play();
        }
    }
    public void Gunning()
    {
       
        StartCoroutine(spawnBullet());
    }
    IEnumerator spawnBullet()
    {
        
        yield return new WaitForSeconds(1f);
        Minigun();
        audioSound.PlayOneShot(MinigunSound);
        yield return new WaitForSeconds(0.2f);
        Minigun();
        audioSound.PlayOneShot(MinigunSound);
        yield return new WaitForSeconds(0.2f);
        Minigun();
        audioSound.PlayOneShot(MinigunSound);

    }
    Vector2 bulletCircle(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = transform.position.x + bulletRadius + Mathf.Cos(radians);
        float y = transform.position.y + bulletRadius * Mathf.Sin(radians);
        return new Vector2(x, y);
    }
    
    public IEnumerator Punching()
    {
      
        punched = true;
        yield return new WaitForSeconds(0.8f);
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 7f);
        foreach(Collider2D col in collider)
        {
            if (col.CompareTag("Player"))
            {
                PlayerHP play = col.GetComponent<PlayerHP>();
                if (col != null)
                {
                    audioSound.PlayOneShot(Punch);
                    play.TakeDamage(25);
                }

            }
        }
        
        
        yield return new WaitForSeconds(0.5f);
        punched = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

        }
    }

   

    void RangedLogic()
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
        if (!playerInRange)
        {
            cooldownRanged -= Time.deltaTime;
        }
        
       
        if (cooldownRanged <= 0 && !ranged)
        {
            ranged = true;

            //anim.SetTrigger("Roaring");
            // cooldownRoaring = MaxcooldownRoaring;
        }
    }
    void RoaringLogic()
    {
        //  float cooldownDecrement = Time.deltaTime / Time.timeScale;
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
        if (!playerInRange)
        {
            cooldownRoaring -= Time.deltaTime;
        }
        
        //  cooldownRoaring = Mathf.Clamp(cooldownRoaring, 0f, MaxcooldownRoaring);
        if (cooldownRoaring <= 0 && !Roared)
        {
            Roared = true;
           
            //anim.SetTrigger("Roaring");
            // cooldownRoaring = MaxcooldownRoaring;
        }
       
        
        
    }
    void InvincibleLogic()
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
        if (!playerInRange)
        {
            cooldownInvincible -= Time.deltaTime;
        }

        //   float cooldownDecrement = Time.deltaTime / Time.timeScale;

        // cooldownRoaring = Mathf.Clamp(cooldownInvincible, 0f, MaxcooldownInvincible);
        if (cooldownInvincible <= 0 && !Invincibled)
        {
            Invincibled = true;
            //anim.SetTrigger("Invincible");
          //  cooldownRoaring = MaxcooldownInvincible;
        }
      

    }
    public void StartSpawningGoblins()
    {
        StartCoroutine(SpawningGoblins());
    }
    public IEnumerator SpawningGoblins()
    {

        while (GoblinCurrent < GoblinsSummonCount)
        {
            int i =  Random.Range(0, Goblins.Length);
            Vector2 randomCircle = Random.insideUnitCircle.normalized * 3f;
            Vector2 spawnPos = (Vector2)gameObject.transform.position + randomCircle;
            GameObject newGoblin = Instantiate(Goblins[i], spawnPos, Quaternion.identity);
            newGoblin.GetComponent<Enemy>().isSaw = true;
            newGoblin.GetComponent<Enemy>().GetBoxToWander(boxToPatrol);
            Instantiate(SummonEffect, newGoblin.transform.position, Quaternion.identity);
            SawGoblins.Add(newGoblin);
            //newGoblin.GetComponent<Enemy>().isSaw = true;
          
                yield return new WaitForSeconds(1f);
            GoblinCurrent += 1;
           
        }
    }

    public void WaitingForSound()
    {
        StartCoroutine(waitForInvincible());
    }
    IEnumerator waitForInvincible()
    {
        yield return new WaitForSeconds(1f);
        audioSound.PlayOneShot(Invincible);
    }
   
    public void StopChase()
    {
        canMove = false;
        path.canMove = false;
    }
    public void ChaseLogic()
    {
        canMove = true;
        Roared = false;
        Invincibled = false;
        path.destination = player.transform.position;
        path.canMove = true;
        anim.SetBool("isWalking", true);
        
    }
    public void CooldownInvincible()
    {
        cooldownInvincible = MaxcooldownInvincible;
    }
    public void CooldownRanged()
    {
        cooldownRanged = MaxcooldownRanged;
    }
    public void CooldownRoaring()
    {
        cooldownRoaring = MaxcooldownRoaring;
    }
}
