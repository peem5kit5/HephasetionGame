using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Image Q;
    public Image E;
    public Image R;
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI warningSkill;
    private void Awake()
    {
        Q.enabled = false;
        E.enabled = false;
        R.enabled = false;
            Instance = this;
            
    }
  

    private Dictionary<KeyCode, PlayerSkills.SkillType> skillKeyMappings = new Dictionary<KeyCode, PlayerSkills.SkillType>();
    IEnumerator ClearWarn()
    {
        
        yield return new WaitForSeconds(2);
        warningSkill.text = "";
    }
    // Method to assign a key code to a skill
    public void AssignKeyCode(KeyCode keyCode, PlayerSkills.SkillType skill)
    {

        if (skillKeyMappings.ContainsValue(skill))
        {
           
            KeyCode existingKeyCode = skillKeyMappings.FirstOrDefault(x => x.Value == skill).Key;
            skillKeyMappings.Remove(existingKeyCode);
        }
        
        if (skillKeyMappings.ContainsKey(keyCode) && skillKeyMappings[keyCode] != skill)
        {
            warningSkill.text = "Replaced Skill Slot!";
            skillKeyMappings.Remove(keyCode);
            StartCoroutine(ClearWarn());
        }
        if(keyCode == KeyCode.Q)
        {
            Q.enabled = true;
            Q.sprite = GetSkillIcon(skill);
        }
        if(keyCode == KeyCode.E)
        {
            E.enabled = true;
            E.sprite = GetSkillIcon(skill);

        }
        if (keyCode == KeyCode.R)
        {
            R.enabled = true;
            R.sprite = GetSkillIcon(skill);

        }
        skillKeyMappings[keyCode] = skill;
        


      
        
    }
    public void RemoveKeyCodeAssignment(PlayerSkills.SkillType skill)
    {
        KeyCode keyCode = skillKeyMappings.FirstOrDefault(x => x.Value == skill).Key;

  
        if (keyCode != KeyCode.None)
        {
            skillKeyMappings.Remove(keyCode);
        }
        if (keyCode == KeyCode.Q)
        {
            Q.enabled = false;
        }
        if (keyCode == KeyCode.E)
        {
            E.enabled = false;

        }
        if (keyCode == KeyCode.R)
        {
            R.enabled = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
      
     
        foreach (KeyCode keyCode in skillKeyMappings.Keys)
        {
            if (Input.GetKeyDown(keyCode))
            {
            
                PlayerSkills.SkillType skill = skillKeyMappings[keyCode];

                // Call the UseSkill method with the associated skill
                UseSkill(skill);
            }
        }
    }

    Sprite GetSkillIcon(PlayerSkills.SkillType skill)
    {
        switch (skill)
        {
            default:
            case PlayerSkills.SkillType.Ember: return SkillIcon.Instance.Ember;
            case PlayerSkills.SkillType.TrapMine: return SkillIcon.Instance.TrapMine;
            case PlayerSkills.SkillType.GodLight: return SkillIcon.Instance.GodLight;
            case PlayerSkills.SkillType.TakeAim: return SkillIcon.Instance.TakeAim;
            case PlayerSkills.SkillType.Camoflage: return SkillIcon.Instance.Camoflage;
            case PlayerSkills.SkillType.SlingShotArrow: return SkillIcon.Instance.SlingShot;

        }
            
    }

    void UseSkill( PlayerSkills.SkillType skill)
    {
        switch (skill)
        {
            case PlayerSkills.SkillType.Ember:
                Debug.Log("Fire");
                break;
            case PlayerSkills.SkillType.TrapMine:
                Debug.Log("Mine Set!");
                break;
            case PlayerSkills.SkillType.GodLight:
                Debug.Log("Flashbang!");
                break;
            case PlayerSkills.SkillType.TakeAim:
                Debug.Log("Aiming!");
                break;
            case PlayerSkills.SkillType.Camoflage:
                Debug.Log("Silently!");
                break;
            case PlayerSkills.SkillType.SlingShotArrow:
                Debug.Log("Whoop!");
                break;

        }
    }
  

  
}
