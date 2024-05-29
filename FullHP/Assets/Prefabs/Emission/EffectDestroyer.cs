using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    public float Time;
    void Start()
    {
        Destroy(gameObject, Time);
    }

}
