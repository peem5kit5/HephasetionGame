using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class PlayerSkills
{
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }
   
    public enum SkillType
    {
        None,

        //Utilites Skills
        CQC,// No
        DoorSweeper, // CQC
        TacticalStance, // CQC
        WindLanguage, // Door and Tac

        Vengeance, // No
        Ember, // Vengeance
        TrapMine, // Vengeance
        GodLight,// Ember and Trap

        
        TakeDown, // No
        TakeAim, // TakeDown
        Camoflage, // TakeDown
        SlingShotArrow, // Take Aim and Camoflage

    }
    public List<SkillType> unlockedSkillTypeList;

    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
       // Load();
    }
    public void UnlockSkill(SkillType skillType)
    {
        if (LevelSystem.Instance.skillPoint >= 1 && !unlockedSkillTypeList.Contains(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
            LevelSystem.Instance.skillPoint--;

        }
        
           //LevelSystem.Instance.skillPoint--;
        
    }
    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }
    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            //Utilities Skills
            case SkillType.DoorSweeper: return SkillType.CQC;
            case SkillType.TacticalStance: return SkillType.CQC ;
            case SkillType.WindLanguage: return SkillType.DoorSweeper | SkillType.TacticalStance;
            //Sword Skills
            case SkillType.Ember: return SkillType.Vengeance;
            case SkillType.TrapMine: return SkillType.Vengeance;
            case SkillType.GodLight: return SkillType.Ember | SkillType.TrapMine;
            //Gun Skills
            case SkillType.TakeAim: return SkillType.TakeDown;
            case SkillType.Camoflage:  return SkillType.TakeDown;
            case SkillType.SlingShotArrow:  return SkillType.TakeAim | SkillType.Camoflage;

        }
        return SkillType.None;
    }
    public bool CanUseThisSkill(SkillType skillType)
    {
        if (IsSkillUnlocked(skillType))
        {
            UnlockSkill(skillType);
           // Save();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool TryUnlockSkill(SkillType skillType)
    {
        SkillType skillRequire = GetSkillRequirement(skillType);

        if(skillRequire != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequire))
            {
                UnlockSkill(skillType);
                
                Debug.Log("Skill Init");
             //   Save();
                return true;
            }
            else
            {
                Debug.Log("No Skill");
                return false;
                
            }
        }
        else
        {
            UnlockSkill(skillType);
           // Save();
            Debug.Log("Skill Init");
            return true;
        }
    }
    public void Save()
    {
        string savePath = Application.persistentDataPath + "/skillData.json";
        string json = JsonUtility.ToJson(new SkillData(unlockedSkillTypeList));
        File.WriteAllText(savePath, json);
    }
    public void DeleteSave()
    {
        string savePath = Application.persistentDataPath + "/skillData.json";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
    public void Load()
    {
        string savePath = Application.persistentDataPath + "/skillData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SkillData saveData = JsonUtility.FromJson<SkillData>(json);
            unlockedSkillTypeList = saveData.unlockedSkillType;
            foreach(PlayerSkills.SkillType skill in saveData.unlockedSkillType)
            {
                OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skill });
                //UnlockSkill(skill);
            }
            
           
            Debug.Log(savePath);
        }
    }
   
}

[Serializable]
public class SkillData
{
    public List<PlayerSkills.SkillType> unlockedSkillType;

    public SkillData(List<PlayerSkills.SkillType> unlockedSkillType)
    {
        this.unlockedSkillType = unlockedSkillType;
    }
}

