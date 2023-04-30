using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Skilltree : MonoBehaviour
{
    private PlayerSkills skills;


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

    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.skills = playerSkills;
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
        skills.TryUnlockSkill(PlayerSkills.SkillType.CQC);
        skillNameText.text = "CQC";
        DeactivatedAll();
        Debug.Log("CQC Init!");
        PathCQCPath.SetActive(true);
    }
    public void UnlockVengeance()
    {
        skills.TryUnlockSkill(PlayerSkills.SkillType.Vengeance);
        skillNameText.text = "Vengeance";
        DeactivatedAll();
        Debug.Log("Vengeance Init!");
        VengenPath.SetActive(true);
    }
    public void UnlockTakeDown()
    {
        skills.TryUnlockSkill(PlayerSkills.SkillType.TakeDown);
        skillNameText.text = "The Best Hunter";
        DeactivatedAll();
        TakeDownPath.SetActive(true);
    }
    public void UnlockDoorSweeper()
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
    public void UnlockTacticalStance()
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
    public void UnlockWindLanguage()
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
    public void UnlockCamoflage()
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

    //Sword Skills

   
    public void UnlockEmber()
    {
        skills.TryUnlockSkill(PlayerSkills.SkillType.Ember);
        if (!skills.TryUnlockSkill(PlayerSkills.SkillType.Ember))
        {
            Debug.Log("Cannot Unlock!");
        }
        else
        {
            skillNameText.text = "Swift";
            DeactivatedAll();
            Ember.SetActive(true);
            EmberPath.SetActive(true);
        }
    }
    public void UnlockTrapMine()
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
    public void UnlockGodLight()
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
    public void UnlockTakeAim()
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

    //Guns Skills

    public void UnlockSlingShot()
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
    
    
}
