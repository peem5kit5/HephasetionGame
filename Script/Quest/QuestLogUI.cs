using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    public static QuestLogUI Instance;
    public GameObject questButtonPrefab;
    public Transform questListPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI goal;

    private QuestManager questManager;

    private void Start()
    {
        Instance = this;
        questManager = FindObjectOfType<QuestManager>();
        UpdateQuestListPanel();
    }

    private void UpdateQuestListPanel()
    {
        int x = 0;
        int y = 0;
        float spacingY = 100f;
        // Destroy all existing quest buttons
        foreach (Transform child in questListPanel)
        {
            Destroy(child.gameObject);
        }
        //questButtons.Clear();
        // Instantiate a button for each quest in the quest list
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

            // Add an onClick event to the button that displays the quest's name and description
            rect.GetComponent<Button>().onClick.AddListener(() =>
            {
                titleText.text = quest.title;
                descriptionText.text = quest.description;
                moneyText.text = quest.moneyReward.ToString();
                itemText.text = quest.itemReward.ToString();
                goal.text = quest.goal.goalType.ToString();
            });
        }
    }

    public void QuestAdded(Quest quest)
    {
        UpdateQuestListPanel();
    }

    public void QuestRemoved(Quest quest)
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
