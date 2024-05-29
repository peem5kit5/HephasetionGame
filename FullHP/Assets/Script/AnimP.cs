using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimP : MonoBehaviour
{
    private Animator anim;
    bool rolling = false;
    bool canRoll = true;
    bool ctrlReplacement;
    //public string[] rollDirections = { "RollUp", "RollLeft", "RollDown", "RollRight" };
    public int lastDirection;
    public float rollCurrent;
    public float startRollTimer;
    private Player player;
    public float moveH, moveV;
    public float animRollCooldown;
    ToggleUI ui;
    PlayerHP playerHP;
    bool death;
    bool isanimDead;
    // Start is called before the first frame update
    void Awake()
    {
       
        anim = GetComponent<Animator>();
        playerHP = transform.parent.GetComponent<PlayerHP>();
       // float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
     //   Debug.Log("R1 " + result1);
     //   float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left);
     //   Debug.Log("R2 " + result2);
    //    float result3 = Vector2.SignedAngle(Vector2.up, Vector2.down);
    //    Debug.Log("R3 " + result3);
    }
    private void Start()
    {
        ui = FindObjectOfType<ToggleUI>();
        player = transform.parent.GetComponent<Player>();
        //player = GetComponent<Player>();
    }
    public void Update()
    {
        if (ui.isLayout && !death)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            Vector3 direction = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            moveH = Input.GetAxis("Horizontal");
            moveV = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveH, moveV);
            DashAnim();
            if (angle > -45 && angle <= 45)
            {
                if (!rolling)
                {
                    if (!player.Run)
                    {
                        if (movement != Vector2.zero)
                        {
                            anim.Play("RunE");
                        }
                        else
                        {
                            anim.Play("IdleE");
                        }
                    }
                    else
                    {
                        anim.Play("SprintE");
                    }
                   

                }
                else if (rolling && !player.canRoll)
                {
                    anim.Play("RollRight");
                }
            }
            else if (angle > 45 && angle <= 135)
            {
                if (!rolling)
                {
                    if (!player.Run)
                    {
                        if (movement != Vector2.zero)
                        {
                            anim.Play("RunN");
                        }
                        else
                        {
                            anim.Play("IdleN");
                        }
                    }
                    else
                    {
                        anim.Play("SprintN");
                    }
                   

                }
                else if (rolling && !player.canRoll)
                {
                    anim.Play("RollLeft");
                }
            }
            else if (angle > 135 || angle <= -135)
            {
                if (!rolling)
                {
                    if (!player.Run)
                    {
                        if (movement != Vector2.zero)
                        {
                            anim.Play("RunW");
                        }
                        else
                        {
                            anim.Play("IdleW");
                        }
                    }
                    else
                    {
                        anim.Play("SprintW");
                    }
                }
                else if (rolling && !player.canRoll)
                {
                    anim.Play("RollLeft");
                }
            }
            else if (angle > -135 && angle <= -45)
            {
                if (!rolling)
                {
                    if (!player.Run)
                    {
                        if (movement != Vector2.zero)
                        {
                            anim.Play("RunS");
                        }
                        else
                        {
                            anim.Play("IdleS");
                        }
                    }
                    else
                    {
                        anim.Play("SprintS");
                    }
                    
                }
                else if (rolling && !player.canRoll)
                {
                    anim.Play("RollRight");
                }
            }


            if (Input.GetKeyDown(KeyCode.LeftControl) && player.runStamina > 2)
            {
                ctrlReplacement = true;
            }

         
        }

        if (playerHP.currentHealth <= 0)
        {
            death = true;
            if (!isanimDead)
            {
                anim.SetTrigger("Death");
                isanimDead = true;
            }
            
        }


    }


    private void FixedUpdate()
    {
        //DashAnim();
    }
    
    void DashAnim()
    {
        if (!rolling)
        {
            if (ctrlReplacement && canRoll)
            {
                rolling = true; //initiate dash
                rollCurrent = startRollTimer;
            //    Debug.Log("Increasing");
                StartCoroutine(CanRoll());
            }
        }
        else
        {
         //   Debug.Log("Decreasing");
            rollCurrent -= Time.deltaTime;
            if (rollCurrent <= 0)
            {
                rolling = false;
            }
        }
        ctrlReplacement = false;
    }
    IEnumerator CanRoll()
    {
        canRoll = false;
        yield return new WaitForSeconds(animRollCooldown);
        canRoll = true;
    }
}
