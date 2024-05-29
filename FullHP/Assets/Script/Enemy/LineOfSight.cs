using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public GameObject aleartImage;
    public float rotationSpeed;
    public float distance;
    public LayerMask layer;
    public EnemyAnim enemy;
    public GameObject enem;
    public Enemy enemBehaviour;


    void Start()
    {
        float RandomSpeed = Random.Range(0, 3);
        rotationSpeed = rotationSpeed + RandomSpeed;
        //Transform parentTransform = transform.parent;
        // enemBehaviour = parentTransform.gameObject.GetComponent<Enemy>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
       // sprite.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, layer);
                  if (hitInfo.collider != null && hitInfo.collider.CompareTag("Player") )
                   {
                // enemBehaviour.transform.parent.GetComponent<Enemy>();
                     enemy.anim.SetTrigger("Alert");

                     aleartImage.SetActive(true);
                    aleartImage.SetActive(true);
                   // aleartImage.GetComponent<SpriteRenderer>().color = Color.red;
                      this.enabled = false;
           
                    



                   }
                   else if (hitInfo.collider != null && hitInfo.collider.CompareTag("Wall"))
                   {
                        Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                        return;
                   }
                   else
                   {
       
                         aleartImage.SetActive(false);
                   }
        Debug.DrawLine(transform.position, hitInfo.point, Color.green);
                       
                   
          
         
            if (transform.eulerAngles.z > 90f && transform.eulerAngles.z < 270f)
            {
          
            enem.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
            enem.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (enemBehaviour.isSaw == true)
            {

                aleartImage.SetActive(true);
     
                 this.enabled = false;

            }
            if (enemBehaviour.currentDurationSus > 0)
            {

                aleartImage.SetActive(true);
              
                
            }
            else
           {
              aleartImage.SetActive(false);
            //  aleartImage.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        
    }
  
}
