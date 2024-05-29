using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload_UI : MonoBehaviour
{
   
    public Transform playerTransform;
    public float yOffset = 1f;

    // Update is called once per frame
    void Update()
    {
       
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z);
    }

    
}
