using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSoldier_Rushing : StateMachineBehaviour
{
    Transform SuperSolider;
    bool damaged;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SuperSolider = animator.transform.parent;
        SuperSolider.GetComponent<SpecialUnits>().path.canMove = false;
        damaged = false;
        SuperSolider.GetComponent<SpecialUnits>().PlaySoldier_ChargeSound();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = SuperSolider.GetComponent<SpecialUnits>().player.GetComponent<Player>();
        Vector2 direction = (player.transform.position - animator.transform.position).normalized;
        SuperSolider.GetComponent<Rigidbody2D>().velocity = direction * 6;
        Collider2D[] collider = Physics2D.OverlapCircleAll(animator.transform.position, 1.3f);
        foreach(Collider2D col in collider)
        {
            if (col != null)
            {
                if (col.CompareTag("Player"))
                {
                    if (!damaged)
                    {
                        col.GetComponent<PlayerHP>().TakeDamage(20);
                        damaged = true;
                    }
                }
            }
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SuperSolider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SuperSolider.GetComponent<SpecialUnits>().path.canMove = true;
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
