using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Wolf : StateMachineBehaviour
{
    Transform Wolf;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Wolf = animator.transform.parent;
        Wolf.GetComponent<EnemyAnimal>().path.canMove = false;
        Wolf.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(Wolf.position, Wolf.GetComponent<EnemyAnimal>().attackRange);
        foreach(Collider2D col in collider)
        {
            if (col != null)
            {
                if (col.CompareTag("Player"))
                {
                    col.GetComponent<PlayerHP>().TakeDamage(15);
                }
            }
        }
        Wolf.GetComponent<EnemyAnimal>().path.canMove = true;
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
