using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    Player player;
    public float duration;
   // public GameObject warpEffect;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Destroy(gameObject, duration);
        
    }
    private void OnDestroy()
    {
     //   Instantiate(warpEffect, this.gameObject.transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
          //  Vector2 thisdirection = new Vector2(transform.position.x, transform.position.y);
            player.SlingShot(gameObject.transform, duration);
           // Instantiate(warpEffect, player.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
          //  Vector2 thisdirection = new Vector2(transform.position.x, transform.position.y);
            player.SlingShot(gameObject.transform, duration);
         //   Instantiate(warpEffect, player.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
