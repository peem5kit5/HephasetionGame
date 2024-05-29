using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleMove;

    [Range(0, 10)]
    [SerializeField] int AfterVelo;

    [Range(0, 1f)]
    [SerializeField] float dustFormationPeroid;

    [SerializeField] Rigidbody2D playerRB;

    float counter;

    private void Start()
    {
        ParticleMove = GetComponent<ParticleSystem>();
        playerRB = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if(Mathf.Abs(playerRB.velocity.magnitude) > AfterVelo)
        {
            if(counter > dustFormationPeroid)
            {
                ParticleMove.Play();
                counter = 0;
            }
        }
    }
}
