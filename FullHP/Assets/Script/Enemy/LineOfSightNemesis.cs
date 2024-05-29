using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightNemesis : MonoBehaviour
{
    public GameObject aleartImage;
    public float rotationSpeed;
    public float distance;
    public LayerMask layer;
    //public EnemyAnim enemy;
    private Nemesis nemesisBehaviour;
    bool sense;

    void Start()
    {
        nemesisBehaviour = transform.parent.GetComponent<Nemesis>();
        //Transform parentTransform = transform.parent;
        // enemBehaviour = parentTransform.gameObject.GetComponent<Enemy>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        // sprite.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, layer);

        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Player"))
        {
            // enemBehaviour.transform.parent.GetComponent<Enemy>();
            nemesisBehaviour.isSaw = true;
            nemesisBehaviour.sus = false;
            nemesisBehaviour.patrol = false;
            aleartImage.SetActive(true);
            aleartImage.SetActive(true);
            // aleartImage.GetComponent<SpriteRenderer>().color = Color.red;
            this.enabled = false;

            Debug.Log("Player Detect");



        }
        else if (hitInfo.collider != null && hitInfo.collider.CompareTag("Wall"))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            return;
        }
        else
        {

            aleartImage.SetActive(false);
        }
        Debug.DrawLine(transform.position, hitInfo.point, Color.green);


        if (transform.eulerAngles.z > 90f && transform.eulerAngles.z < 270f)
        {

            nemesisBehaviour.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            nemesisBehaviour.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (nemesisBehaviour.isSaw == true)
        {

            aleartImage.SetActive(true);

            this.enabled = false;

        }
        else
        {
            aleartImage.SetActive(false);
        }

    }

}
