using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHP : MonoBehaviour
{
    public bool isLifeSteal = false;
    public float maxHP;
    public float currentHP;
    public GameObject WeaponHolder;
    private Rigidbody2D rb;
    private Player player;
    LevelSystem level;
    public int XP;
    public Color originalColor;
    private Color origin;
    AIPath path;
    bool healed;
    bool addexp;
    void Start()
    {
        path = GetComponent<AIPath>();
        origin = GetComponentInChildren<SpriteRenderer>().material.color;
        originalColor = origin;
        level = FindObjectOfType<LevelSystem>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }
    public void TakeDamage(float force,float damage, Vector2 bulletPos)
    {
        currentHP -= damage;
        StartCoroutine(HitFlash());
        KnockBack(force, bulletPos);
        if (currentHP <= 0)
        {
            Die();

        }
    }
    void KnockBack(float force,Vector2 pos)
    {
        Vector2 knockbackDir = (new Vector2(transform.position.x, transform.position.y) - pos).normalized;
        rb.AddForce(knockbackDir * force, ForceMode2D.Impulse);
    }
    public IEnumerator HitFlash()
    {
        if(path != null)
        {
            path.canMove = false;
        }
      
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        if (path != null)
        {
            path.canMove = true;
        }
    }
    public void Die()
    {
        if (player.vengeance && !healed)
        {
            PlayerHP hp = FindObjectOfType<PlayerHP>();
            hp.Heal(5);
            healed = true;
        }
        this.enabled = false;
        if(WeaponHolder != null)
        {
            WeaponHolder.SetActive(false);
        }
        if (!addexp)
        {
            addexp = true;
            level.AddExp(XP);
        }
        
        rb.velocity = Vector3.zero;
        
     //   Destroy(gameObject, 0.01f);
    }
}
