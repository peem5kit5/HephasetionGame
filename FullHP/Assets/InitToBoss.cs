using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitToBoss : MonoBehaviour
{
    public Animator anim;
    public enum Boss
    {
        Pigman,
        Eagle,
        Milady,
        Nathan
    }
    public Boss bosstype;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("ChangeGo");
            StartCoroutine(CheckBoss());
        }
    }
    public IEnumerator CheckBoss()
    {
        yield return new WaitForSeconds(1f);
        switch (bosstype)
        {
            default:
            case Boss.Pigman:
                foreach(Quest quest in QuestManager.Instance.activeQuests)
                {
                    if(quest.goal.goalType == GoalType.BatholoQuest4)
                    {
                        if (quest != null)
                        {
                            SaveManager.Instance.SaveAll();
                            SceneManager.LoadScene("Pigman_Cutscene");
                        }
                    
                    }
                    else
                    {
                        break;
                    }
                }
               
                break;
            case Boss.Eagle:
                
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (quest.goal.goalType == GoalType.BatholoQuest8)
                    {
                        if (quest != null)
                        {
                            SaveManager.Instance.SaveAll();
                            SceneManager.LoadScene("Eagle_Cutscene");
                        }
                            

                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case Boss.Milady:

                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (quest.goal.goalType == GoalType.BatholoQuest10)
                    {
                        if (quest != null)
                        {
                            SaveManager.Instance.SaveAll();
                            SceneManager.LoadScene("Milady_Cutscene");
                        }
                           
                    }
                    else
                    {
                        break;
                    }
                }
               

                break;
            case Boss.Nathan:
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (quest.goal.goalType == GoalType.BatholoQuest12)
                    {
                        if (quest != null)
                        {
                            SaveManager.Instance.SaveAll();
                            SceneManager.LoadScene("Nathan_Cutscene");
                        }
                            
                    }
                    else
                    {
                        break;
                    }
                }
                
                break;

        }
    }
   
}
