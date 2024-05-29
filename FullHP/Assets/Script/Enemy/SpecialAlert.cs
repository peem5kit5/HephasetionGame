using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAlert : MonoBehaviour
{
    private Transform enemy;
    public List<Enemy> enemies;
    public List<SpecialUnits> special;
    public List<Nemesis> nemesis;
    void Start()
    {
        enemy = this.transform.parent;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<Enemy>() != null)
            {
                 enemies.Add(collision.GetComponent<Enemy>());
            }
            
            if(collision.GetComponent<SpecialUnits>() != null)
            {
                special.Add(collision.GetComponent<SpecialUnits>());
            }
           
            if(collision.GetComponent<Nemesis>() != null)
            {
                nemesis.Add(collision.GetComponent<Nemesis>());
            }

        }

        

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                enemies.Remove(collision.GetComponent<Enemy>());
            }

            if (collision.GetComponent<SpecialUnits>() != null)
            {
                special.Remove(collision.GetComponent<SpecialUnits>());
            }
            if (collision.GetComponent<Nemesis>() != null)
            {
                nemesis.Remove(collision.GetComponent<Nemesis>());
            }


        }



    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (enemy.GetComponent<SpecialUnits>().isSaw)
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

                    foreach(Nemesis nemesissm in nemesis)
            {
                nemesissm.isSaw = true;
            }
                
            
            
            
        }
        
    }
}
