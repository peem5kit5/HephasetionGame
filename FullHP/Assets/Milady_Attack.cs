using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milady_Attack : StateMachineBehaviour
{
    Transform Milady;
    bool damaged;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Milady = animator.transform.parent;
        Milady.GetComponent<Boss_Milady>();
        Milady.GetComponent<Boss_Milady>().Attacked = false;
        Milady.GetComponent<BoxCollider2D>().enabled = true;
        Milady.GetComponent<Boss_Milady>().attackSound();
        damaged = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Milady.GetComponent<Boss_Milady>().TargetingAttack();
        Collider2D[] collider = Physics2D.OverlapCircleAll(animator.transform.position, 1.2f);
        foreach(Collider2D col in collider)
        {
            if (!damaged)
            {
                if (col != null)
                {
                    if (col.CompareTag("Player"))
                    {
                        PlayerHP hp = col.GetComponent<PlayerHP>();
                        if (hp != null)
                        {
                            hp.TakeDamage(5);
                            
                        }
                        damaged = true;
                    }
                }
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Milady.GetComponent<Boss_Milady>().Attacked = false;
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
