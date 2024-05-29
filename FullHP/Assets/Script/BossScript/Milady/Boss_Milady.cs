using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Milady : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float maxCooldownJump;
    public float currentJump;

    public float maxBlock;
    public float currentBlock;
    EnemyHP hp;
    SpriteRenderer sprite;
    public LayerMask target;
    public float range;
    public GameObject deathEffect;
    float maxSceneDead;
    float currentSceneDead;
    public Quest quest11;
    public bool Attacked;
    public GameObject bulletPrefab;
    bool shadowToggle;
    GameObject Shadow;
    AudioSource audioSound;
    public AudioClip deflectSound;
    public AudioClip AttackSound;
    public AudioClip JumpDownSound;
    public AudioClip JumpUpSound;
    BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        hp = GetComponent<EnemyHP>();
        Shadow = transform.Find("Shadow").gameObject;
        audioSound = GetComponent<AudioSource>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FacingPlayer();
        Logic();
        ControllingHP();
    }
    public void Deflecting()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 1.2f);
        foreach(Collider2D col in collider)
        {
            if(col != null)
            {
                if (col.CompareTag("Bullet"))
                {
                    Destroy(col.gameObject);
                    GameObject deflectBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                    Rigidbody2D bulletRB = deflectBullet.GetComponent<Rigidbody2D>();
                    Player player = FindObjectOfType<Player>();
                    Vector2 dir = (player.transform.position -this.transform.position).normalized;
                    bulletRB.velocity = dir * 8;
                }
            }
        }
    }
    public void ShadowToggle(bool toggle)
    {
        shadowToggle = toggle;
        if (shadowToggle)
        {
            Shadow.SetActive(true);
        }
        else
        {
            Shadow.SetActive(false);
        }

    }
    public void ControllingHP()
    {
        if (hp.currentHP <= 0 && !GameManager.Instance.Milady)
        {
            StartCoroutine(Ending());
        }
    }
    IEnumerator Ending()
    {
        box.enabled = false;
        GameManager.Instance.MiladyDefeated = true;
        List<Quest> questing = new List<Quest>();

        foreach (Quest quest in QuestManager.Instance.activeQuests)
        {

            if (quest.goal.goalType == GoalType.BatholoQuest10)
            {


                questing.Add(quest);
                //inv.AddItem(npcQuest.itemReward);

            }
        }
        foreach (Quest quest in questing)
        {
            QuestManager.Instance.CompletedQuests(quest);

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
        maxSceneDead = 1.5f;
        currentSceneDead -= Time.deltaTime;
        if (currentSceneDead <= 0)
        {

            DeathEffectInteract.Instance.KilledEffect();
            currentSceneDead = maxSceneDead;
        }
        yield return new WaitForSeconds(3.5f);

        //GameManager.Instance.Pigman = false;
        Instantiate(deathEffect, this.transform.position, Quaternion.identity);
        Destroy(gameObject, 0.01f);
        QuestManager.Instance.ActiveQuests(quest11);
        SaveManager.Instance.SaveAll();
        SaveManager.Instance.Outside_Milady();
        SaveManager.Instance.LoadToScene();

    }
    void Logic()
    {
        if (!GameManager.Instance.MiladyDefeated)
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
            currentJump -= Time.deltaTime;
            if (!Attacked)
            {
                currentBlock -= Time.deltaTime;
            }

            if (currentJump <= 0 && currentBlock > 0)
            {
                anim.SetTrigger("Jump");
                currentJump = maxCooldownJump;
            }
            else if (currentBlock <= 0)
            {
                anim.SetTrigger("Block");
                currentBlock = maxBlock;
            }
        }
       
    }
    public void FindPlayer()
    {
        Player player = FindObjectOfType<Player>();
        transform.position = player.transform.position;
    }
    public void FacingPlayer()
    {
        Player player = FindObjectOfType<Player>();
        Vector3 playerdirection = player.transform.position - transform.position;
        if (playerdirection.x > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }



    }
    public void JumpSound()
    {
        audioSound.PlayOneShot(JumpUpSound);
    }
    public void DownSound()
    {
        audioSound.PlayOneShot(JumpDownSound);
    }
    public void DeflectSound()
    {
        audioSound.PlayOneShot(deflectSound);
    }
    public void attackSound()
    {
        audioSound.PlayOneShot(AttackSound);
    }
    public void TargetingAttack()
    {
        if (!Attacked)
        {
            Player player = FindObjectOfType<Player>();
            Vector3 playerdirection = player.transform.position - transform.position;
            Vector2 origin = transform.position;
            Vector2 raycastDirLeft = Vector2.left;
            Vector2 raycastDirRight = Vector2.right;
            if (playerdirection.x > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(origin, raycastDirRight, range, target);
                if (hit.collider != null)
                {
                    PlayerHP playerHP = hit.collider.GetComponent<PlayerHP>();
                    if (playerHP != null)
                    {
                        playerHP.TakeDamage(10);
                        Attacked = true;
                    }
                }
                sprite.flipX = true;

            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(origin, raycastDirLeft, range, target);
                if (hit.collider != null)
                {
                    PlayerHP playerHP = hit.collider.GetComponent<PlayerHP>();
                    if (playerHP != null)
                    {
                        playerHP.TakeDamage(10);
                        Attacked = true;
                    }
                }
                sprite.flipX = false;
            }
        }

    }
}
