using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHP : MonoBehaviour
{
    public bool isLifeSteal = false;
    public float maxHP = 100;
    public float currentHP;
    public GameObject WeaponHolder;
    private Rigidbody2D rb;
    private Player player;
    LevelSystem level;
    public int XP;
    private Color originalColor;
    private Color origin;
    AIPath path;
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
        path.canMove = false;
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        path.canMove = true;
    }
    public void Die()
    {
        if (isLifeSteal)
        {
            player.SetHealingMod(15);
        }
        this.enabled = false;
        if(WeaponHolder != null)
        {
            WeaponHolder.SetActive(false);
        }
        level.AddExp(XP);
        rb.velocity = Vector3.zero;
        Destroy(gameObject, 1);
    }
}
