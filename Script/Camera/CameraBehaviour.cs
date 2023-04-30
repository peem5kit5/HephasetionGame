using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    ToggleUI ui;

    private void Start()
    {
        ui = FindObjectOfType<ToggleUI>();
    }
    private void Update()
   {
        if(ui != null && ui.isLayout)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = (player.position + mousePos) / 2f;

            targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
            targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

            this.transform.position = targetPos;
        }
       

    }
    
}
