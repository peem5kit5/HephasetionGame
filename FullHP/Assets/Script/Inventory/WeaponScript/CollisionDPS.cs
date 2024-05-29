using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollisionDPS : MonoBehaviour
{
    public float MaxInterval;
    public float currentInterval;
    public int damage;


    private void Update()
    {

        if (!DeathEffectInteract.Instance.Slowed)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

        }
        else
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

        }
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 3);

            foreach(Collider2D enem in collider)
            {
                if(enem != null)
                {
                    if (enem.CompareTag("Enemy"))
                    {
                        currentInterval += Time.deltaTime;
                        if (currentInterval >= MaxInterval)
                        {
                            enem.GetComponent<EnemyHP>().TakeDamage(0.5f, damage, this.transform.position);
                        currentInterval = 0;
                        }
                        
                    }
                    else if (enem.CompareTag("Player"))
                    {
                        currentInterval += Time.deltaTime;
                        if (currentInterval >= MaxInterval)
                        {
                            enem.GetComponent<PlayerHP>().TakeDamage(damage);
                            currentInterval = 0;
                        }
                       
                    }
                    
                }
            }
            
           
        
      
    }



}
   









