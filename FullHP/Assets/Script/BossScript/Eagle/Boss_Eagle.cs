using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Eagle : MonoBehaviour
{
    public Transform pos2;
    public Transform pos3;

    EnemyHP enemyHP;

    public GameObject Flash;

    public Rigidbody2D rb;
    public LineRenderer linerenderer;
    public float Damage;
    public float Force;
    Animator anim;
    SpriteRenderer sprite;
    bool Back;
    public float HPToNextPos1;
    public float HPToNextPos2;
    public float HPToNextPos3;

    public float maxCoolDownShot;
    public float coolDownShot;

    public float shootRange;

    public Transform shootPoint;
    public LayerMask target;
    public bool isRunning;
    public GameObject bullet;
    Player player;
    public GameObject deathEffect;
    float maxSceneDead;
    float currentSceneDead;
    bool death;
    AudioSource audio;
    public AudioClip shootSoumd;
    public AudioClip flashSound;
    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        enemyHP = GetComponent<EnemyHP>();
        box = GetComponent<BoxCollider2D>();
    }

    public void AnimatorController()
    {
        if (rb.velocity.magnitude > 0.01f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (rb.velocity.y > 0.1f)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;

        }
    }
    public void Flashing()
    {
        audio.PlayOneShot(flashSound);
        GameObject FlashBang = Instantiate(Flash, player.transform.position, Quaternion.identity);
       
        Destroy(FlashBang, 2);
    }
    void ShootLogic()
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
        coolDownShot -= Time.deltaTime;
        if (!isRunning)
        {
            Vector3 currentPosition = linerenderer.GetPosition(1);
            if (coolDownShot > 0 && coolDownShot <= 3)
            {
                linerenderer.enabled = true;
                Vector2 direction = (Vector2)player.transform.position - (Vector2)shootPoint.position;

                RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, shootRange, target);

                // Instantiate the laser prefab
                Vector2 endPosition = hit ? hit.point : (Vector2)shootPoint.position + direction.normalized * shootRange;
              
                Vector3 newPosition = Vector3.Lerp(currentPosition, endPosition, Time.deltaTime * 3);

                // Set laser's start and end positions
                linerenderer.SetPosition(0, shootPoint.position);
                linerenderer.SetPosition(1, newPosition);

                // Optional: Handle hit logic if required
                if (hit)
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        return;
                    }
                    else if (hit.collider.CompareTag("Player"))
                    {
                        return;
                    }
                    // Handle player hit (apply damage, effects, etc.)
                }
            }
            else if (coolDownShot <= 0)
            {
                linerenderer.enabled = false;
                Vector3 shootDirection = (currentPosition - transform.position).normalized;
                GameObject bulletOBJ = Instantiate(bullet, shootPoint.position, Quaternion.identity);
                Rigidbody2D rb = bulletOBJ.GetComponent<Rigidbody2D>();
                rb.velocity = shootDirection * 80;
                Destroy(bulletOBJ, 5);
                StartCoroutine(Shake());
                coolDownShot = maxCoolDownShot;
                audio.PlayOneShot(shootSoumd);
            }
        }
        else
        {
            return;
        }
    
    }
    IEnumerator Shake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        yield return new WaitForSeconds(0.05f);
        this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
    }
    void ControlHP()
    {

        if (enemyHP.currentHP <= HPToNextPos2 && enemyHP.currentHP > HPToNextPos3 && !Back)
        {
            Back = true;
            isRunning = true;

            anim.SetTrigger("Flashing");
            StartCoroutine(MoveToPos1());
            Debug.Log("Back");

        }
        else if (enemyHP.currentHP <= HPToNextPos3 && Back)
        {
            Back = false;
            isRunning = true;
            anim.SetTrigger("Flashing");

            StartCoroutine(MoveToPos2());

        }
        else if (enemyHP.currentHP <= 0)
        {
            anim.SetBool("isMoving", false);
            coolDownShot = maxCoolDownShot;
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
            linerenderer.enabled = false;
            if (!death)
            {
                StartCoroutine(Ending());
                death = true;
            }
            
        }
    }
    IEnumerator Ending()
    {
        box.enabled = false;
        GameManager.Instance.EagleDefeated = true;
        List<Quest> questing = new List<Quest>();

        foreach (Quest quest in QuestManager.Instance.activeQuests)
        {

            if (quest.goal.goalType == GoalType.BatholoQuest8)
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
        SaveManager.Instance.Outside_Eagle();
        SaveManager.Instance.LoadToScene();

    }
    // Update is called once per frame
    private void LateUpdate()
    {
        ShootLogic();
    }
    void Update()
    {
        AnimatorController();
        ControlHP();
      //  ShootLogic();
        if (!Back)
        {
            shootPoint.localPosition = new Vector3(-0.2f, -1.66f, 0);
        }
        else
        {
            shootPoint.localPosition = new Vector3(0.2f, 1.650f, 0);
        }
    }

    public void Pos1()
    {
        StartCoroutine(MoveToPos1());
    }
    public void Pos2()
    {
        StartCoroutine(MoveToPos2());
    }
    IEnumerator MoveToPos1()
    {
        isRunning = true;
        linerenderer.enabled = false;
        yield return new WaitForSeconds(2f);
        anim.SetBool("Back", true);
        rb.velocity = Vector2.up * 5;
        yield return new WaitForSeconds(3.5f);
        linerenderer.enabled = true;
        rb.velocity = Vector2.zero;
        isRunning = false;
        this.transform.position = pos2.position;
    }
    IEnumerator MoveToPos2()
    {
        isRunning = true;
        linerenderer.enabled = false;
        yield return new WaitForSeconds(2f);
        anim.SetBool("Back", false);
        rb.velocity = Vector2.down * 5;
        yield return new WaitForSeconds(3.5f);
        linerenderer.enabled = true;
        rb.velocity = Vector2.zero;
        isRunning = false;
        this.transform.position = pos3.position;
    }
}
