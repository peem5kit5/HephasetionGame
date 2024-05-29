using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathEffectInteract : MonoBehaviour
{
    public static DeathEffectInteract Instance { get; private set; }
    public Animator anim;
    public GameObject effect;
    CameraShake camShake;
    public bool Slowed = false;
    // Start is called before the first frame update
    void Start()
    {
        camShake = FindObjectOfType<CameraShake>();
        Instance = this;
      //  anim = GetComponent<Animator>();
    //    effect = GetComponent<GameObject>();
        
    }
    public void StunnedEffect(float stun)
    {
        StartCoroutine(Stunned(stun));
        
    }
    public void KilledEffect()
    {
        StartCoroutine(FlashDeathEffect());
        SoundManager.Instance.EnemyDeadSound();
    }
    IEnumerator Stunned(float beingStunned)
    {
        StartCoroutine(camShake.ShakePistol(0.05f, 0.02f));
        effect.SetActive(true);
        anim.SetBool("Bang",true);
        yield return new WaitForSeconds(beingStunned);
        effect.SetActive(false);
    }
    IEnumerator FlashDeathEffect()
    {
            StartCoroutine(camShake.ShakePistol(0.2f, 0.07f));
        effect.SetActive(true);
        Slowed = true;
             Time.timeScale = 0.3f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Debug.Log(Time.timeScale + "time");
            anim.SetTrigger("Flash");
            yield return new WaitForSeconds(0.2f);
              Slowed = false;
        effect.SetActive(false);
        Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Debug.Log(Time.timeScale + "time");

    }
}
