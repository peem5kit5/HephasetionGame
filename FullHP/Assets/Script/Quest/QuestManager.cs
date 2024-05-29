using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<Quest> storedQuest = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();
    public List<Quest> finishedQuests = new List<Quest>();
    private Player player;

    public TextMeshProUGUI textQuestUpdated;
    public Animator anim;

    public Quest quest9;
    public Quest quest10;
    public Quest quest11;
    public Quest quest12;
   // QuestLog log;
  //  public delegate void OnQuestChange(List<Quest> activeQuests, List<Quest> completedQuests, List<Quest> finishedQuest);
  //  public static event OnQuestChange onQuestChange;

    public void Start()
    {

        Instance = this;
        player = FindObjectOfType<Player>();
      //  Load();



    }
  

    public void CompletedQuests(Quest quest)
    {
        player.activeQuest.Remove(quest);
        player.completedQuest.Add(quest);
        completedQuests.Add(quest);
        activeQuests.Remove(quest);
        QuestLogUI.Instance.QuestRemoved();
        QuestLogUI.Instance.QuestUpdated(quest);
        quest.isCompleted = true;
        quest.isActive = true;
        textQuestUpdated.color = Color.yellow;
        textQuestUpdated.text = "Quest Completed " + quest.title;
        anim.SetTrigger("Quest_Updated");

        
       

    }
    public void FinishQuests(Quest quest)
    {
        player.completedQuest.Remove(quest);
   
        completedQuests.Remove(quest);
        textQuestUpdated.color = Color.green;
        textQuestUpdated.text = "Quest Finished " + quest.title;
        anim.SetTrigger("Quest_Updated");
        quest.isActive = false;
        quest.isCompleted = true;
   //   onQuestChange?.Invoke(activeQuests, completedQuests, finishedQuests);


    }
    public void FinishQuestsStore(Quest quest)
    {
        player.completedQuest.Remove(quest);

        completedQuests.Remove(quest);
        finishedQuests.Add(quest);
        textQuestUpdated.color = Color.green;
        textQuestUpdated.text = "Quest Finished " + quest.title;
        anim.SetTrigger("Quest_Updated");
        quest.isActive = false;
        quest.isCompleted = true;
        //   onQuestChange?.Invoke(activeQuests, completedQuests, finishedQuests);


    }

    public void ActiveQuests(Quest quest)
    {
        player.activeQuest.Add(quest);
        activeQuests.Add(quest);
        QuestLogUI.Instance.QuestAdded();
     //   QuestLogUI.Instance.QuestUpdated(quest);
         quest.isActive = true;
         quest.isCompleted = false;
        textQuestUpdated.color = Color.white;
        textQuestUpdated.text = "Quest Accept " + quest.title;
        anim.SetTrigger("Quest_Updated");

        //QuestLogUI.Instance.QuestAdded(questA);
        //  QuestLogUI.Instance.QuestUpdated(questA);
        // onQuestChange?.Invoke(activeQuests, completedQuests, finishedQuests);
        //  playerData.SavePlayerData();


    }
    public Quest[] GetQuestList()
    {
        return activeQuests.ToArray();
    }

    public List<Quest> GetActiveQuests()
    {
        return activeQuests;
    }
    public List<Quest> GetCompletedQuests()
    {
        return completedQuests;
    }
    public List<Quest> GetFinishedQuests()
    {
        return finishedQuests;
    }
   public void Save()
    {
        string savePath = Application.persistentDataPath + "/questData.json";
        string json = JsonUtility.ToJson(new QuestData(activeQuests,completedQuests,finishedQuests));
        File.WriteAllText(savePath, json);
    }
    public void DeleteSave()
    {
        string savePath = Application.persistentDataPath + "/questData.json";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
    public void Load()
    {
        string savePath = Application.persistentDataPath + "/questData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            QuestData saveData = JsonUtility.FromJson<QuestData>(json);
           
            activeQuests = saveData.activeQuests;
            completedQuests = saveData.completedQuests;
            finishedQuests = saveData.finishedQuests;
            QuestLogUI.Instance.QuestAdded();
            
           
            List<Quest> questing = new List<Quest>();
            if (GameManager.Instance.BatholoDead)
            {

                if (QuestManager.Instance.completedQuests.Count > 0)
                {

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {
                        if (quest.goal.goalType == GoalType.BatholoQuest8)
                        {
                            questing.Add(quest);
                            //GameManager.Instance.BatholoDead = true;
                        }
                    }

                }
                foreach (Quest quest8 in questing)
                {
                    if (questing.Count > 0)
                    {
                        QuestManager.Instance.FinishQuests(quest8);
                        QuestManager.Instance.ActiveQuests(QuestManager.Instance.quest9);
                        QuestLogUI.Instance.QuestAdded();
                    }

                }

                
            }
           

        }
    }
}
[System.Serializable]
public class QuestData
{
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public List<Quest> finishedQuests;

    public QuestData(List<Quest> activeQ, List<Quest> completedQ, List<Quest> finishedQ)
    {
        this.activeQuests = activeQ;
        this.completedQuests = completedQ;
        this.finishedQuests = finishedQ;
    }
}
