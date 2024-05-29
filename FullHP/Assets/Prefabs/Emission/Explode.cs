using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public ParticleSystem effect;
  //  public Animator anim;
    public AudioSource audioManage;
    public AudioClip Boom;
    void Start()
    {
        audioManage = GetComponent<AudioSource>();
        audioManage.clip = Boom;
        audioManage.Play();
        effect.Play();
       // anim.Play("Exploding");
        Destroy(gameObject,1f);
    }

    
}
