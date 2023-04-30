using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentamount;

    public bool IsReached()
    {
        return (currentamount >= requiredAmount);
    }
    public void QuestGoblinKilled()
    {
        if (goalType == GoalType.KillGoblin)
        currentamount++;
    }
    public void QuestGoblinKilled2()
    {
        if (goalType == GoalType.KillGoblin2)
            currentamount++;
    }
    public void QuestItemCollected()
    {
        if (goalType == GoalType.Gathering)
            currentamount++;
    }

}
public enum GoalType
{
    KillGoblin2,
    KillGoblin,
    Gathering,
    Chat,
    Escort
}
