using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
    }
    public void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
        {
             unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });

        }
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
            case SkillType.Camoflage: return SkillType.TakeDown;
            case SkillType.SlingShotArrow: return SkillType.TakeAim | SkillType.Camoflage;

        }
        return SkillType.None;
    }
    public bool CanUseThisSkill(SkillType skillType)
    {
        if (IsSkillUnlocked(skillType))
        {
            UnlockSkill(skillType);
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
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            UnlockSkill(skillType);
            return true;
        }
    }
}
