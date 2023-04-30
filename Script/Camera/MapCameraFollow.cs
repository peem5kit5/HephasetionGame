using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraFollow : MonoBehaviour
{
    public Transform playerTarget;



    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(playerTarget.position.x, playerTarget.position.y, playerTarget.position.z);
        transform.position = targetPosition;
    }
}
