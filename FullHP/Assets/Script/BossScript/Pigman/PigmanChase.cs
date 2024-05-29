using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigmanChase : StateMachineBehaviour
{
    Transform Pigman;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Pigman = animator.transform.parent;
        Pigman.GetComponent<Boss_Pigman>().ChaseLogic();
        //  Pigman = 

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Pigman.GetComponent<Boss_Pigman>().path.destination = Pigman.GetComponent<Boss_Pigman>().player.transform.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Pigman.GetComponent<Boss_Pigman>().StopChase();
    }
}
