using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnArea;
    public int maxEnemies;
    public float spawnCooldown;

    private List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        InvokeRepeating("SpawnEnemies", 0, 10f);
    }
    void SpawnEnemies()
    {
        if(enemies.Count >= maxEnemies)
        {
            return;
        }
        Vector2 spawnPoint = Random.insideUnitCircle * spawnArea;
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject newEnemy = Instantiate(enemyPrefabs[randomIndex], spawnPoint, Quaternion.identity);
        enemies.Add(newEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count == 0)
        {
            StartCoroutine(StartSpawnCoolDown());
        }
    }
    IEnumerator StartSpawnCoolDown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        SpawnEnemies();
    }
}
