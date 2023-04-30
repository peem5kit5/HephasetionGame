using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertArea : MonoBehaviour
{
    private Transform enemy;
    public List<Enemy> enemies;
    public List<SpecialUnits> special;
    void Start()
    {
        enemy = this.transform.parent;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                enemies.Add(collision.GetComponent<Enemy>());
            }

            if (collision.GetComponent<SpecialUnits>() != null)
            {
                special.Add(collision.GetComponent<SpecialUnits>());
            }


        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (enemy.GetComponent<Enemy>().isSaw)
        {


            foreach (Enemy enem in enemies)
            {

                enem.isSaw = true;
                //Debug.Log("Go get Him Bois!");
            }




            foreach (SpecialUnits enem in special)
            {

                enem.isSaw = true;
                //Debug.Log("Go get Him Bois!");
            }




        }

    }
}
