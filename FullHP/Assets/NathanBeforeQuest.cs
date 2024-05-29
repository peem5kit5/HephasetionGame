using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NathanBeforeQuest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") | collision.CompareTag("Invisible"))
        {
            Debug.Log("Tag PLayer");
            CheckQuest();
        }
    }
    void CheckQuest()
    {
        List<Quest> questing = new List<Quest>();
           foreach (Quest quest in QuestManager.Instance.activeQuests)
            {
                if(quest.goal.goalType == GoalType.BatholoQuest11)
                {
                    questing.Add(quest);
                    Debug.Log("Add Quest");
                }
            }
        
        foreach(Quest quest in questing)
        {
            QuestManager.Instance.CompletedQuests(quest);
            QuestManager.Instance.ActiveQuests(QuestManager.Instance.quest12);
            Debug.Log("Completed Quest");
        }
       
    }
}
