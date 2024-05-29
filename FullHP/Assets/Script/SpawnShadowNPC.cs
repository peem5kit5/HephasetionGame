using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShadowNPC : MonoBehaviour
{
    public GameObject[] NPC;

    public int maxNPC_Count;
    public int currentNPC_Count;

    BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        while(currentNPC_Count < maxNPC_Count)
        {

            int random = Random.Range(0, NPC.Length);
            Vector2 spawnAreaSize = box.size;
            Vector2 spawnAreaCenter = (Vector2)transform.position + box.offset;
            Vector2 randomSpawn = new Vector2(Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.x + spawnAreaSize.x / 2f),
            Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2f, spawnAreaCenter.y + spawnAreaSize.y / 2f));
            GameObject npc = Instantiate(NPC[random], randomSpawn, Quaternion.identity);
            float randomingWanderTime = Random.Range(5, 10);
            float randomcurrentWander = Random.Range(0, randomingWanderTime);
            npc.GetComponent<NPCShadow>().maxWanderTimer = randomingWanderTime;
            npc.GetComponent<NPCShadow>().wanderTimer = randomcurrentWander;
            currentNPC_Count++;
        }
    }
}
