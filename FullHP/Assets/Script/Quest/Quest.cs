using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class Quest
{
    public bool isActive;
    public bool isCompleted;

    public string title;
    public string description;
    public int moneyReward;
    public Item itemReward;
    public Image itemImage;

    public QuestGoal goal;
    public string GoalDes;
    public void Complete()
    {
        isActive = false;
    }
}
