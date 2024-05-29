using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    public Animator anim;
    public Transform gun;
    public Camera cam;
    public float fireend;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mousePos);
        Vector3 direction = (mouseWorldPos - gun.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle > -45 && angle <= 45)
        {

                
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("recoilRight");
            }
            else
            {
                anim.Play("right");
            }

        }
        else if (angle > 45 && angle <= 135)
        {

               
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("recoilUp");
            }
            else
            {
                anim.Play("up");
            }

        }
        else if (angle > 135 || angle <= -135)
        {
                
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("recoilLeft");
                Debug.Log("Recoil Deploy");
                
            }
            else
            {
                anim.Play("left");
            }
        }
        else if (angle > -135 && angle <= -45)
        {
               
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("recoilDown");
            }
            else
            {
                anim.Play("down");
            }

        }
        
    }
}
