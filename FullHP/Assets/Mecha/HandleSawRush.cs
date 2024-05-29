using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSawRush : StateMachineBehaviour
{
    Transform enem;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enem = animator.transform.parent;
        enem.GetComponent<SpecialUnits>().chainSaw = true;
        enem.GetComponent<SpecialUnits>().canMove = false;
        enem.GetComponent<SpecialUnits>().PlayChainSawSound();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        enem.GetComponent<SpecialUnits>().canMove = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enem.GetComponent<SpecialUnits>().chainSaw = false;
        enem.GetComponent<SpecialUnits>().canMove = true;
        enem.GetComponent<SpecialUnits>().anim.SetBool("StartChainsaw", false);

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
