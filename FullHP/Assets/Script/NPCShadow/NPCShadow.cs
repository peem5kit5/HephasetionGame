using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShadow : MonoBehaviour
{
    public bool isRunningAway;
    public float runDuration;
    public float runTimer;
    Vector2 targetPos;

    public float wanderingSpeed;
    public float runSpeed;
    Rigidbody2D rb;
    Player player;

    public float wanderTimer;
    public float maxWanderTimer;
    Animator anim;

    SpriteRenderer sprite;
    private void Start()
    {
        SetRandomTarget();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    // ... Rest of the script ...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && !isRunningAway && wanderTimer < maxWanderTimer)
        {

           SetRandomTarget();
        }
        else if(collision.CompareTag("NPCShadow") && !isRunningAway && wanderTimer < maxWanderTimer)
        {
            SetRandomTarget();
        }
        else if (collision.CompareTag("Player") && !isRunningAway && wanderTimer < maxWanderTimer)
        {
            targetPos = this.transform.position;
        }
    }
    void SetRandomRunTarget(Vector2 outofthis)
    {
        Vector2 currentPosition = transform.position;
        Vector2 wallNormal = (Vector2)transform.position - targetPos;

        // Calculate a new target position away from the wall, but ensure it's not within the collision range
        float safeDistance = 1f; // Adjust this value based on your NPC's size and desired behavior
        Vector2 newTargetPos = currentPosition + wallNormal.normalized * (safeDistance + 1.0f);

        // Ensure the new target position is within a certain range
        float maxDistanceFromCurrent = 10f; // Adjust this value as needed
        targetPos = Vector2.ClampMagnitude(newTargetPos - currentPosition, maxDistanceFromCurrent) + currentPosition;
    }
    private void Update()
    {
        if (isRunningAway)
        {
            RunAway();
        }
        else
        {
            MoveToWaypoint();
        }
        AnimLogic();
    }
    void AnimLogic()
    {
        if (isRunningAway)
        {
            if(rb.velocity.magnitude >= 0.1f)
            {
                anim.SetBool("isRunning", true);
             
            }
            else
            {
                anim.SetBool("isRunning", false);
                
            }
            anim.SetBool("isMoving", false);
        }
        else
        {
            if (Vector2.Distance(transform.position, targetPos) >= 0.1f)
            {
                anim.SetBool("isMoving", true);
                
            }
            else
            {
                anim.SetBool("isMoving", false);
                
            }
            anim.SetBool("isRunning", false);
        }
        if (rb.velocity.x >= 0.1f)
        {
            sprite.flipX = true;

        }
        else
        {
            sprite.flipX = false;

        }
    }
   
    private void RunAway()
    {
        // Calculate the direction away from the player
        Vector2 runDirection = rb.position - (Vector2)player.transform.position;
        rb.velocity = runDirection.normalized * runSpeed;

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
        // Update the run timer
        runTimer += Time.deltaTime;
        if (runTimer >= runDuration)
        {
            isRunningAway = false;
            rb.velocity = Vector2.zero;
            runTimer = 0f;
        }
    }
    private void MoveToWaypoint()
    {
        //Vector2 position = new Vector2(targetPos.x, targetPos.y);
        
       
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
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0)
        {
            
            SetRandomTarget();
            wanderTimer = maxWanderTimer;
           
        }
        if (Vector2.Distance(transform.position, targetPos) <= 0.1f)
        {

            //wanderTimer = maxWanderTimer;
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 moveDirection = (targetPos - (Vector2)transform.position).normalized;
            rb.velocity = moveDirection * wanderingSpeed;
        }

            
        
       
        


    }
    
    void SetRandomTarget()
    {
        Vector2 currentPosition = transform.position;
        targetPos = new Vector2(
            Random.Range(currentPosition.x - 3f, currentPosition.x + 3f),
            Random.Range(currentPosition.y - 3f, currentPosition.y + 3f)
        );
    }
    public void StartRunningAway()
    {
        isRunningAway = true;
    }
}
