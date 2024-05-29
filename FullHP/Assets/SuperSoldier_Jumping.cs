using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSoldier_Jumping : StateMachineBehaviour
{
    Transform SuperSoldier;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SuperSoldier = animator.transform.parent;
        SuperSoldier.GetComponent<SpecialUnits>().path.canMove = false;
        SuperSoldier.GetComponent<SpecialUnits>().spawnTarget();
        SuperSoldier.GetComponent<SpecialUnits>().FollowTarget();
        SuperSoldier.GetComponent<SpecialUnits>().PlaySoldier_JumpSound();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
        SuperSoldier.GetComponent<BoxCollider2D>().enabled = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SuperSoldier.GetComponent<SpecialUnits>().transform.position = animator.transform.position;
        SuperSoldier.GetComponent<SpecialUnits>().path.canMove = true;
        SuperSoldier.GetComponent<BoxCollider2D>().enabled = true;
        SuperSoldier.GetComponent<SpecialUnits>().PlaySoldier_DownSound();
        Collider2D[] collider = Physics2D.OverlapCircleAll(animator.transform.position, 1f);
        foreach (Collider2D col in collider)
        {
            if (col != null)
            {
                if (col.CompareTag("Player"))
                {

                    col.GetComponent<PlayerHP>().TakeDamage(20);


                }
            }

        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
   // override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //  {
      
        // Implement code that processes and affects root motion
   // }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
