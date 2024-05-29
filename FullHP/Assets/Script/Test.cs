using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Attack : MonoBehaviour
{
	public List<GameObject> arrows = new List<GameObject>();
	public GameObject currentArrow;

	public GameObject redArrow_Prefab;
	public GameObject blueArrow_Prefab;
	public GameObject yellowArrow_Prefab;
	private int index;

	public Transform shootingPoint;
	public enum ArrowType
    {
		RedArrow,
		BlueArrow,
		YellowArrow
    }
	public ArrowType type;

    private void Start()
    {
		currentArrow = redArrow_Prefab;
    }
    void BUYArrow(GameObject gameObject)
    {
		arrows.Add(gameObject);
    }
	void AnimatorCon(string anim)
    {
		
    }
    void ChangeArrow()
    {
		
			if(index < arrows.Count)
            {
				currentArrow = arrows[index];
				AnimatorCon(arrows[index].name);
				index++;
            }
            else
            {
				currentArrow = arrows[0];
				AnimatorCon(arrows[0].name);
				index = 0;
            }
        
    }

	void Shoot()
    {
		GameObject shootOBJ = Instantiate(currentArrow, shootingPoint.position, Quaternion.identity);
		Rigidbody2D rb = shootOBJ.GetComponent<Rigidbody2D>();
		Vector3 dir = transform.position;
		rb.velocity =dir * 5;
    }
}
