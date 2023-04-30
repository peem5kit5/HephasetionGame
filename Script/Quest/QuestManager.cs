using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<Quest> storedQuest = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();
    public List<Quest> finishedQuests = new List<Quest>();
    private Player player;
   // QuestLog log;
  //  public delegate void OnQuestChange(List<Quest> activeQuests, List<Quest> completedQuests, List<Quest> finishedQuest);
  //  public static event OnQuestChange onQuestChange;

    private void Awake()
    {
        
        //QuestLog.onQuestChange += UpdateQuestToLog;
    }
    public void Start()
    {

        Instance = this;
        player = FindObjectOfType<Player>();
        Load();



    }
    private void Update()
    {
       
            foreach (Quest quest in completedQuests)
            {
                quest.isCompleted = true;
                quest.isActive = true;
               
                
                
            }
            foreach (Quest quest in activeQuests)
            {
                quest.isActive = true;
                quest.isCompleted = false;
            

        }
            foreach (Quest quest in finishedQuests)
            {
                quest.isCompleted = true;
                quest.isActive = false;
           
        }
          
        
    }

    public void CompletedQuests(Quest quest)
    {
        player.activeQuest.Remove(quest);
        player.completedQuest.Add(quest);
        completedQuests.Add(quest);
        activeQuests.Remove(quest);
        QuestLogUI.Instance.QuestRemoved(quest);
        QuestLogUI.Instance.QuestUpdated(quest);
        quest.isCompleted = true;
        quest.isActive = true;
        Save();
        
       

    }
    public void FinishQuests(Quest quest)
    {
        player.completedQuest.Remove(quest);
        player.finishedQuest.Add(quest);
        finishedQuests.Add(quest);
        completedQuests.Remove(quest);
      
        quest.isActive = false;
        quest.isCompleted = true;
        Save();    //   onQuestChange?.Invoke(activeQuests, completedQuests, finishedQuests);


    }
    public void ActiveQuests(Quest quest)
    {
        player.activeQuest.Add(quest);
        activeQuests.Add(quest);
        QuestLogUI.Instance.QuestAdded(quest);
        QuestLogUI.Instance.QuestUpdated(quest);
         quest.isActive = true;
         quest.isCompleted = false;
        Save();
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
    public void Load()
    {
        string savePath = Application.persistentDataPath + "/questData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            QuestData saveData = JsonUtility.FromJson<QuestData>(json);
            activeQuests = saveData.activeQuests;
            completedQuests = saveData.finishedQuests;
            finishedQuests = saveData.finishedQuests;
            player.activeQuest = saveData.activeQuests;
            player.completedQuest = saveData.completedQuests;
            player.finishedQuest = saveData.finishedQuests;
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
