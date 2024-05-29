using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float cooldown;
    public float maxCooldown;
    public int maxEnemiesCount;
    public int currentEnemiesCount;
    public BoxCollider2D SpawnBox;
    public bool canSpawn;
    public bool Reduce;
    public List<GameObject> gameObjects = new List<GameObject>();
    public List<Enemy> enem = new List<Enemy>();
    public List<GameObject> objectToCheck = new List<GameObject>();
    bool allEnemiesSaw;
    bool sense;
    private void Start()
    {
        cooldown = 0;
        Reduce = false;
    }
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
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            //cooldown = maxCooldown;
            canSpawn = true;
        }
        //foreach(GameObject obj in gameObjects)
       // {
       //     if(obj.GetComponent<Enemy>() != null)
       //     {
       //         if (!obj.GetComponent<Enemy>().isSaw && !obj.GetComponent<Enemy>().Sus && Reduce && cooldown <= 0)
       //         {
      //              objectToCheck.Add(obj.gameObject);
      //              allEnemiesSaw = true;
      //          }
      //     }
      //  }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canSpawn | collision.CompareTag("Invisible") && canSpawn)
        {
            Bounds colliderBounds = SpawnBox.bounds;
            Vector3 colliderCenter = colliderBounds.center;
                Reduce = false;
                while (currentEnemiesCount < maxEnemiesCount)
                {
                float randomX = Random.Range(colliderCenter.x - colliderBounds.extents.x, colliderCenter.x + colliderBounds.extents.x);
                float randomY = Random.Range(colliderCenter.y - colliderBounds.extents.y, colliderCenter.y + colliderBounds.extents.y);
              
                Vector2 randomSpawn = new Vector2(randomX, randomY);
                int enemiesNumber = Random.Range(0, enemies.Length);
                       GameObject enemObject = Instantiate(enemies[enemiesNumber], randomSpawn, Quaternion.identity);
                       if(enemObject.GetComponent<SpecialUnits>() != null)
                   {
                      enemObject.GetComponent<SpecialUnits>().GetBoxToWander(SpawnBox);

                     }
                   else if (enemObject.GetComponent<Enemy>() != null)
                     {

                       enemObject.GetComponent<Enemy>().GetBoxToWander(SpawnBox);
                     GetEnemy(enemObject.GetComponent<Enemy>());
                  }
               
                currentEnemiesCount++;
                    
                }
            cooldown = maxCooldown;
                canSpawn = false;
          
           
                 


        }
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") | collision.CompareTag("Invisible"))
        {
            Reduce = true;
        }
    }
    void GetEnemy(Enemy enemy)
    {
        gameObjects.Add(enemy.gameObject);
        enem.Add(enemy);

    }
    
   
}
