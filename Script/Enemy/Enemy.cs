using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour

{
    public AudioSource audio;
    public AudioClip Gah;
    private AIPath path;
    public float speed;
    public float coverDetectRadius = 5f;
    public EnemyHP hp;
    public EnemiesType enemiesType;
    public Player playerQ;
    public Quest quest;
    public EnemyShooting shoot;
    public EnemyShootingAutomatic shootSMG;
    public EnemyAnim enemyAnim;
    public bool isSaw = false;
    public GameObject gun;

    Transform coverPos;
    public float wanderingTime;
    private float wanderingDuration;
    private Vector2 wanderPos;
    Vector2 startPos;
    private bool takingCover = false;
    private Transform player;
    LineOfSight lineofSight;
    bool wander = true;

    
    public enum EnemiesType
    {
        Goblin,
        Soldier,
        GoblinQuest,
        GoblinQuest2,

    }
    void Start()
    {
        path = GetComponent<AIPath>();
        audio = GetComponent<AudioSource>();
        Transform child = transform.Find("LineOfSight");
        lineofSight = child.GetComponent<LineOfSight>();
        gun.SetActive(false);
        //inv = FindObjectOfType<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        playerQ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        startPos = transform.position;
        if (isSaw && !takingCover)
        {

            gun.SetActive(true);
            Alert();
          
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
        }
        else
        {
            if (wander)
            {
                Wandering();
                StartCoroutine(wanderTiming());
            }
        }
        if(hp.currentHP < hp.maxHP && !isSaw)
        {
            enemyAnim.anim.SetTrigger("Alert");
        }
        if(hp.currentHP <= 0)
        {
            HPout();
        }
        
    }
    IEnumerator wanderTiming()
    {
        wander = false;
        yield return new WaitForSeconds(5);

        wander = true;
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cover") && isSaw && !takingCover && collision.GetComponent<TakeCoverPos>().takeCover == false)
        {
            
            
                takingCover = true;
                collision.GetComponent<TakeCoverPos>().takeCover = true;
                path.destination = collision.transform.position;
                if(path.endReachedDistance <= 0)
                {
                    path.isStopped = true;
                }
                
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cover"))
        {
            path.isStopped = false;
            takingCover = false;
        }
    }

    void Wandering()
    {
        wanderingTime += Time.deltaTime;
        if(wanderingTime >= wanderingDuration && !isSaw)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            wanderPos = startPos + randomDirection * coverDetectRadius;
            path.destination = wanderPos;
            wanderingTime = 0;
        }
    }
    void HPout()
    {
        switch (enemiesType)
        {
            default:
            case EnemiesType.GoblinQuest:
                List<Quest> questing1 = new List<Quest>();
                playerQ.moneyCount += 50;
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {

                    if (quest.goal.goalType == GoalType.KillGoblin)
                    {
                            quest.goal.QuestGoblinKilled();
                        if (quest.goal.IsReached())
                        {

                            questing1.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);
                        }
                    }
                }
                foreach(Quest quest in questing1)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                }
                break;
            case EnemiesType.GoblinQuest2:
                playerQ.moneyCount += 50;
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
                foreach (Quest quest in questing2)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                }
                break;
            case EnemiesType.Goblin:
                playerQ.moneyCount += 50;
                Debug.Log("Arghhh!");
                break;
            case EnemiesType.Soldier:
                playerQ.moneyCount += 150;
                Debug.Log("Noo!");
                break;

        }

        Destroy(gameObject);
    }

    public void Alert()
        {
        // Vector2 direction = (player.position - transform.position).normalized;
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, radius, LayerMask.GetMask("BlockPath"));
        
            path.destination = player.position;
            Vector3 playerdirection = player.position - transform.position;

            if (playerdirection.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        
        

        //   if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        //   {
        //       transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //   }
        //   else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        //   {
        //       transform.position = this.transform.position;
        //   }
        //   else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        //   {
        //        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        //    }

        

        if (shoot != null)
        {
            shoot.Shooting();
        }
        else if (shootSMG != null)
        {
            shootSMG.Shooting();
        }




    }
    

    IEnumerator AlertSound()
    {
        audio.clip = Gah;
        audio.Play();
        yield return new WaitForSeconds(3);
        audio.Stop();
    }
}


