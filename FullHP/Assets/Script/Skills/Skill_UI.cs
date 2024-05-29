using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_UI : MonoBehaviour
{
    public PlayerSkills.SkillType skill;
    public KeyCode skillKeyCode;
    public string Key;
    public GameObject EQButton;
    public GameObject DeQButton;
    public Button[] otherEQList;
    public Button[] otherDEQList;
    private void Start()
    {
    }
    public void EquipButtonClick()
    {
        HandleEquip();
    }
    public void DequipButtonClick()
    {
       
            EQButton.SetActive(true);
            GameManager.Instance.RemoveKeyCodeAssignment(skill);
            foreach (Button button in otherEQList)
            {
                button.interactable = true;
              
            }
            DeQButton.SetActive(false);
        
        
    }
    private void HandleEquip()
    {
           
            GameManager.Instance.AssignKeyCode(skillKeyCode, skill);
            DeQButton.SetActive(true);
            foreach(Button button in otherEQList)
            {
                button.interactable = false;
            
            }
            EQButton.SetActive(false);
        
       

    }
   

}
