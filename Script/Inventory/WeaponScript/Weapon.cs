using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject thisGameObject;
    public List<GameObject> otherGameObject;
    public AnimP animGun;
    public Animator anim;

    private void Start()
    {


        anim = GetComponent<Animator>();
        animGun = GetComponent<AnimP>();
    }
    
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < otherGameObject.Count; i++)
        if (thisGameObject.activeInHierarchy)
        {
            foreach (GameObject gameObject in otherGameObject)
            {
                gameObject.SetActive(false);
            }
        }
        else if (otherGameObject[i].activeInHierarchy)
        {
                foreach(GameObject gameObject in otherGameObject)
                {
                    otherGameObject[i].SetActive(true);
                }
        }
        
    }
   

}
