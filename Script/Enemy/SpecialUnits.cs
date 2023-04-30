using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class SpecialUnits : MonoBehaviour
{
    public AIPath path;
    public Animator anim;
    public Transform player;
    public bool isSaw = false;
    public bool canMove = false;
    private EnemyHP hp;
    Transform shootingPoint;
    Rigidbody2D rb;
    public float DashSpeed;

    //MechaGoblin
    public GameObject Missile;
    int missile = 0;
    int maxmissile = 4;
    public float fireRate;
    public bool special;
    public bool armed = true;
    public bool chainSaw = false;
    public GameObject damagingArea;
    bool isDashing = false;
    //

    Vector3 lastPos;
    public enum SpecialUnitType
    {
        MechaGoblin,
        Trapper,
        SuperSoldier,
        Turret
    }
    public SpecialUnitType unit;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();
        lastPos = transform.position;
        hp = this.GetComponent<EnemyHP>();
        anim = transform.GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootingPoint = transform.Find("shootingPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.currentHP < hp.maxHP && !isSaw && canMove)
        {
            anim.SetTrigger("Alert");

        }
        if (isSaw)
        {
            EnemiesBehaviour();
        }
        if(hp.currentHP <= 0)
        {
            HPOut();
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
                    if(missile >= maxmissile)
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
                else if(!canMove && !armed && !special && !chainSaw)
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
        }
    }
    
    void HPOut()
    {
        // switch (unit)
        //  {
        //     default: 
        //         case SpecialUnitType.MechaGoblin
        //  }
        Destroy(gameObject);
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
