using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoverPos : MonoBehaviour
{
    public bool takeCover = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            takeCover = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            takeCover = false;
        }
    }
}
