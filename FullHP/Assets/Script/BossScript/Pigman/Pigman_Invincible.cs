using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigman_Invincible : StateMachineBehaviour
{
    Transform Pigman;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

      //  Pigman.GetComponent<Boss_Pigman>().StopChase();
        Pigman = animator.transform.parent;
        Pigman.GetComponent<Boss_Pigman>().WaitingForSound();

        //Pigman.GetComponent<Boss_Pigman>().StopChase();
        // Pigman.GetComponent<BoxCollider2D>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        Pigman.GetComponent<Boss_Pigman>().path.canMove = false;
        Pigman.GetComponent<Boss_Pigman>().rb.velocity = Vector3.zero;
        Pigman.GetComponent<Boss_Pigman>().gameObject.tag = "Wall";

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Pigman.GetComponent<Boss_Pigman>().CooldownInvincible();
        Pigman.GetComponent<Boss_Pigman>().gameObject.tag = "Enemy";
       // Pigman.GetComponent<BoxCollider2D>().enabled = true;
        Pigman.GetComponent<Boss_Pigman>().path.canMove = true;
        //  Pigman.GetComponent<Boss_Pigman>().ChaseLogic();
        // Pigman.GetComponent<Boss_Pigman>().CooldownInvincible();
        Pigman.GetComponent<Boss_Pigman>().Invincibled = false;
       // Pigman.GetComponent<Boss_Pigman>().ChaseLogic();
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
