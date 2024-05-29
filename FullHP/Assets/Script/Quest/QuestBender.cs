using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBender : MonoBehaviour
{
    public enum QuestNum
    {
        None,
        Batholo5,
        Batholo6,
        Batholo9
    }
    public QuestNum number;
    QuestDetecter questdetecter;
    EnemyHP enemyHP;
    public float radius;
    bool reached;
    bool reachedEnemy;

    void Start()
    {
        reached = false;
        questdetecter = FindObjectOfType<QuestDetecter>();
        enemyHP = GetComponent<EnemyHP>();
    }
    void CheckQuest()
    {
        switch (number)
        {
            default:
            case QuestNum.None:
                questdetecter.GetArrowToDestroy(this.transform.position);
                reached = true;
                break;
            case QuestNum.Batholo5:
                questdetecter.GetItemQuest5(this.transform.position);

                reached = true;
                break;
            case QuestNum.Batholo6:
                List<Quest> questin6 = new List<Quest>();

                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {

                    if (quest.goal.goalType == GoalType.BatholoQuest6)
                    {
                      
                        quest.goal.BatholoTrack6();
                        if (quest.goal.IsReached())
                        {
                            questin6.Add(quest);
                        }
                      
                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
                foreach (Quest quest in questin6)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                }
                questdetecter.GetArrowToDestroy(this.transform.position);
                reached = true;
                break;
            case QuestNum.Batholo9:
                List<Quest> questin9 = new List<Quest>();

                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {

                    if (quest.goal.goalType == GoalType.BatholoQuest9)
                    {

                        quest.goal.BatholoTrack6();
                        if (quest.goal.IsReached())
                        {
                            questin9.Add(quest);
                        }

                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
                foreach (Quest quest in questin9)
                {
                    QuestManager.Instance.CompletedQuests(quest);
                    QuestManager.Instance.ActiveQuests(QuestManager.Instance.quest10);
                }
                questdetecter.GetArrowToDestroy(this.transform.position);
                reached = true;
                break;


        }
    }
    public enum Object
    {
        Enemy,
        Object
    }
    public Object whatIsThis;

    void CheckObject()
    {
        switch (whatIsThis)
        {
            default:
            case Object.Enemy:
              //  questdetecter.GetArrowToDestroy(this.transform.position );
             //  questdetecter.CreatedArrow(this.transform.position);
              
                break;
            case Object.Object:
             
                CheckQuest();
               
                break;

        }
        reached = true;

    }
    // Update is called once per frame
    void Update()
    {
        if(enemyHP != null)
        {

           
            if (enemyHP.currentHP <= 0)
            {
                if (!reached)
                {
                    questdetecter.GetArrowToDestroy(this.transform.position);
                    CheckQuest();
                    reached = true;
                }
               
                
            }
        }
        else
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, radius);
            foreach (Collider2D collider in col)
            {
                if (collider.CompareTag("Player"))
                {
                    if (!reached)
                    {
                        Debug.Log("Reach");
                        CheckObject();

                    }


                }
            }
        }
      
     
    }
   


}
