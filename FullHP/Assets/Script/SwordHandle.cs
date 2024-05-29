using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHandle : MonoBehaviour
{
    public Transform playerTransform;
    public Animator animSword;
    // Update is called once per frame
    private void Awake()
    {

    }
    void Update()
    {
      
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - (Vector2)playerTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }
   

    public void AnimatedSword()
    {
        SoundManager.Instance.SwipingSword();
        animSword.SetTrigger("Slash");
    }
    public void ComboSword()
    {
        StartCoroutine(Combo());
    }
    IEnumerator Combo()
    {
        SoundManager.Instance.SwipingSword();
        animSword.SetTrigger("Slash");
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.SwipingSword();
        animSword.SetTrigger("Slash");
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.SwipingSword();
        animSword.SetTrigger("Slash");
    }
}
