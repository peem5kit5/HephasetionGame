using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightSpecial : MonoBehaviour
{
    public GameObject aleartImage;
    public float rotationSpeed;
    public float distance;
    public LayerMask layer;
    public Transform enemy;
    public GameObject enem;

    void Start()
    {
        enemy = transform.parent;
        //Transform parentTransform = transform.parent;
        // enemBehaviour = parentTransform.gameObject.GetComponent<Enemy>();
    }
    void Update()
    {
        layer = ~(1 << LayerMask.NameToLayer("Theirself"));
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, layer);
        if (hitInfo.collider != null)
        {


            if (hitInfo.collider.CompareTag("Player"))
            {
                // enemBehaviour.transform.parent.GetComponent<Enemy>();
                enemy.GetComponent<SpecialUnits>().anim.SetTrigger("Alert");
                this.enabled = false;
                aleartImage.SetActive(true);
            }
            if (enemy.GetComponent<SpecialUnits>().isSaw == true)
            {
                this.enabled = false;
                aleartImage.SetActive(true);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);

        }
        if (transform.eulerAngles.z > 90f && transform.eulerAngles.z < 270f)
        {
            enem.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            enem.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
