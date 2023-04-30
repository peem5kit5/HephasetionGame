using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogNPCquest : MonoBehaviour
{
    public GameObject weapon;
    public TextMeshProUGUI nameText;
    public string name;
    public Player player;

    public bool dialogueIsPlaying { get; private set; }
    public TextMeshProUGUI TalkDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool chatting = false;
    public bool playerinRange;
    public bool typing;
    public Transform givePos;
    public ToggleUI uiToggle;
    public GameObject notice;
    //public string Tag;
    public GameObject chatUI;
    public GameObject AcceptButton;
    public GameObject RefuseButton;

    //for Quest
    public Quest quest;
    public Sprite itemImageGive;
    public Item itemGive;
    public string[] sentencesQuestFail;
    private int indexQuestFail;

    public string[] sentencesQuestSucceed;
    private int indexQuestSucceed;

    public string[] sentencesFinish;
    private int indexFinish;

    public GameObject questUI;
    QuestManager questmanager;
    public bool completedquest;
  //  public int questLine;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI itemText;
    // public ItemAssets itemAssetNPC;

    // end

    //this for disabled gun and camera
    

    public GameObject continueButton;

    public DialogNPCquest thisnpc;

    private bool isChatting;
    private void Start()
    {
        this.thisnpc = this;
        questmanager = FindObjectOfType<QuestManager>();
        //completedquest = false;
        
        

    }
    private void Awake()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            notice.SetActive(true);
            gameObject.GetComponent<DialogNPCquest>().thisnpc.playerinRange = true;


            Debug.Log("Chat");


        }
       
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        thisnpc.playerinRange = false;
        notice.SetActive(false);

    }

    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        

        if (chatting)
        {
            weapon.SetActive(false);
        }
       
        

        if (Input.GetKeyDown(KeyCode.F) && thisnpc.playerinRange )
        {
            foreach (Quest questA in player.activeQuest)
            {
                if (questA.goal.goalType == thisnpc.quest.goal.goalType)
                {
                    thisnpc.quest.isActive = true;

                }
            }
            foreach (Quest questC in player.completedQuest)
            {
                if (questC.goal.goalType == thisnpc.quest.goal.goalType)
                {
                    thisnpc.quest.isActive = true;

                }
            }
            foreach (Quest questF in player.finishedQuest)
            {
                if (questF.goal.goalType == thisnpc.quest.goal.goalType)
                {
                    thisnpc.quest.isActive = false;
                    thisnpc.quest.isCompleted = true;

                }
            }
            //Time.timeScale = 0f;
            //uiToggle.Deactivated();
            Debug.Log("Press");
            //notice.SetActive(false);
            chatting = true;
                chatUI.SetActive(true);
            nameText.text = this.name.ToString();
                

            if (!gameObject.GetComponent<DialogNPCquest>().quest.isActive && !gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
                {

                    if (this.thisnpc.typing == false)
                    {
                        thisnpc.index = 0;
                        StartCoroutine(Type());
                        thisnpc.typing = true;
                    }

                }

                else if (gameObject.GetComponent<DialogNPCquest>().quest.isActive && gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
                {
                    if (this.thisnpc.typing == false)
                    {

                        thisnpc.indexQuestSucceed = 0;
                        StartCoroutine(Type());
                        thisnpc.typing = true;

                    }

                }
                else if (gameObject.GetComponent<DialogNPCquest>().quest.isActive && !gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
                {
                    Debug.Log("You !");
                    if (thisnpc.typing == false)
                    {
                        Debug.Log("You Failed!");
                        this.thisnpc.indexQuestFail = 0;
                        StartCoroutine(Type());
                        this.thisnpc.typing = true;

                    }

                }
                else if (gameObject.GetComponent<DialogNPCquest>().quest.isActive && !gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
                {
                    if (this.thisnpc.typing == false)
                    {

                        this.thisnpc.indexQuestFail = 0;
                        StartCoroutine(Type());
                        this.thisnpc.typing = true;

                    }

                }
                else if (!gameObject.GetComponent<DialogNPCquest>().quest.isActive && gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
                {
                    if (this.thisnpc.typing == false)
                    {
                        this.thisnpc.indexFinish = 0;
                        StartCoroutine(Type());
                        this.thisnpc.typing = true;
                    }
                }

            }
            //  else if (!playerinRange)
            //  {
            //      chat.SetActive(false);
            //      typing = false;
            //   }
            if (this.thisnpc.typing == true)
            {

                if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentences[index])
                {
                    this.thisnpc.continueButton.SetActive(true);
                }

                else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesQuestFail[indexQuestFail])
                {
                    this.thisnpc.continueButton.SetActive(true);
                }

                else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesQuestSucceed[indexQuestSucceed])
                {
                    this.thisnpc.continueButton.SetActive(true);
                }
                else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesFinish[indexFinish])
                {
                    this.thisnpc.continueButton.SetActive(true);
                }



            
        }
            

       

    }
    IEnumerator Type()
    {
       
          //  Debug.Log("Typing" + Tag);
            if (!gameObject.GetComponent<DialogNPCquest>().quest.isCompleted && !gameObject.GetComponent<DialogNPCquest>().quest.isActive)
            {
                foreach (char letter in this.thisnpc.sentences[index].ToCharArray())
                {
                    this.thisnpc.TalkDisplay.text += letter;
                    yield return new WaitForSeconds(thisnpc.typingSpeed);

                }
            }
            else if (gameObject.GetComponent<DialogNPCquest>().quest.isActive && gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
            {
                foreach (char letter in this.sentencesQuestSucceed[indexQuestSucceed].ToCharArray())
                {
                    this.thisnpc.TalkDisplay.text += letter;
                    yield return new WaitForSeconds(this.thisnpc.typingSpeed);

                }
            }
            else if (gameObject.GetComponent<DialogNPCquest>().quest.isActive && !gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
            {
                foreach (char letter in this.thisnpc.sentencesQuestFail[indexQuestFail].ToCharArray())
                {
                    this.thisnpc.TalkDisplay.text += letter;
                    yield return new WaitForSeconds(this.thisnpc.typingSpeed);

                }
            }
            else if (!gameObject.GetComponent<DialogNPCquest>().quest.isActive && gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
            {
                foreach (char letter in this.thisnpc.sentencesFinish[indexFinish].ToCharArray())
                {
                    this.thisnpc.TalkDisplay.text += letter;
                    yield return new WaitForSeconds(this.thisnpc.typingSpeed);

                }
            
            }
       


    }
    public void NextSentence()
    {
       
            this.thisnpc.continueButton.SetActive(false);
            if (!gameObject.GetComponent<DialogNPCquest>().quest.isCompleted && !gameObject.GetComponent<DialogNPCquest>().quest.isActive)
            {

                if (this.thisnpc.index < this.thisnpc.sentences.Length - 1)
                {
                    this.thisnpc.index++;
                    this.thisnpc.TalkDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    this.thisnpc.index = 0;
                    this.thisnpc.TalkDisplay.text = "";
                    this.thisnpc.chatting = false;
                    this.thisnpc.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                    OpenUIQuest();
                weapon.SetActive(true);
            }
            }
            else if (gameObject.GetComponent<DialogNPCquest>().quest.isActive && gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
            {
                if (this.thisnpc.indexQuestSucceed < this.thisnpc.sentencesQuestSucceed.Length - 1)
                {
                    this.thisnpc.indexQuestSucceed++;
                    this.thisnpc.TalkDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    this.thisnpc.indexQuestSucceed = 0;
                    this.thisnpc.TalkDisplay.text = "";
                    this.thisnpc.chatting = false;
                    this.thisnpc.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                    QuestManager.Instance.FinishQuests(thisnpc.quest);
                   // this.thisnpc.quest.Complete();
                    Give();
                weapon.SetActive(true);
            }
            }
            else if (gameObject.GetComponent<DialogNPCquest>().quest.isActive && !gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
            {
                if (this.thisnpc.indexQuestFail < this.thisnpc.sentencesQuestFail.Length - 1)
                {
                    this.thisnpc.indexQuestFail++;
                    this.thisnpc.TalkDisplay.text = "";
                    this.StartCoroutine(Type());
                }
                else
                {
                    this.thisnpc.indexQuestFail = 0;
                    this.thisnpc.TalkDisplay.text = "";
                    this.thisnpc.chatting = false;
                    this.thisnpc.typing = false;
                    this.chatUI.SetActive(false);
                    uiToggle.IntoPlay();
                weapon.SetActive(true);
            }
            }
            else if (!gameObject.GetComponent<DialogNPCquest>().quest.isActive && gameObject.GetComponent<DialogNPCquest>().quest.isCompleted)
            {

                if (this.thisnpc.indexFinish < this.thisnpc.sentencesFinish.Length - 1)
                {
                    this.thisnpc.indexQuestFail++;
                    this.thisnpc.TalkDisplay.text = "";

                    this.StartCoroutine(Type());
                }
                else
                {
                    this.thisnpc.indexFinish++;
                    this.thisnpc.TalkDisplay.text = "";
                    this.thisnpc.chatting = false;
                    this.thisnpc.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                weapon.SetActive(true);
            }

        }
        


    }
    public void OpenUIQuest()
    {
        RefuseButton.SetActive(true);
        AcceptButton.SetActive(true);
        thisnpc.questUI.SetActive(true);
        thisnpc.titleText.text = quest.title;
        thisnpc.descriptionText.text = quest.description;
       thisnpc.moneyText.text = quest.moneyReward.ToString();
        thisnpc.itemGive = quest.itemReward;
        weapon.SetActive(false);
        //thisnpc.itemImageGive = quest.itemImage.sprite;

       // itemAssetNPC = quest.itemAsset;
    }
    public void AcceptQuest()
    {
        RefuseButton.SetActive(false);
        AcceptButton.SetActive(false);
        thisnpc.questUI.SetActive(false);
        thisnpc.player.quests.Add(thisnpc.quest);
        foreach(Quest quest in player.quests)
        {
            if(quest == thisnpc.quest)
            {
                thisnpc.quest.isActive = true;
                QuestManager.Instance.ActiveQuests(thisnpc.quest);
            }
        }
        thisnpc.player.quest = thisnpc.quest;
        weapon.SetActive(true);
        // foreach(Quest quest in thisnpc.player.quests)
        //   {
        //       thisnpc.player.quest = quest;
        //   }
        // thisnpc.player.quests.Add(quest);

    }
    public void RefuseQuest()
    {
        weapon.SetActive(true);
        thisnpc.questUI.SetActive(false);
       // thisnpc.quest.isActive = true;
        //thisnpc.player.quests.Add(quest);

    }
    void CheckingQuest()
    {
        foreach(Quest questing in player.activeQuest)
        {
            if(questing == thisnpc.quest)
            {
                thisnpc.quest.isActive = true;
                thisnpc.quest.isCompleted = false;
            }
        }
        foreach (Quest questing in player.completedQuest)
        {
            if (questing == thisnpc.quest)
            {
                thisnpc.quest.isActive = true;
                thisnpc.quest.isCompleted = true;
            }
        }
        foreach (Quest questing in player.finishedQuest)
        {
            if (questing == thisnpc.quest)
            {
                thisnpc.quest.isActive = false;
                thisnpc.quest.isCompleted = true;
            }
        }
       

    }

    public void Give()
    {
        player.inventory.AddItem(itemGive);
        ItemWorld.SpawnItemWorld(givePos.position, itemGive);
    }
    
}
