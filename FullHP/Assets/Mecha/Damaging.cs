using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    public float damage;
    bool isDamaging;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDamaging = true;
            StartCoroutine(DealDamage(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDamaging = false;
        }
    }

    IEnumerator DealDamage(Collider2D coll)
    {
        while (isDamaging)
        {
            coll.GetComponent<PlayerHP>().TakeDamage(damage * Time.deltaTime);
            yield return null;
        }
    }

}
