using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV_Player : MonoBehaviour
{
    public float coneAngle;
    public float coneRange;
    ToggleUI toggleUi;
    public LayerMask enemyLayer;
    void Start()
    {
        toggleUi = FindObjectOfType<ToggleUI>();
    }
    void Directing()
    {
        if (toggleUi.isLayout)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - transform.position;
            transform.up = direction.normalized;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        Directing();
        DetectEnemies();
    }
    void DetectEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, coneRange, enemyLayer);
       // List<Enemy> enemiesVisible = new List<Enemy>();
       // List<Collider2D> detectedEnemies = new List<Collider2D>();
        foreach(Collider2D collider in colliders)
        {

            Enemy enemy = collider.GetComponent<Enemy>();
            SpecialUnits specialEnemy = collider.GetComponent<SpecialUnits>();
            Nemesis nemesis = collider.GetComponent<Nemesis>();
            EnemyAnimal animal = collider.GetComponent<EnemyAnimal>();
            if(enemy != null)
            {
                Vector2 direction = collider.transform.position - transform.position;
                float angle = Vector2.Angle(transform.up, direction);
                if (angle <= coneAngle * 0.5f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, coneRange, enemyLayer);
                    if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        if (enemy != null)
                        {
                            enemy.SetVisibility(true);
                        }
                        
                        // enemiesVisible.Add(enemy);
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                    }
                    else
                    {
                        if (enemy != null)
                        {
                            enemy.SetVisibility(false);
                        }
                     

                    }

                }
                else
                {
                    if (enemy != null)
                    {
                        enemy.SetVisibility(false);
                    }
                  

                }
            }
            if(specialEnemy != null)
            {
                Vector2 direction = collider.transform.position - transform.position;
                float angle = Vector2.Angle(transform.up, direction);
                if (angle <= coneAngle * 0.5f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, coneRange, enemyLayer);
                    if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        if (specialEnemy != null)
                        {
                            specialEnemy.SetVisibility(true);
                        }

                        // enemiesVisible.Add(enemy);
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                    }
                    else
                    {
                        if (specialEnemy != null)
                        {
                            specialEnemy.SetVisibility(false);
                        }


                    }

                }
                else
                {
                    if (specialEnemy != null)
                    {
                        specialEnemy.SetVisibility(false);
                    }


                }
            }
            if (nemesis != null)
            {
                Vector2 direction = collider.transform.position - transform.position;
                float angle = Vector2.Angle(transform.up, direction);
                if (angle <= coneAngle * 0.5f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, coneRange, enemyLayer);
                    if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        if (nemesis != null)
                        {
                            nemesis.SetVisibility(true);
                        }

                        // enemiesVisible.Add(enemy);
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                    }
                    else
                    {
                        if (nemesis != null)
                        {
                            nemesis.SetVisibility(false);
                        }


                    }

                }
                else
                {
                    if (nemesis != null)
                    {
                        nemesis.SetVisibility(false);
                    }


                }
            }
            if (animal != null)
            {
                Vector2 direction = collider.transform.position - transform.position;
                float angle = Vector2.Angle(transform.up, direction);
                if (angle <= coneAngle * 0.5f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, coneRange, enemyLayer);
                    if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        if (animal != null)
                        {
                            animal.SetVisibility(true);
                        }

                        // enemiesVisible.Add(enemy);
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                    }
                    else
                    {
                        if (animal != null)
                        {
                            animal.SetVisibility(false);
                        }


                    }

                }
                else
                {
                    if (animal != null)
                    {
                        animal.SetVisibility(false);
                    }


                }
            }


        }
      
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, coneRange);

        Vector3 coneDirection = Quaternion.Euler(0, 0, -coneAngle * 0.5f) * transform.up;
        Gizmos.DrawLine(transform.position, transform.position + coneDirection * coneRange * 0.5f);
        coneDirection = Quaternion.Euler(0, 0, coneAngle * 0.5f) * transform.up;
        Gizmos.DrawLine(transform.position, transform.position + coneDirection * coneRange * 0.5f);
    }
}
