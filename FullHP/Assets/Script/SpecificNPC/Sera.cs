using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Sera : MonoBehaviour
{
    public GameObject weapon;
    public TextMeshProUGUI nameText;
    public string NPCname;
    public Player player;

    public bool dialogueIsPlaying { get; private set; }
    public TextMeshProUGUI TalkDisplay;
 
    public float typingSpeed;
    public bool chatting = false;
    public bool playerinRange;
    public bool typing;
    public ToggleUI uiToggle;
    public GameObject notice;
    //public string Tag;
    public GameObject chatUI;
    public GameObject AcceptButton;
    public GameObject RefuseButton;
    //for Quest

    public Quest SeraQuest1;
    public Quest SeraQuest2;
    public string[] BatholoQuest;
    private int indexBatholoQuest;

    public string[] sentencesGetQuest;
    private int indexGetQuest;

    public string[] sentencesFinishQuest;
    private int indexFinishQuest;

    public string[] sentencesNotQuest;
    private int indexNotQuest;
    //  public int questLine;

    // public ItemAssets itemAssetNPC;
    public Image PlayerImage;
    public Image NPCImage;
    // end
    public int moneyReward;
    //this for disabled gun and camera

    public TextMeshProUGUI NPC_Name;
    public TextMeshProUGUI Player_Name;
    public Button continueButton;

    public Sera thisnpc;


    public Camera cam;
   public  bool alreadyHaveQuest;
    public GameObject FOV;
    public bool BatholoQuest2;
    public bool BatholoCompleted;
    bool Freplacement;
   public bool Completed;
    int random;
    // Start is called before the first frame update
    void Start()
    {
       


        this.thisnpc = this;
        //completedquest = false;
        player = FindObjectOfType<Player>();
        uiToggle = FindObjectOfType<ToggleUI>();
        alreadyHaveQuest = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            if (QuestManager.Instance.activeQuests.Count > 0)
            {
                foreach (Quest questing in QuestManager.Instance.activeQuests)
                {
                    if (questing.goal.goalType == GoalType.KillGoblin | questing.goal.goalType == GoalType.KillGoblin2)
                    {
                        alreadyHaveQuest = true;
                        Completed = false;
                        //quest = questing;
                    }
                }
            }
            if (QuestManager.Instance.completedQuests.Count > 0)
            {
                foreach (Quest questing in QuestManager.Instance.completedQuests)
                {
                    if (questing.goal.goalType == GoalType.KillGoblin | questing.goal.goalType == GoalType.KillGoblin2)
                    {
                        alreadyHaveQuest = true;
                    //    quest = questing;
                        Completed = true;
                    }
                }
            }

            notice.SetActive(true);
            gameObject.GetComponent<Sera>().thisnpc.playerinRange = true;


            Debug.Log("Chat");


        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        thisnpc.playerinRange = false;
        notice.SetActive(false);

    }
    public void NextSentence()
    {

        this.thisnpc.continueButton.enabled = false;
        if (BatholoQuest2 && !BatholoCompleted)
        {
            if (this.thisnpc.indexBatholoQuest < this.thisnpc.BatholoQuest.Length - 1)
            {
                this.thisnpc.indexBatholoQuest++;
                this.thisnpc.TalkDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                this.thisnpc.indexBatholoQuest = 0;
                this.thisnpc.TalkDisplay.text = "";
                this.thisnpc.chatting = false;
                this.thisnpc.typing = false;
                this.thisnpc.indexFinishQuest = 0;
                this.thisnpc.TalkDisplay.text = "";
                this.thisnpc.chatting = false;
                this.thisnpc.typing = false;
                




                if (QuestManager.Instance.activeQuests.Count > 0)
                {
                    List<Quest> questsCopy = new List<Quest>(QuestManager.Instance.activeQuests);
                    foreach (Quest quest in questsCopy)
                    {
                        if(quest.goal.goalType == GoalType.BatholoQuest2)
                        {
                            quest.goal.BatholoTrack2();
                            if (quest.goal.IsReached())
                            {
                                QuestManager.Instance.activeQuests.Remove(quest);
                                QuestManager.Instance.CompletedQuests(quest);
                            }
                        }
                    }
                }


                        this.chatUI.SetActive(false);
                this.uiToggle.IntoPlay();
                //QuestManager.Instance.FinishQuests(thisnpc.quest);
                // this.thisnpc.quest.Complete();
                // ItemWorld.SpawnItemWorld(player.gameObject.transform.position,itemGive);
                weapon.SetActive(true);
                BatholoQuest2 = false;
                BatholoCompleted = true;
                Freplacement = false;
                uiToggle.isChatting = false;
                // this.chatUI.SetActive(false);

                //  this.uiToggle.IntoPlay();
                //  weapon.SetActive(true);

            }
        }
        else if (!alreadyHaveQuest && !BatholoQuest2 && !Completed)
        {

            if (this.thisnpc.indexGetQuest < this.thisnpc.sentencesGetQuest.Length - 1)
            {
                this.thisnpc.indexGetQuest++;
                this.thisnpc.TalkDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                this.thisnpc.indexGetQuest = 0;
                this.thisnpc.TalkDisplay.text = "";
                this.thisnpc.chatting = false;
                this.thisnpc.typing = false;
               // this.chatUI.SetActive(false);
                AcceptButton.SetActive(true);
                RefuseButton.SetActive(true);
                Button buttonAccept = AcceptButton.GetComponent<Button>();
                Button buttonRefuse = RefuseButton.GetComponent<Button>();
                buttonAccept.onClick.RemoveAllListeners();
                buttonRefuse.onClick.RemoveAllListeners();
                buttonAccept.onClick.AddListener(() => AcceptQuestSera());
                buttonRefuse.onClick.AddListener(() => RefuseQuestSera());
                continueButton.gameObject.SetActive(false);
                uiToggle.isChatting = false;
                //  this.uiToggle.IntoPlay();
                //  weapon.SetActive(true);

            }
        }
        else if (alreadyHaveQuest && Completed)
        {
            if (this.thisnpc.indexFinishQuest < this.thisnpc.sentencesFinishQuest.Length - 1)
            {
                this.thisnpc.indexFinishQuest++;
                this.thisnpc.TalkDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                this.thisnpc.indexFinishQuest = 0;
                this.thisnpc.TalkDisplay.text = "";
                this.thisnpc.chatting = false;
                this.thisnpc.typing = false;
                this.chatUI.SetActive(false);
                this.uiToggle.IntoPlay();
                //QuestManager.Instance.FinishQuests(thisnpc.quest);
                // this.thisnpc.quest.Complete();
                // ItemWorld.SpawnItemWorld(player.gameObject.transform.position,itemGive);
                weapon.SetActive(true);

                alreadyHaveQuest = false;
                Completed = false;
                Freplacement = false;
                List<Quest> questing = new List<Quest>();
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {

                    if (quest.goal.goalType == GoalType.KillGoblin)
                    {
                        quest.goal.QuestGoblinKilled();
                        if (quest.goal.IsReached())
                        {
                            questing.Add(quest);
                            moneyReward += 500;
                            //inv.AddItem(npcQuest.itemReward);
                        }
                    }
                    else if (quest.goal.goalType == GoalType.KillGoblin2)
                    {
                        quest.goal.QuestGoblinKilled2();
                        if (quest.goal.IsReached())
                        {

                          
                            questing.Add(quest);
                            moneyReward += 1000;
                            //inv.AddItem(npcQuest.itemReward);
                        }
                    }
                }
                foreach (Quest quest in questing)
                {
                    QuestManager.Instance.FinishQuests(quest);
                    player.moneyCount += moneyReward;
                    
                }
                
                uiToggle.isChatting = false;


            }
        }
        else if (alreadyHaveQuest && !Completed)
        {
            if (this.thisnpc.indexNotQuest < this.thisnpc.sentencesNotQuest.Length - 1)
            {
                this.thisnpc.indexNotQuest++;
                this.thisnpc.TalkDisplay.text = "";
                StartCoroutine(Type());
                uiToggle.isChatting = false;
            }
            else
            {
                this.thisnpc.indexNotQuest = 0;
                this.thisnpc.TalkDisplay.text = "";
                this.thisnpc.chatting = false;
                this.thisnpc.typing = false;
                this.chatUI.SetActive(false);
                this.uiToggle.IntoPlay();
               // continueButton.gameObject.SetActive(false);
                player.moneyCount += moneyReward;
                Freplacement = false;
                uiToggle.isChatting = false;
                // this.thisnpc.quest.Complete();
                // ItemWorld.SpawnItemWorld(player.gameObject.transform.position,itemGive);
                weapon.SetActive(true);
            }
        }

        if (this.thisnpc.typing == true)
        {

            if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesGetQuest[indexGetQuest])
            {
                this.thisnpc.continueButton.enabled = true;

            }

            else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesFinishQuest[indexFinishQuest])
            {
                this.thisnpc.continueButton.enabled = true;

            }

            else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesNotQuest[indexNotQuest])
            {
                this.thisnpc.continueButton.enabled = true;

            }
            else if (this.thisnpc.TalkDisplay.text == this.thisnpc.BatholoQuest[indexBatholoQuest])
            {
                this.thisnpc.continueButton.enabled = true;

            }


        }


    }
    IEnumerator Type()
    {

        //  Debug.Log("Typing" + Tag);

        if (BatholoQuest2 && !BatholoCompleted)
        {
            foreach (char letter in this.thisnpc.BatholoQuest[indexBatholoQuest].ToCharArray())
            {
                this.thisnpc.TalkDisplay.text += letter;
                yield return new WaitForSeconds(thisnpc.typingSpeed);

            }
        }
        else if (!alreadyHaveQuest && !Completed)
        {
            foreach (char letter in this.thisnpc.sentencesGetQuest[indexGetQuest].ToCharArray())
            {
                this.thisnpc.TalkDisplay.text += letter;
                yield return new WaitForSeconds(thisnpc.typingSpeed);

            }
        }
        else if (alreadyHaveQuest && !Completed)
        {
            foreach (char letter in this.sentencesNotQuest[indexNotQuest].ToCharArray())
            {
                this.thisnpc.TalkDisplay.text += letter;
                yield return new WaitForSeconds(this.thisnpc.typingSpeed);

            }
        }
        else if (alreadyHaveQuest && Completed)
        {
            foreach (char letter in this.sentencesFinishQuest[indexFinishQuest].ToCharArray())
            {
                this.thisnpc.TalkDisplay.text += letter;
                yield return new WaitForSeconds(this.thisnpc.typingSpeed);

            }
        }



    }
    // Update is called once per frame
    void Update()
    {
        if (this.thisnpc.typing == true)
        {

            if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesGetQuest[indexGetQuest])
            {
                this.thisnpc.continueButton.enabled = true;

            }

            else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesNotQuest[indexNotQuest])
            {
                this.thisnpc.continueButton.enabled = true;
            }

            else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesFinishQuest[indexFinishQuest])
            {
                this.thisnpc.continueButton.enabled = true;
            }
            else if (this.thisnpc.TalkDisplay.text == this.thisnpc.BatholoQuest[indexBatholoQuest])
            {
                this.thisnpc.continueButton.enabled = true;
            }
        }
            foreach (Quest quest in QuestManager.Instance.activeQuests)
        {
            if(quest.goal.goalType == GoalType.BatholoQuest2 && !BatholoCompleted)
            {
                BatholoQuest2 = true;

            }
        }
        foreach (Quest quest in QuestManager.Instance.completedQuests)
        {
            if (quest.goal.goalType == GoalType.BatholoQuest2 && !BatholoCompleted)
            {
                BatholoQuest2 = false;

            }
        }
       
        
            if (chatting)
        {
            //continueButton.enabled = false;
            weapon.SetActive(false);
            float maxZoom = 6f;
            float minZoom = 2f;
            float smoothTime = 0.07f;
            float newZoom = cam.orthographicSize * Time.fixedDeltaTime;
            float velocity = 0f;
            newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, newZoom, ref velocity, smoothTime);
            Vector2 thisPosition = new Vector3(transform.position.x, transform.position.y - 0.65f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, thisPosition, Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.F) && thisnpc.playerinRange && !Freplacement)
        {
            uiToggle.isChatting = true;
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() => NextSentence());
            continueButton.enabled = false;
            Player_Name.enabled = false;
            NPC_Name.text = NPCname;

            notice.SetActive(false);
            PlayerImage.enabled = false;
            NPCImage.enabled = false;
            uiToggle.ChatToggle();
            Debug.Log("Press");
            //notice.SetActive(false);
            
            // continueButton.onClick.RemoveAllListeners();

            if (!BatholoQuest2)
            {
                foreach (Quest questA in QuestManager.Instance.activeQuests)
                {
                    if (questA.goal.goalType == GoalType.KillGoblin | questA.goal.goalType == GoalType.KillGoblin2)
                    {
                       Completed = false;
                        alreadyHaveQuest = true;
                    }
                }
                foreach (Quest questC in QuestManager.Instance.completedQuests)
                {
                    if (questC.goal.goalType == GoalType.KillGoblin | questC.goal.goalType == GoalType.KillGoblin2)
                    {
                        //thisnpc.quest.isActive = false;
                        Completed = true;
                        alreadyHaveQuest = true;
                    }
                }
                chatting = true;
                chatUI.SetActive(true);
                nameText.text = this.NPCname.ToString();
                if (!alreadyHaveQuest && !Completed)
                {

                    if (this.thisnpc.typing == false)
                    {
                        thisnpc.indexGetQuest = 0;
                        StartCoroutine(Type());
                        thisnpc.typing = true;
                    }

                }

                else if (alreadyHaveQuest && !Completed)
                {
                    if (this.thisnpc.typing == false)
                    {

                        thisnpc.indexNotQuest = 0;
                        StartCoroutine(Type());
                        thisnpc.typing = true;

                    }

                }
                else if (alreadyHaveQuest && Completed)
                {
                    Debug.Log("You !");
                    if (thisnpc.typing == false)
                    {
                        Debug.Log("You Failed!");
                        this.thisnpc.indexFinishQuest = 0;
                        StartCoroutine(Type());
                        this.thisnpc.typing = true;

                    }

                }
            }
            else
            {
                chatting = true;
                chatUI.SetActive(true);
                nameText.text = this.NPCname.ToString();
                 
                    if (thisnpc.typing == false)
                    {
                        Debug.Log("You Failed!");
                        this.thisnpc.indexBatholoQuest = 0;
                        StartCoroutine(Type());
                        this.thisnpc.typing = true;

                    }

                
            }
           Freplacement = true;
        }

    }
    public void AcceptQuestSera()
    {
       // thisnpc.player.quests.Add(thisnpc.quest);
        
        int random = Random.Range(0, 1);
        if(random == 0)
        {
            QuestManager.Instance.ActiveQuests(SeraQuest1);
        }
        else if(random == 1)
        {
            QuestManager.Instance.ActiveQuests(SeraQuest2);
        }
        alreadyHaveQuest = true;
        
            
        
        continueButton.gameObject.SetActive(true);
       // thisnpc.player.quest = thisnpc.quest;
        weapon.SetActive(true);
        RefuseButton.SetActive(false);
        AcceptButton.SetActive(false);
        uiToggle.IntoPlay();
        chatUI.SetActive(false);
        Freplacement = false;
        // foreach(Quest quest in thisnpc.player.quests)
        //   {
        //       thisnpc.player.quest = quest;
        //   }
        // thisnpc.player.quests.Add(quest);

    }
    public void RefuseQuestSera()
    {
        weapon.SetActive(true);
        uiToggle.IntoPlay();
        chatUI.SetActive(false);
        RefuseButton.SetActive(false);
        AcceptButton.SetActive(false);
        continueButton.gameObject.SetActive(true);
        Freplacement = false;
    }

}
    
