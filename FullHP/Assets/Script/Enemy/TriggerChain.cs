using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChain : MonoBehaviour
{
    Transform enemy;
    private void Start()
    {
        enemy = transform.parent;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.GetComponent<SpecialUnits>().special == false && enemy.GetComponent<SpecialUnits>().armed == false)
        {
            enemy.GetComponent<SpecialUnits>().anim.SetBool("StartChainsaw", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.GetComponent<SpecialUnits>().special == false && enemy.GetComponent<SpecialUnits>().armed == false)
        {
            enemy.GetComponent<SpecialUnits>().canMove = true;
        }
    }
}
