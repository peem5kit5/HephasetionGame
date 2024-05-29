using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar_Charge : StateMachineBehaviour
{
    Transform Boar;
    bool chargeDamage;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boar = animator.transform.parent;
        Boar.GetComponent<EnemyAnimal>().path.canMove = false;
        chargeDamage = false;
        //Boar.GetComponent<EnemyAnimal>().Charging();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
            Player player = Boar.GetComponent<EnemyAnimal>().player;
            Vector2 direction = (player.transform.position - animator.transform.position).normalized;
            Boar.GetComponent<Rigidbody2D>().velocity = direction * 7;
            Collider2D[] collider = Physics2D.OverlapCircleAll(animator.transform.position, 3f);
            foreach (Collider2D col in collider)
            {
                if (col != null)
                {
                    if (!chargeDamage)
                    {

                        if (col.CompareTag("Player"))
                        {
                            PlayerHP playerhp = col.GetComponent<PlayerHP>();
                            if (playerhp != null)
                            {
                                playerhp.TakeDamage(20);
                                chargeDamage = true;
                            }
                        }
                    }
                }
            }
        
     
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Boar.GetComponent<EnemyAnimal>().path.canMove = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
 //  override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //  {
        // Implement code that sets up animation IK (inverse kinematics)
       
  //  }
}
