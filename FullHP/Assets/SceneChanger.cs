using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animChangingAnim;

    public enum SceneTo
    {
        BarToSlum,
        SlumToBar,
        SlumToBattle,
        CityToBattle,
        DesertToBattle,
        BattleToSlum,
        BattleToDesert,

        BattleToTanium,
        TaniumToBattle
    }
    public SceneTo sceneGo;
    // Start is called before the first frame update
    private void Update()
    {
     
    }
    IEnumerator CheckSceneSave()
    {
        animChangingAnim.SetTrigger("ChangeGo");
        yield return new WaitForSeconds(1);
        switch (sceneGo)
        {
            default:
            case SceneTo.BarToSlum:
                SaveManager.Instance.SaveAll();
                SaveManager.Instance.Bar_Slum();
                SaveManager.Instance.LoadToScene();
                break;
            case SceneTo.SlumToBar:
                SaveManager.Instance.SaveAll();
                if (!GameManager.Instance.BatholoDead)
                {
                    if (!GameManager.Instance.EagleDefeated)
                    {
                        SaveManager.Instance.Slum_Bar();
                        SaveManager.Instance.LoadToScene();
                    }
                    else
                    {
                  
                        GameManager.Instance.BatholoDead = true;
                        SceneManager.LoadScene("Batholo_Deathscene");
                    }
                }
                
              
             
                break;
            case SceneTo.SlumToBattle:
                SaveManager.Instance.SaveAll();
                SaveManager.Instance.Slum_Battle();
                SaveManager.Instance.LoadToScene();
                break;
            case SceneTo.CityToBattle:
                SaveManager.Instance.SaveAll();
                SaveManager.Instance.City_Battle();
                SaveManager.Instance.LoadToScene();
                break;
            case SceneTo.DesertToBattle:
                SaveManager.Instance.SaveAll();
                SaveManager.Instance.Desert_Battle();
                SaveManager.Instance.LoadToScene();
                break;
            case SceneTo.BattleToSlum:
                SaveManager.Instance.SaveAll();
                SaveManager.Instance.Battle_Slum();
                SaveManager.Instance.LoadToScene();

               // case SceneTo.
                break;
            case SceneTo.BattleToDesert:
                SaveManager.Instance.SaveAll();
                SaveManager.Instance.Battle_Desert();
                SaveManager.Instance.LoadToScene();

                // case SceneTo.
                break;
            case SceneTo.BattleToTanium:
                if(QuestManager.Instance.activeQuests.Count > 0)
                {
                    foreach(Quest quest in QuestManager.Instance.activeQuests)
                    {
                        if(quest.goal.goalType == GoalType.BatholoQuest10 | quest.goal.goalType == GoalType.BatholoQuest11 | quest.goal.goalType == GoalType.BatholoQuest12 | GameManager.Instance.NathanDefeated)
                        {
                            SaveManager.Instance.SaveAll();
                            SaveManager.Instance.Battle_Tanium();
                            SaveManager.Instance.LoadToScene();
                        }
                     
                    }
                }
              
                break;
            case SceneTo.TaniumToBattle:
                SaveManager.Instance.SaveAll();
                SaveManager.Instance.Tanium_Battle();
                SaveManager.Instance.LoadToScene();

                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") | collision.CompareTag("Invisible"))
        {
           
            StartCoroutine(CheckSceneSave());
        }
    }
   
}
