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
    public void QuestGoblinKilled3()
    {
        if (goalType == GoalType.KillGoblin3)
            currentamount++;
    }
    public void QuestItemCollected()
    {
        if (goalType == GoalType.Gathering)
            currentamount++;
    }

    public void BatholoTrack1()
    {
        if (goalType == GoalType.BatholoQuest1)
            currentamount++;
    }
    public void BatholoTrack2()
    {
        if (goalType == GoalType.BatholoQuest2)
            currentamount++;
    }
    public void BatholoTrack3()
    {
        if (goalType == GoalType.BatholoQuest3)
            currentamount++;

    }
    public void BatholoTrack4()
    {

        if (goalType == GoalType.BatholoQuest4)
            currentamount++;
    }
    public void BatholoTrack5()
    {

        if (goalType == GoalType.BatholoQuest5)
            currentamount++;
    }
    public void BatholoTrack6()
    {

        if (goalType == GoalType.BatholoQuest6)
            currentamount++;
    }
    public void BatholoTrack7()
    {

        if (goalType == GoalType.BatholoQuest7)
            currentamount++;
    }
    public void BatholoTrack8()
    {

        if (goalType == GoalType.BatholoQuest8)
            currentamount++;
    }
    public void BatholoTrack9()
    {

        if (goalType == GoalType.BatholoQuest9)
            currentamount++;
    }
    public void BatholoTrack10()
    {

        if (goalType == GoalType.BatholoQuest10)
            currentamount++;
    }
    public void BatholoTrack11()
    {

        if (goalType == GoalType.BatholoQuest11)
            currentamount++;
    }
    public void BatholoTrack12()
    {

        if (goalType == GoalType.BatholoQuest12)
            currentamount++;
    }
}
public enum GoalType
{
    KillGoblin3,
    KillGoblin2,
    KillGoblin,
    Gathering,
    Chat,
    Escort,
    BatholoQuest1,
    BatholoQuest2,
    BatholoQuest3,
    BatholoQuest4,
    BatholoQuest5,
    BatholoQuest6,
    BatholoQuest7,
    BatholoQuest8,
    BatholoQuest9,
    BatholoQuest10,
    BatholoQuest11,
    BatholoQuest12,
    MysteriousGirlQuest,
    PridefulElfQuest,
    AnnoyedElfQuest,

}

