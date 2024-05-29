using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAnim : MonoBehaviour
{
    public Animator anim;
    public Vector3 lastPosition;
    public bool isMoving;
    AIPath enemy;
    void Start()
    {
        anim = GetComponent<Animator>();
        lastPosition = transform.position;
        enemy = transform.parent.gameObject.GetComponent<AIPath>();
    }

    void Update()
    {
        
            if (enemy.velocity.magnitude > 0.01f)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
       
       
        
    }
    
}
