using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookieCar : MonoBehaviour
{
    public Player move;
    public Transform player;
    public Transform vehicleSeat;
    public float vehicleSpeed = 5f;

    private bool isPlayerInside = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isPlayerInside)
            {
                ExitVehicle();
            }
            else
            {
                EnterVehicle();
            }
        }
    }

    void EnterVehicle()
    {
        move.isDriving = true;
        player.transform.position = vehicleSeat.transform.position;
        player.position = vehicleSeat.position;
        player.parent = vehicleSeat;
        isPlayerInside = true;
    }

    void ExitVehicle()
    {
        move.isDriving = false;
        player.transform.position = player.localPosition;
        player.parent = null;
        isPlayerInside = false;
    }

    void FixedUpdate()
    {
        if (isPlayerInside)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(horizontal, vertical);
            direction.Normalize();

            Vector2 force = direction * vehicleSpeed;
            GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
}
