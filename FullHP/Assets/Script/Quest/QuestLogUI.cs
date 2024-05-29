using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    public static QuestLogUI Instance { get; private set; }
    public GameObject questButtonPrefab;
    public Transform questListPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI goal;
    public TextMeshProUGUI goalMax;
    private QuestManager questManager;


    private void Start()
    {
        
            Instance = this;
        
        
        questManager = FindObjectOfType<QuestManager>();
        UpdateQuestListPanel();
    }

    private void UpdateQuestListPanel()
    {
        List<GameObject> childList = new List<GameObject>();
        int x = 0;
        int y = 0;
        float spacingY = 100f;
        questListPanel.transform.DetachChildren();
        //questButtons.Clear();

        for (int i = 0; i < questManager.activeQuests.Count; i++)
        {
            Quest quest = questManager.activeQuests[i];
            RectTransform rect = Instantiate(questButtonPrefab, questListPanel).GetComponent<RectTransform>();
            //GameObject questButton = Instantiate(questButtonPrefab, questListPanel);
            rect.anchoredPosition = new Vector2(x, y * spacingY);
            x++;
            if(x > 0)
            {
                x = 0;
                y++;
            }
            
            rect.GetComponentInChildren<TextMeshProUGUI>().text = quest.title;
            int questIndex = i;

            rect.GetComponent<Button>().onClick.RemoveAllListeners();
 
             rect.GetComponent<Button>().onClick.AddListener(() =>
            {
                titleText.text = quest.title;
                descriptionText.text = quest.description;
                moneyText.text = quest.moneyReward.ToString();
                if(quest.itemReward.type != Item.ItemType.None)
                {
                    itemText.text = quest.itemReward.GetName().ToString();
                }
                else
                {
                    itemText.text = "";
                }

                goal.text = quest.goal.currentamount.ToString() + " /" + quest.goal.requiredAmount.ToString();
              //  goalMax.text = quest.goal.requiredAmount.ToString();
                if(quest.goal.currentamount >= quest.goal.requiredAmount)
                {
                    goal.text = "Completed";
                    goalMax.text = "";
                }
            });
        }
    }

    public void QuestAdded()
    {
        UpdateQuestListPanel();
    }

    public void QuestRemoved()
    {
        UpdateQuestListPanel();
    }

    public void QuestUpdated(Quest quest)
    {
        // Update the quest button text
        foreach (Transform child in questListPanel)
        {
            if (child.GetComponentInChildren<TextMeshProUGUI>().text == quest.title)
            {
                child.GetComponentInChildren<TextMeshProUGUI>().text = quest.title;
            }
        }
    }
}
