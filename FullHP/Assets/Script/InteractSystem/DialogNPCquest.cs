using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogNPCquest : MonoBehaviour
{
    public GameObject weapon;
    public TextMeshProUGUI nameText;
    public string NPCname;
    public Player player;

    public bool dialogueIsPlaying { get; private set; }
    public TextMeshProUGUI TalkDisplay;
    public string[] sentences;
    private int index;
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
    public Quest quest;
    public string[] sentencesQuestFail;
    private int indexQuestFail;

    public string[] sentencesQuestSucceed;
    private int indexQuestSucceed;

    public string[] sentencesFinish;
    private int indexFinish;
    public bool completedquest;
  //  public int questLine;

    // public ItemAssets itemAssetNPC;
    public Image PlayerImage;
    public Image NPCImage;
    // end
    public Item Reward;
    public int moneyReward;
    //this for disabled gun and camera

    public TextMeshProUGUI NPC_Name;
    public TextMeshProUGUI Player_Name;
    public Button continueButton;

    public DialogNPCquest thisnpc;
    
    private bool isChatting;
    public Camera cam;
    bool completed;

    public GameObject chatInWorld;
    public string sentenceInWorld;
    public TextMeshPro textChatInWorld;
    public TextMeshPro nameInWorld;
    public GameObject FOV;
    public bool Active;
    public bool Completed;
    public enum QuestNPC
    {
        Mysterious_Girl,
        Annoyed_Elf,
        Scared_Goblin,
        Prideful_Elf,
        Bully
    }
    private void Start()
    {
        this.thisnpc = this;
        //completedquest = false;
        player = FindObjectOfType<Player>();
        uiToggle = FindObjectOfType<ToggleUI>();
        completed = false;
    }
    private void Awake()
    {
        
    }
    IEnumerator TypeChatinWorld()
    {

            foreach (char letter in this.sentenceInWorld.ToCharArray())
            {
                textChatInWorld.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            notice.SetActive(true);
            gameObject.GetComponent<DialogNPCquest>().thisnpc.playerinRange = true;
            chatInWorld.SetActive(true);
            nameInWorld.text = NPCname;
            StartCoroutine(TypeChatinWorld());
            Debug.Log("Chat");


        }
       
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        thisnpc.playerinRange = false;
        notice.SetActive(false);
        chatInWorld.SetActive(false);
        textChatInWorld.text = "";
        nameInWorld.text = "";
    }

    private void FixedUpdate()
    {
        
    }

   
    private void Update()
    {
        
        
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

        foreach (Quest questA in QuestManager.Instance.activeQuests)
        {
            if (questA.goal.goalType == GoalType.MysteriousGirlQuest)
            {
                Active = true;
                Completed = false;

            }
        }
        foreach (Quest questC in QuestManager.Instance.completedQuests)
        {
            if (questC.goal.goalType == GoalType.MysteriousGirlQuest)
            {
                Active = true;
                Completed = true;
            }
        }
        foreach(Quest questF in QuestManager.Instance.finishedQuests)
        {
            if (questF.goal.goalType == GoalType.MysteriousGirlQuest)
            {
                Active = false;
                Completed = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && thisnpc.playerinRange)
        {
            continueButton.gameObject.SetActive(true);
            chatInWorld.SetActive(false);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() => NextSentence());
            continueButton.enabled = false;
            Player_Name.enabled = false;
            NPC_Name.text = NPCname;
           
            notice.SetActive(false);
            PlayerImage.enabled = false;
            NPCImage.enabled = false;
            uiToggle.ChatToggle();
           // continueButton.onClick.RemoveAllListeners();
           
           
            //Time.timeScale = 0f;
            //uiToggle.Deactivated();
            Debug.Log("Press");
            //notice.SetActive(false);
            chatting = true;
            chatUI.SetActive(true);
            nameText.text = this.NPCname.ToString();
                

            if (!Active && !Completed)
                {

                    if (this.thisnpc.typing == false)
                    {
                        thisnpc.index = 0;
                        StartCoroutine(Type());
                        thisnpc.typing = true;
                    }

                }

                else if (Active && Completed)
                {
                    if (this.thisnpc.typing == false)
                    {

                        thisnpc.indexQuestSucceed = 0;
                        StartCoroutine(Type());
                        thisnpc.typing = true;

                    }

                }
                else if (Active && !Completed)
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
                else if (!Active && Completed)
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
                    this.thisnpc.continueButton.enabled = true;
                }

                else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesQuestFail[indexQuestFail])
                {
                    this.thisnpc.continueButton.enabled = true;
                }

                else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesQuestSucceed[indexQuestSucceed])
                {
                    this.thisnpc.continueButton.enabled = true;
                }
                else if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesFinish[indexFinish])
                {
                    this.thisnpc.continueButton.enabled = true;
                }
      
            }
            

       

    }
    IEnumerator Type()
    {
       
          //  Debug.Log("Typing" + Tag);
            if (!Active && !Completed)
            {
                foreach (char letter in this.thisnpc.sentences[index].ToCharArray())
                {
                    this.thisnpc.TalkDisplay.text += letter;
                    yield return new WaitForSeconds(thisnpc.typingSpeed);
                   
                }
            }
            else if (Active && Completed)
            {
                foreach (char letter in this.sentencesQuestSucceed[indexQuestSucceed].ToCharArray())
                {
                    this.thisnpc.TalkDisplay.text += letter;
                    yield return new WaitForSeconds(this.thisnpc.typingSpeed);

                }
            }
            else if (Active && !Completed)
            {
                foreach (char letter in this.thisnpc.sentencesQuestFail[indexQuestFail].ToCharArray())
                {
                    this.thisnpc.TalkDisplay.text += letter;
                    yield return new WaitForSeconds(this.thisnpc.typingSpeed);

                }
            }
            else if (!Active && Completed)
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
        
        this.thisnpc.continueButton.enabled = false;
        if (!Active && !Completed)
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
                AcceptButton.SetActive(true);
                RefuseButton.SetActive(true);
                TextMeshProUGUI AcceptBText = AcceptButton.GetComponent<TextMeshProUGUI>();
                AcceptBText.text = "Accept";
                TextMeshProUGUI RefuseBText = RefuseButton.GetComponent<TextMeshProUGUI>();
                RefuseBText.text = "Refuse";
                Button AcceptB = AcceptButton.GetComponent<Button>();
                AcceptB.onClick.RemoveAllListeners();
                AcceptB.onClick.AddListener(() => AcceptQuest());
                Button AcceptR = RefuseButton.GetComponent<Button>();
                AcceptR.onClick.RemoveAllListeners();
                AcceptR.onClick.AddListener(() => RefuseQuest());
                continueButton.gameObject.SetActive(false);
                }
            }
            else if (Active && Completed)
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
                List<Quest> questing = new List<Quest>();
                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {

                    if (quest.goal.goalType == GoalType.MysteriousGirlQuest)
                    {


                        questing.Add(quest);
                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
                foreach (Quest quest in questing)
                {
                    QuestManager.Instance.FinishQuestsStore(quest);
                }
            
    
                      ItemWorld.SpawnItemWorld(player.transform.position, Reward);
                       player.moneyCount += moneyReward;
                // this.thisnpc.quest.Complete();
                    // ItemWorld.SpawnItemWorld(player.gameObject.transform.position,itemGive);
                    weapon.SetActive(true);
                }
            }
            else if (Active && !Completed)
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
            else if (Active && !Completed)
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
   
    public void AcceptQuest()
    {
        RefuseButton.SetActive(false);
        AcceptButton.SetActive(false);
        thisnpc.player.quests.Add(thisnpc.quest);
      
                thisnpc.quest.isActive = true;
                QuestManager.Instance.ActiveQuests(thisnpc.quest);
      
        thisnpc.player.quest = thisnpc.quest;
        weapon.SetActive(true);
        this.chatUI.SetActive(false);
        continueButton.gameObject.SetActive(true);
        uiToggle.IntoPlay();
        // foreach(Quest quest in thisnpc.player.quests)
        //   {
        //       thisnpc.player.quest = quest;
        //   }
        // thisnpc.player.quests.Add(quest);

    }
    public void RefuseQuest()
    {
        weapon.SetActive(true);
        uiToggle.IntoPlay();
    }
   

    
    
}
