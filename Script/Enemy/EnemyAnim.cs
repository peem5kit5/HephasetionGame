using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public Animator anim;
    private Vector3 lastPosition;

    void Start()
    {
        anim = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != lastPosition)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        lastPosition = transform.position;
    }
}
