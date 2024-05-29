using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShake : MonoBehaviour
{
    public CameraShake camShake;
    public float shakeDuration;
    public float magnitude;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(camShake.ShakePistol(shakeDuration, magnitude));
        }
    }
}
