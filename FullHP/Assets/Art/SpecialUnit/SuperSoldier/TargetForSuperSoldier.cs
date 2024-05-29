using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForSuperSoldier : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!DeathEffectInteract.Instance.Slowed)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

        }
        Vector3 newPosition = Vector3.Lerp(this.transform.position, player.transform.position, Time.deltaTime * 1.5f);
        this.transform.position = newPosition;
    }
}
