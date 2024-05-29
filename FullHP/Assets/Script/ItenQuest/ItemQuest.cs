using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuest : MonoBehaviour
{
    public enum ItemQuestType
    {
        Bag,
        Chip,
        BatholoDiary,
        GoldenCoin,
        Lolipop,
        IDcard
    }
    public ItemQuestType type;
    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") | collision.CompareTag("Invisible"))
        {
            CheckItemForQuest();
            Destroy(gameObject);
        }
    }
   void CheckItemForQuest()
    {
        switch (type)
        {
            case ItemQuestType.Bag:
                List<Quest> questing11 = new List<Quest>();
                foreach (Quest quest in player.activeQuest)
                {
                    if(quest.goal.goalType == GoalType.BatholoQuest11)
                    {
                        questing11.Add(quest);
                    }
                }
                foreach(Quest quest in questing11)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                }
               
                break;
            case ItemQuestType.Chip:
                List<Quest> questing5 = new List<Quest>();

                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {

                    if (quest.goal.goalType == GoalType.BatholoQuest5)
                    {


                        questing5.Add(quest);
                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
                foreach (Quest quest in questing5)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                }
                break;
            case ItemQuestType.BatholoDiary:
                List<Quest> questing7 = new List<Quest>();

                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {

                    if (quest.goal.goalType == GoalType.BatholoQuest7)
                    {


                        questing7.Add(quest);
                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
                foreach (Quest quest in questing7)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                }
                break;
            case ItemQuestType.IDcard:
                List<Quest> questing9 = new List<Quest>();

                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {

                    if (quest.goal.goalType == GoalType.BatholoQuest9)
                    {


                        questing9.Add(quest);
                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
                foreach (Quest quest in questing9)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                    QuestManager.Instance.ActiveQuests(QuestManager.Instance.quest10);

                }
                break;
            case ItemQuestType.GoldenCoin:
                foreach (Quest quest in player.activeQuest)
                {
                    if (quest.goal.goalType == GoalType.PridefulElfQuest)
                    {
                        QuestManager.Instance.CompletedQuests(quest);
                    }
                }
                break;
            case ItemQuestType.Lolipop:
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    List<Quest> questingMysterious = new List<Quest>();
                    foreach (Quest questGirl in QuestManager.Instance.activeQuests)
                    {
                        if (questGirl.goal.goalType == GoalType.MysteriousGirlQuest)
                        {
                            questingMysterious.Add(questGirl);
                        }
                    }
                    foreach (Quest questG in questingMysterious)
                    {
                        QuestManager.Instance.CompletedQuests(questG);
                    }

                }
                break;
        }
    }
}
