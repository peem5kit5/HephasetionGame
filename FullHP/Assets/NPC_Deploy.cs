using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Deploy : MonoBehaviour
{
 

    public GameObject Roger;
    public GameObject RogerQuest;

    public GameObject Cat;
    public GameObject Elf;

    public Transform RogerPlace;
    public Transform CatPlace;
    public Transform ElfPlace;
    bool Deployed;

    void Start()
    {
        Deployed = false;
    }
    void CheckMap()
    {

        if (QuestManager.Instance.completedQuests.Count > 0)
        {
            if (!Deployed)
            {
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {
                    if (quest != null)
                    {
                        if (quest.goal.goalType == GoalType.BatholoQuest2)
                        {
                            Instantiate(Roger, RogerPlace.position, Quaternion.identity);
                            Debug.Log("Hey");
                            Deployed = true;
                        }
                        else if (quest.goal.goalType == GoalType.BatholoQuest4)
                        {
                            Instantiate(Cat, CatPlace.position, Quaternion.identity);
                            Deployed = true;
                        }
                        else if (quest.goal.goalType == GoalType.BatholoQuest6)
                        {
                            Instantiate(Elf, ElfPlace.position, Quaternion.identity);
                            Deployed = true;
                        }
                    }

                }


            }
        }
       
        
    }
    // Update is called once per frame
    void Update()
    {
       
            CheckMap();
            
            
        
        
    }
}
