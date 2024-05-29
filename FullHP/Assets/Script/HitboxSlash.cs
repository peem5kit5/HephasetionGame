using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSlash : MonoBehaviour
{
    CameraShake shake;
    Player player;
    private void Awake()
    {
        shake = FindObjectOfType<CameraShake>();
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<EnemyHP>() != null)
            {
                if (collision.GetComponent<Enemy>() != null)
                {
                    if (collision.GetComponent<Enemy>().isSaw)
                    {
                        shake.ShakePistol(0.1f, 0.03f);
                        SoundManager.Instance.SwordHitting();
                        if (player.takedown)
                        {
                            collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 50, this.gameObject.transform.position);
                        }
                        else
                        {
                            collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 25, this.gameObject.transform.position);
                        }
                        
                    }
                    else
                    {
                        SoundManager.Instance.BackStabSound();
                        shake.ShakePistol(0.1f, 0.2f);
                        if (player.takedown)
                        {
                            collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 100, this.gameObject.transform.position);
                        }
                        else
                        {
                            collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 25, this.gameObject.transform.position);
                        }
                    }
                }
                else if (collision.GetComponent<Nemesis>() != null)
                {

                    shake.ShakePistol(0.1f, 0.03f);
                    SoundManager.Instance.SwordHitting();
                    if (player.takedown)
                    {
                        collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 50, this.gameObject.transform.position);
                    }
                    else
                    {
                        collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 25, this.gameObject.transform.position);
                    }

                }
                else if (collision.GetComponent<SpecialUnits>() != null)

                {
                    SoundManager.Instance.BackStabSound();
                    shake.ShakePistol(0.1f, 0.2f);
                    if (player.takedown)
                    {
                        collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 50, this.gameObject.transform.position);
                    }
                    else
                    {
                        collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 25, this.gameObject.transform.position);
                    }
                }
                else

                {
                    SoundManager.Instance.BackStabSound();
                    shake.ShakePistol(0.1f, 0.2f);
                    if (player.takedown)
                    {
                        collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 100, this.gameObject.transform.position);
                    }
                    else
                    {
                        collision.GetComponent<EnemyHP>().TakeDamage(2.5f, 50, this.gameObject.transform.position);
                    }
                }


            }

        }
        else if (collision.CompareTag("Bullet"))
        {
            if(collision.GetComponent<EnemyBulletScript>() != null)
            {
                Destroy(collision.gameObject);
            }
          
        }

    }
}
