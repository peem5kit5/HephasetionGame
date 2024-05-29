using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDetecter : MonoBehaviour
{
    public GameObject QuestNaviatorPrefab;

    public GameObject Quest5Mecha;
    Player player;
    Transform uiPanel;
    public Camera uiCam;
    public Transform outsideBarPos;
    public Transform insideBarPos;
    public Transform insideSlumPos;
    public Transform outsideSlumPos;
    public Transform insideDesert;
    public Transform outsideDesert;


    public Transform GoTo_Tanium;
    public Transform SuperSoldierPos;


    public Transform Ho;
    public Transform BlackSmith;
    public Transform MadDoctor;
    public Transform Sera;

    public Transform Batholo;

    public Transform Quest3Area;
    public Transform Quest4Area;
    public Transform Quest5_1Area;
    public Transform Quest5_2Area;
    public Transform Quest6Area;
    public Transform Quest6_1Area;
    public Transform Quest6_2Area;
    public Transform Quest6_3Area;
    public Transform Quest7Area;
    public Transform Quest8Area;
    public Transform Quest9Area;
    public Transform Quest11Area;

    public Transform SeraQuest1;
    public Transform SeraQuest2;
    bool isHavingNav;
    bool isChangingNav;

    bool moving;
    public List<GameObject> storeArrow = new List<GameObject>();

    public GameObject QuestObject5;
    public GameObject QuestObject11;
    public GameObject SuperSoldierQuest9;
    public Transform MiladyPos;
    public Transform NathanPos;
    public GameObject Area1SeraQuest;
    public GameObject Area2SeraQuest;

    public Transform lolipopPlace;
    public GameObject Lolipop;
    bool isHavingLolipop;

    public Item itemPigman;
    public Item itemEagle;

    public bool isFollowingArrow;
    public enum Map
    {
        Bar,
        Slum,
        SlumBattle,
        Tanium,
        Desert
    }
    public Map type;
    void Start()
    {
        player = FindObjectOfType<Player>();
        uiPanel = transform.Find("UI");
        isHavingNav = false;
        isChangingNav = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMap();
        if (isFollowingArrow)
        {
            foreach(GameObject arrow in storeArrow)
            {
                Nemesis nem = FindObjectOfType<Nemesis>();
                arrow.GetComponent<QuestNavigator>().GetPositionForQuest(nem.transform.position);
            }
        }
    }
    public void DestroyAllArrow()
    {
        foreach(GameObject obj in storeArrow)
        {
            if(obj != null)
            {
                Destroy(obj);
            }
            else
            {
                return;
            }
        }
    }
  public void GetItemQuest5(Vector2 point)
    {
        Instantiate(QuestObject5, point, Quaternion.identity);
    }
    public void GetArrowToDestroy(Vector2 point)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(point, 4f);

        foreach(Collider2D col in collider)
        {
            if (col.CompareTag("Arrow") && col != null)
            {
                Debug.Log("Arrow collider detected. Destroying...");
                Destroy(col.gameObject);


            }
        }
      
    }

    public void CreatedArrow(Vector2 pos)
    {
        DestroyAllArrow();
        RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
        QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
        storeArrow.Add(QuestNav.gameObject);
        QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(pos);
    }
    public void CheckMap()
    {
        switch (type)
        {
            default:
            case Map.Bar:
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (!isHavingNav)
                    {
                        if (quest.goal.goalType == GoalType.BatholoQuest1 | quest.goal.goalType == GoalType.BatholoQuest2 | quest.goal.goalType == GoalType.BatholoQuest3 | quest.goal.goalType == GoalType.BatholoQuest4 | quest.goal.goalType == GoalType.BatholoQuest5 | quest.goal.goalType == GoalType.BatholoQuest6 | quest.goal.goalType == GoalType.BatholoQuest7 | quest.goal.goalType == GoalType.BatholoQuest8)
                        {
                            DestroyAllArrow();
                            
                            RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                            QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                            QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(outsideBarPos.position);
                            storeArrow.Add(QuestNav.gameObject);
                            isHavingNav = true;
                        }


                    }
                    
                }
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {
                    if (!isChangingNav)
                    {
                        if (quest.goal.goalType == GoalType.BatholoQuest1 | quest.goal.goalType == GoalType.BatholoQuest2 | quest.goal.goalType == GoalType.BatholoQuest3 | quest.goal.goalType == GoalType.BatholoQuest4 | quest.goal.goalType == GoalType.BatholoQuest5 | quest.goal.goalType == GoalType.BatholoQuest6 | quest.goal.goalType == GoalType.BatholoQuest7 | quest.goal.goalType == GoalType.BatholoQuest8)
                        {
                            DestroyAllArrow();
                            RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                            QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                            QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Batholo.position);
                            storeArrow.Add(QuestNav.gameObject);
                            isChangingNav = true;
                        }

                    }

                }
                break;
            case Map.Slum:
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if(quest != null)
                    {
                        if (!isHavingNav)
                        {
                            if (quest.goal.goalType == GoalType.BatholoQuest1)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav1 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav1.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                RectTransform QuestNav2 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav2.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                RectTransform QuestNav3 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav3.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                storeArrow.Add(QuestNav1.gameObject);
                                storeArrow.Add(QuestNav2.gameObject);
                                storeArrow.Add(QuestNav3.gameObject);
                                Debug.Log("Created Arrow");
                                QuestNav1.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Ho.position);
                                QuestNav2.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(BlackSmith.position);
                                QuestNav3.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(MadDoctor.position);
                                isHavingNav = true;
                            }
                            else if(quest.goal.goalType == GoalType.BatholoQuest2)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                storeArrow.Add(QuestNav.gameObject);
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Sera.position);
                                isHavingNav = true;
                            }
                            else if ( quest.goal.goalType == GoalType.BatholoQuest3 | quest.goal.goalType == GoalType.BatholoQuest4 | quest.goal.goalType == GoalType.BatholoQuest5 | quest.goal.goalType == GoalType.BatholoQuest6 | quest.goal.goalType == GoalType.BatholoQuest7 | quest.goal.goalType == GoalType.BatholoQuest8 | quest.goal.goalType == GoalType.BatholoQuest9 | quest.goal.goalType == GoalType.BatholoQuest10 | quest.goal.goalType == GoalType.BatholoQuest11 | quest.goal.goalType == GoalType.BatholoQuest12)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(outsideSlumPos.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }

                        }
                        if(quest.goal.goalType == GoalType.BatholoQuest7 | quest.goal.goalType == GoalType.BatholoQuest8 | quest.goal.goalType == GoalType.BatholoQuest9 | quest.goal.goalType == GoalType.BatholoQuest10 | quest.goal.goalType == GoalType.BatholoQuest11 | quest.goal.goalType == GoalType.BatholoQuest12)
                        {
                            MapSound.Instance.audioSource.Stop();
                        }
                        
                     
                    }
                 
                    
                }
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {
                    if (!isChangingNav)
                    {
                        DestroyAllArrow();
                        if (quest.goal.goalType == GoalType.BatholoQuest1 | quest.goal.goalType == GoalType.BatholoQuest2 | quest.goal.goalType == GoalType.BatholoQuest3 | quest.goal.goalType == GoalType.BatholoQuest4 | quest.goal.goalType == GoalType.BatholoQuest5 | quest.goal.goalType == GoalType.BatholoQuest6 | quest.goal.goalType == GoalType.BatholoQuest7 | quest.goal.goalType == GoalType.BatholoQuest8)
                        {
                            RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                            QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                            QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideBarPos.position);
                            storeArrow.Add(QuestNav.gameObject);
                            isChangingNav = true;
                        }

                    }
                  

                }
                break;
            case Map.SlumBattle:
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (quest != null)
                    {
                        if (quest.goal.goalType == GoalType.KillGoblin)
                        {
                            Area1SeraQuest.SetActive(true);
                        }
                        if (quest.goal.goalType == GoalType.KillGoblin2)
                        {
                            Area2SeraQuest.SetActive(true);
                        }
                        if (!isHavingLolipop)
                        {
                            if(quest.goal.goalType == GoalType.MysteriousGirlQuest)
                            {
                                Instantiate(Lolipop, lolipopPlace.position, Quaternion.identity);
                                isHavingLolipop = true;
                            }
                        }
                    }
                }
                    foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (!isHavingNav)
                    {
                        if(quest != null)
                        {
                            
                            if (quest.goal.goalType == GoalType.BatholoQuest3)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest3Area.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest4)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest4Area.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest5)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest5_1Area.position);
                                RectTransform QuestNav2 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav2.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav2.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest5_2Area.position);
                                GameObject Mecha = Instantiate(Quest5Mecha, Quest5_1Area.position,Quaternion.identity);
                                Mecha.AddComponent<QuestBender>();
                                Mecha.GetComponent<QuestBender>().whatIsThis = QuestBender.Object.Enemy;
                                Mecha.GetComponent<QuestBender>().number = QuestBender.QuestNum.Batholo5;
                                Mecha.GetComponent<QuestBender>().radius = 8f;
                                storeArrow.Add(QuestNav.gameObject);
                                storeArrow.Add(QuestNav2.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest6)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest6Area.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest7)
                            {
                                isFollowingArrow = true;
                                isHavingNav = true;
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest8)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest8Area.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest9)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(SuperSoldierPos.position);
                                GameObject superobj = Instantiate(SuperSoldierQuest9, SuperSoldierPos.position, Quaternion.identity);
                       
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest10)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(GoTo_Tanium.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                           
                        }
                       

                    }
                   
                   

                }
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {
                    if (!isChangingNav)
                    {
                        if(quest != null)
                        {
                            if (quest.goal.goalType == GoalType.BatholoQuest2)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideSlumPos.position);
                                isChangingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest3)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideSlumPos.position);
                                isChangingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest4)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideSlumPos.position);
                                player.inventory.AddItem(itemPigman);
                                isChangingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest5)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideSlumPos.position);
                                
                                isChangingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest6)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideSlumPos.position);
                                isChangingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest7)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideSlumPos.position);
                                isChangingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest8)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideSlumPos.position);
                                isChangingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest9)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(GoTo_Tanium.position);
                                isChangingNav = true;
                            }
                        }
                      

                    }
                   

                }
                break;
            case Map.Desert:
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (!isHavingNav)
                    {
                        if (quest != null)
                        {
                            if (quest.goal.goalType == GoalType.BatholoQuest6)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav1 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav1.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav1.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest6_1Area.position);
                                storeArrow.Add(QuestNav1.gameObject);
                                RectTransform QuestNav2 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav2.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav2.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest6_2Area.position);
                                storeArrow.Add(QuestNav2.gameObject);
                                RectTransform QuestNav3 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav3.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav3.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest6_3Area.position);
                                storeArrow.Add(QuestNav3.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest8)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav1 = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav1.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav1.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(Quest8Area.position);
                                storeArrow.Add(QuestNav1.gameObject);
                                isHavingNav = true;
                            }
                        }
                    }
                }
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {
                    if (!isChangingNav)
                    {
                        if (quest != null)
                        {
                            if (quest.goal.goalType == GoalType.BatholoQuest6)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideDesert.position);
                                isChangingNav = true;
                            }
                            if (quest.goal.goalType == GoalType.BatholoQuest8)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(insideDesert.position);
                                player.inventory.AddItem(itemEagle);
                                isChangingNav = true;
                            }
                        }
                    }
                }
                break;
            case Map.Tanium:
                foreach (Quest quest in QuestManager.Instance.activeQuests)
                {
                    if (!isHavingNav)
                    {
                        if (quest != null)
                        {
                            if (quest.goal.goalType == GoalType.BatholoQuest10)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(MiladyPos.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest11)
                            {
                                DestroyAllArrow();

                                Instantiate(QuestObject11, Quest11Area.position, Quaternion.identity);

                                isHavingNav = true;
                            }
                            else if (quest.goal.goalType == GoalType.BatholoQuest12)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(NathanPos.position);
                                storeArrow.Add(QuestNav.gameObject);
                                isHavingNav = true;
                            }
                        }

                    }
                  
                 
                }
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {
                    if (!isChangingNav)
                    {
                        if (quest != null)
                        {
                           
                            if (quest.goal.goalType == GoalType.BatholoQuest11)
                            {
                                DestroyAllArrow();
                                RectTransform QuestNav = Instantiate(QuestNaviatorPrefab, uiPanel).GetComponent<RectTransform>();
                                QuestNav.gameObject.GetComponent<QuestNavigator>().uiCam = uiCam;
                                QuestNav.gameObject.GetComponent<QuestNavigator>().GetPositionForQuest(NathanPos.position);
                                isChangingNav = true;
                            }
                           
                        }
                    }
                }

                break;
        }
    }
}
