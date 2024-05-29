using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Skilltree : MonoBehaviour
{
    public PlayerSkills skills;

    Player player;
    public GameObject PathCQCPath;
    public GameObject PathTacticalPath;
    public GameObject DoorPath;
    public GameObject TakeDownPath;
    public GameObject TakeAimPath;
    public GameObject CamoPath;
    public GameObject VengenPath;
    public GameObject EmberPath;
    public GameObject MinePath;

    public GameObject GodLight;
    public GameObject TrapWires;
    public GameObject Camoflage;
    public GameObject Ember;
    public GameObject TakeAim;
    public GameObject SlingShot;

    public TextMeshProUGUI skillNameText;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
       
       
    }
    private void Start()
    {
        // player.Load();
        
    }
    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.skills = playerSkills;
    }
    private void Update()
    {
        
        
    }
    //Utilities Skills
    void DeactivatedAll()
    {
        GodLight.SetActive(false);
        TrapWires.SetActive(false);
        Camoflage.SetActive(false);
        Ember.SetActive(false);
        TakeAim.SetActive(false);
        SlingShot.SetActive(false);
    }
    public void UnlockCQC()
    {
        if(LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.CQC))
        {
               skills.TryUnlockSkill(PlayerSkills.SkillType.CQC);
            skillNameText.text = "CQC";
            DeactivatedAll();
            Debug.Log("CQC Init!");
            PathCQCPath.SetActive(true);
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.CQC))
        {
            DeactivatedAll();
            skillNameText.text = "CQC";
            return;
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
     
      
        
      //  skills.TryUnlockSkill(PlayerSkills.SkillType.CQC);
          
       
    }
    public void UnlockVengeance()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.Vengeance))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.Vengeance);
            skillNameText.text = "Vengeance";
            DeactivatedAll();
            Debug.Log("Vengence Init!");
            VengenPath.SetActive(true);
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.Vengeance))
        {
            DeactivatedAll();
            skillNameText.text = "Vengeance";
            return;
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockTakeDown()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TakeDown))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.TakeDown);
            skillNameText.text = "TakeDown";
            DeactivatedAll();
            Debug.Log("Takedown Init!");
            TakeDownPath.SetActive(true);
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TakeDown))
        {
            DeactivatedAll();
            skillNameText.text = "TakeDown";
            return;
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockDoorSweeper()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.DoorSweeper))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.DoorSweeper);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.DoorSweeper))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Door Sweeper";
                DeactivatedAll();
                DoorPath.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.DoorSweeper))
        {
            skillNameText.text = "Door Sweeper";
            return;
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockTacticalStance()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TacticalStance))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.TacticalStance);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.TacticalStance))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Tactical Stance";
                DeactivatedAll();
                PathTacticalPath.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TacticalStance))
        {
            DeactivatedAll();
            skillNameText.text = "Tactical Stance";
            return;
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockWindLanguage()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.WindLanguage))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.WindLanguage);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.WindLanguage))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Wind Language";
                DeactivatedAll();

            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.WindLanguage))
        {
            DeactivatedAll();
            skillNameText.text = "Wind Language";
            return;
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockCamoflage()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.Camoflage))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.Camoflage);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.Camoflage))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Camoflage";
                DeactivatedAll();
                Camoflage.SetActive(true);
                CamoPath.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.Camoflage))
        {
            DeactivatedAll();
            skillNameText.text = "Camoflage";
            Camoflage.SetActive(true);
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }

    //Sword Skills

   
    public void UnlockEmber()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.Ember))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.Ember);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.Ember))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Ember";
                DeactivatedAll();
                Ember.SetActive(true);
                EmberPath.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.Ember))
        {
            DeactivatedAll();
            skillNameText.text = "Ember";
            Ember.SetActive(true);
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockTrapMine()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TrapMine))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.TrapMine);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.TrapMine))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Trap Mine";
                DeactivatedAll();
                TrapWires.SetActive(true);
                MinePath.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TrapMine))
        {
            DeactivatedAll();
            skillNameText.text = "Trap Mine";
            TrapWires.SetActive(true);
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockGodLight()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.GodLight))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.GodLight);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.GodLight))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "God Light";
                DeactivatedAll();
                GodLight.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.GodLight))
        {
            DeactivatedAll();
            skillNameText.text = "God Light";
            GodLight.SetActive(true);
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    public void UnlockTakeAim()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TakeAim)) 
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.TakeAim);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.TakeAim))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Take Aim";
                DeactivatedAll();
                TakeAim.SetActive(true);
                TakeAimPath.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.TakeAim))
        {
            DeactivatedAll();
            skillNameText.text = "Take Aim";
            TakeAim.SetActive(true);
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }

    //Guns Skills

    public void UnlockSlingShot()
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.SlingShotArrow))
        {
            skills.TryUnlockSkill(PlayerSkills.SkillType.SlingShotArrow);
            if (!skills.TryUnlockSkill(PlayerSkills.SkillType.SlingShotArrow))
            {
                Debug.Log("Cannot Unlock!");
            }
            else
            {
                skillNameText.text = "Sling Shot";
                DeactivatedAll();
                SlingShot.SetActive(true);
            }
        }
        else if (skills.unlockedSkillTypeList.Contains(PlayerSkills.SkillType.SlingShotArrow))
        {
            DeactivatedAll();
            skillNameText.text = "Sling Shot";
            SlingShot.SetActive(true);
        }
        else
        {
            GameManager.Instance.WarnedSkillPoint();
        }
    }
    
    
}
