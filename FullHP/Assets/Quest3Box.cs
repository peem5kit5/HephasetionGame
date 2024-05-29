using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest3Box : MonoBehaviour
{
    Player player;
    QuestDetecter questDetecter;
    public bool isQuesting;

    public float maxGauge;
    public float currentGauge;

    public Slider questSlider;
    Nemesis nem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(QuestManager.Instance.activeQuests.Count > 0)
            {
                List<Quest> questsCopy = new List<Quest>(QuestManager.Instance.activeQuests);
                foreach(Quest quest in questsCopy)
                {
                    if (quest.goal.goalType == GoalType.BatholoQuest3)
                    {
                        currentGauge = 0;
                        isQuesting = true;
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isQuesting = false;
        }
    }
    void Start()
    {
        nem = FindObjectOfType<Nemesis>();
        questSlider.maxValue = maxGauge;
        questDetecter = FindObjectOfType<QuestDetecter>();
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        questSlider.value = currentGauge;
        if (isQuesting)
        {
            if(nem != null)
            {
                nem.sus = true;
            }
            if (!DeathEffectInteract.Instance.Slowed)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                Time.timeScale = 0.3f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

            }

            if(currentGauge < maxGauge)
            {
                currentGauge += Time.deltaTime;
            }
            else
            {
                if (QuestManager.Instance.activeQuests.Count > 0)
                {
                    List<Quest> questsCopy = new List<Quest>(QuestManager.Instance.activeQuests);
                    foreach (Quest quest in questsCopy)
                    {
                        if (quest.goal.goalType == GoalType.BatholoQuest3)
                        {
                            quest.goal.BatholoTrack3();
                            if (quest.goal.IsReached())
                            {
                                questDetecter.GetArrowToDestroy(this.transform.position);
                                QuestManager.Instance.CompletedQuests(quest);
                                isQuesting = false;

                            }
                        }
                    }
                }
            }
        }
        else
        {
            currentGauge = 0;
        }
    }
}
