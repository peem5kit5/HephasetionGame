using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NathanMimic : MonoBehaviour
{
    Boss_Nathan bossNathan;
    EnemyHP hp;
    public GameObject SmokeNathan;
    public float radius = 2f;
    public float speed = 1.5f;
    float angle;
    Transform circleCenter;

    void Start()
    {
        bossNathan = FindObjectOfType<Boss_Nathan>();
        hp = GetComponent<EnemyHP>();
        circleCenter = bossNathan.Center;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossNathan.isAsking)
        {
            this.gameObject.tag = "Wall";
        }
        else
        {
            this.gameObject.tag = "Enemy";
        }
        if(hp.currentHP <= 0)
        {
            bossNathan.MinusCount();
            Instantiate(SmokeNathan, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        MoveAround();
    }
    void MoveAround()
    {
        float x = circleCenter.position.x + radius * Mathf.Cos(angle);
        float y = circleCenter.position.y + radius + Mathf.Sin(angle) + 1 * Mathf.Sin(angle * 2); ;
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
        Vector2 newPos = new Vector2(x, y);
        transform.position = newPos;

        angle += speed * Time.deltaTime;
        if(angle >= 2 * Mathf.PI)
        {
            angle -= 2 * Mathf.PI;
        }
    }
}
