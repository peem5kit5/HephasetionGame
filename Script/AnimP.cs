using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimP : MonoBehaviour
{
    private Animator anim;
    bool rolling = false;
    bool canRoll = true;
    bool shiftReplacement;
    public string[] staticDirections = { "IdleN",  "IdleW", "IdleS","IdleE", }; //fix to be my own animation's name
    public string[] runDirections = { "RunN",  "RunW", "RunS",  "RunE" };
    //public string[] rollDirections = { "RollUp", "RollLeft", "RollDown", "RollRight" };
    public int lastDirection;
    public float rollCurrent;
    public float startRollTimer;
    public Player player;
    public float moveH, moveV;
    public float animRollCooldown;
    ToggleUI ui;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        Debug.Log("R1 " + result1);
        float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left);
        Debug.Log("R2 " + result2);
        float result3 = Vector2.SignedAngle(Vector2.up, Vector2.down);
        Debug.Log("R3 " + result3);
    }
    private void Start()
    {
        ui = FindObjectOfType<ToggleUI>();
        player = GetComponent<Player>();
    }
    public void Update()
    {
        if (ui.isLayout)
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
                    if (movement != Vector2.zero)
                    {
                        anim.Play("RunE");
                    }
                    else
                    {
                        anim.Play("IdleE");
                    }

                }
                else if (rolling)
                {
                    anim.Play("RollRight");
                }
            }
            else if (angle > 45 && angle <= 135)
            {
                if (!rolling)
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
                else if (rolling)
                {
                    anim.Play("RollLeft");
                }
            }
            else if (angle > 135 || angle <= -135)
            {
                if (!rolling)
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
                else if (rolling)
                {
                    anim.Play("RollLeft");
                }
            }
            else if (angle > -135 && angle <= -45)
            {
                if (!rolling)
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
                else if (rolling)
                {
                    anim.Play("RollRight");
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                shiftReplacement = true;
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
            if (shiftReplacement && canRoll)
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
        shiftReplacement = false;
    }
    IEnumerator CanRoll()
    {
        canRoll = false;
        yield return new WaitForSeconds(animRollCooldown);
        canRoll = true;
    }
}
