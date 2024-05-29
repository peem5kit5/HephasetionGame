using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UI_Skilltree uiSkilltree;
    void Start()
    {
        uiSkilltree.SetPlayerSkills(player.GetPlayerSkills());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
