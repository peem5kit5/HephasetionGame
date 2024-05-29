using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Batholo : MonoBehaviour
{
    public GameObject PlayerWeaponHolder;
    public TextMeshProUGUI nameText;
    string NPCname = "Batholo";
    public Player player;
    public NPCassets npcAsset;
    public bool dialogueIsPlaying { get; private set; }
    public TextMeshProUGUI TalkDisplay;


    public float typingSpeed;
    public bool chatting;
    public bool playerinRange;
    public bool typing;
    public ToggleUI uiToggle;
    public GameObject notice;
    //public string Tag;
    public GameObject chatUI;

    //for Quest
    public Sprite itemImageGive;
    public Item itemGive;
    public string[] sentencesQuest1;
    private int indexQuest1;
    

    public string[] sentencesQuest2;
    private int indexQuest2;

    public string[] sentencesQuest3;
    private int indexQuest3;

    public string[] sentencesQuest4;
    int indexQuest4;

    public string[] sentencesQuest5;
    int indexQuest5;

    public string[] sentencesQuest6;
    int indexQuest6;

    public string[] sentencesQuest7;
    int indexQuest7;

    public string[] sentencesQuest8;
    int indexQuest8;

    public string[] sentenceFinish;
    
    QuestManager questmanager;
    public string[] sentencesTalking1;
    int indexTalking1;
    public string[] sentencesTalking2;
    int indexTalking2;
    public string[] sentencesTalking3;
    int indexTalking3;
    public string[] sentencesTalking4;
    int indexTalking4;


    public Item[] Q2reward;
    //  public int questLine;


    // public ItemAssets itemAssetNPC;

    // end

    //this for disabled gun and camera


    public Button continueButton;

    Batholo batholo;

    public Quest BatholoQuest1;
    public Quest BatholoQuest2;
    public Quest BatholoQuest3;
    public Quest BatholoQuest4;
    public Quest BatholoQuest5;
    public Quest BatholoQuest6;
    public Quest BatholoQuest7;
    public Quest BatholoQuest8;

    public int[] batholoImageIndexQ1;
    public int[] batholoImageIndexQ2;
    public int[] batholoImageIndexQ3;
    public int[] batholoImageIndexQ4;
    public int[] batholoImageIndexQ5;
    public int[] batholoImageIndexQ6;
    public int[] batholoImageIndexQ7;
    public int[] batholoImageIndexQ8;
    public int[] batholoImageIndexT1;
    public int[] batholoImageIndexT2;
    public int[] batholoImageIndexT3;
    public int[] batholoImageIndexT4;

    public int[] playerImageIndexQ1;
    public int[] playerImageIndexQ2;
    public int[] playerImageIndexQ3;
    public int[] playerImageIndexQ4;
    public int[] playerImageIndexQ5;
    public int[] playerImageIndexQ6;
    public int[] playerImageIndexQ7;
    public int[] playerImageIndexQ8;
    public int[] playerImageIndexT1;
    public int[] playerImageIndexT2;
    public int[] playerImageIndexT3;
    public int[] playerImageIndexT4;

    public Image Player_Image;
    public Image Batholo_Image;

    public bool Quest1Current;
    public bool Quest2Current;
    public bool Quest3Current;
    public bool Quest4Current;
    public bool Quest5Current;
    public bool Quest6Current;
    public bool Quest7Current;
    public bool Quest8Current;

    

    public bool Quest1Completed;
    public  bool Quest2Completed;
    public  bool Quest3Completed;
    public  bool Quest4Completed;
    public bool Quest5Completed;
    public bool Quest6Completed;
    public bool Quest7Completed;
    public bool Quest8Completed;

    public TextMeshProUGUI Batholo_Name;
    public TextMeshProUGUI Player_Name;

    public Camera cam;

    public bool PigmanDefeated;
    public bool EagleDefeated;
    int selectionTalking;
    public bool Talking;
    QuestDetecter questDetecter;

   public bool quest1passing;
    // Start is called before the first frame update
    void Start()
    {
        chatting = false;
        batholo = GetComponent<Batholo>();
        questmanager = FindObjectOfType<QuestManager>();
        player = FindObjectOfType<Player>();
        questDetecter = FindObjectOfType<QuestDetecter>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TalkDisplay.text = "";
            notice.SetActive(true);
            gameObject.GetComponent<Batholo>().batholo.playerinRange = true;
            questDetecter.GetArrowToDestroy(this.transform.position);
            
            Debug.Log("Chat");
        }

    }

    public void BatholoTalk()
    {
        Color color = new Color(0.2f, 0.2f, 0.2f, 255 / 255);
        Color colorNew = new Color(255 / 255, 255 / 255, 255 / 255, 255 / 255);
        Player_Image.color = color;
        Batholo_Image.color = colorNew;
        Batholo_Name.color = colorNew;
        Player_Name.color = color;
    }
    public void PlayerTalk()
    {

        Color color = new Color(0.2f, 0.2f, 0.2f, 255/255);
        Color colorNew = new Color(255/255, 255/255, 255/255,255/255);
        Player_Image.color = colorNew;
        Batholo_Image.color = color;
        Batholo_Name.color = color;
        Player_Name.color = colorNew;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
              TalkDisplay.text = "";
        batholo.playerinRange = false;
        notice.SetActive(false);
        }
      
    }
 

    // Update is called once per frame
    void CheckImage()
    {
        if (!Talking)
        {
            foreach (int i in batholoImageIndexQ1)
            {
                if (i == indexQuest1 && !Quest1Current && !Quest1Completed && !quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo1");
                   
                }
            }
            foreach (int i in batholoImageIndexQ2)
            {
                if (i == indexQuest2 && !Quest1Current && Quest1Completed && quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo2");
                }
            }
            foreach (int i in batholoImageIndexQ3)
            {
                if (i == indexQuest3 && !Quest2Current && Quest2Completed && quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo3");
                }
            }
            foreach (int i in batholoImageIndexQ4)
            {
                if (i == indexQuest4 && !Quest3Current && Quest3Completed && quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo4");
                }
            }
            foreach (int i in batholoImageIndexQ5)
            {
                if (i == indexQuest5 && !Quest4Current && Quest4Completed && quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo5");
                }
            }
            foreach (int i in batholoImageIndexQ6)
            {
                if (i == indexQuest6 && !Quest5Current && Quest5Completed && quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo6");
                }
            }
            foreach (int i in batholoImageIndexQ7)
            {
                if (i == indexQuest7 && !Quest6Current && Quest6Completed && quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo7");
                }
            }
            foreach (int i in batholoImageIndexQ8)
            {
                if (i == indexQuest8 && !Quest7Current && Quest7Completed && quest1passing)
                {
                    BatholoTalk();
                    Debug.Log("Batholo8");
                }
            }
            foreach (int i in playerImageIndexQ1)
            {
                if (i == indexQuest1 && !Quest1Current && !Quest1Completed && !quest1passing)
                {
                    PlayerTalk();
                
                }
            }
            foreach (int i in playerImageIndexQ2)
            {
                if (i == indexQuest2 && !Quest1Current && Quest1Completed && quest1passing)
                {
                    PlayerTalk();
                
                }
            }
            foreach (int i in playerImageIndexQ3)
            {
                if (i == indexQuest3 && !Quest2Current && Quest2Completed && quest1passing)
                {
                    PlayerTalk();
             
                }

            }
            foreach (int i in playerImageIndexQ4)
            {
                if (i == indexQuest4 && !Quest3Current && Quest3Completed && quest1passing)
                {
                    PlayerTalk();
                    Debug.Log("Player4");
                }
            }
            foreach (int i in playerImageIndexQ5)
            {
                if (i == indexQuest5 && !Quest4Current && Quest4Completed && quest1passing)
                {
                    PlayerTalk();
                    Debug.Log("Player5");
                }
            }
            foreach (int i in playerImageIndexQ6)
            {
                if (i == indexQuest6 && !Quest5Current && Quest5Completed && quest1passing)
                {
                    PlayerTalk();
                    Debug.Log("Player6");
                }
            }
            foreach (int i in playerImageIndexQ7)
            {
                if (i == indexQuest7 && !Quest6Current && Quest6Completed && quest1passing)
                {
                    PlayerTalk();
                    Debug.Log("Player7");
                }
            }
            foreach (int i in playerImageIndexQ8)
            {
                if (i == indexQuest8 && !Quest7Current && Quest7Completed && quest1passing)
                {
                    PlayerTalk();
                    Debug.Log("Player8");
                }
            }
        }
        else
        {
            foreach (int i in batholoImageIndexT1)
            {
                if (i == indexTalking1 && Talking)
                {
                    BatholoTalk();
                }
            }
            foreach (int i in batholoImageIndexT2)
            {
                if (i == indexTalking2 && Talking)
                {
                    BatholoTalk();
                }
            }
            foreach (int i in batholoImageIndexT3)
            {
                if (i == indexTalking3 && Talking)
                {
                    BatholoTalk();
                }
            }
            foreach (int i in batholoImageIndexT4)
            {
                if (i == indexTalking4 && Talking)
                {
                    BatholoTalk();
                }
            }

            foreach (int i in playerImageIndexT1)
            {
                if (i == indexTalking1 && Talking)
                {
                    PlayerTalk();
                }
            }
            foreach (int i in playerImageIndexT2)
            {
                if (i == indexTalking2 && Talking)
                {
                    PlayerTalk();
                }
            }
            foreach (int i in playerImageIndexT3)
            {
                if (i == indexTalking3 && Talking)
                {
                    PlayerTalk();
                }
            }
            foreach (int i in playerImageIndexT4)
            {
                if (i == indexTalking4 && Talking)
                {
                    PlayerTalk();
                }
            }
        }
       
        
    }
    void Update()
    {


   
        if (chatting)
        {
            float maxZoom = 6f;
            float minZoom = 2f;
            float smoothTime = 0.07f;
            float newZoom = cam.orthographicSize * Time.fixedDeltaTime;
            float velocity = 0f;
            newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, newZoom, ref velocity, smoothTime);
            Vector2 thisPosition = new Vector3(transform.position.x, transform.position.y - 0.3f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, thisPosition, Time.deltaTime);
            PlayerWeaponHolder.SetActive(false);
            CheckImage();
        }

        foreach (Quest quest in QuestManager.Instance.activeQuests)
        {
            if (quest.goal.goalType == GoalType.BatholoQuest1)
            {
                Quest1Current = true;
                Talking = true;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest2)
            {
                quest1passing = true;
                Quest2Current = true;
                Talking = true;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest3)
            {
                quest1passing = true;
                Quest3Current = true;
                Talking = true;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest4)
            {
                quest1passing = true;
                Quest4Current = true;
                Talking = true;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest5)
            {
                quest1passing = true;
                Quest5Current = true;
                Talking = true;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest6)
            {
                quest1passing = true;
                Quest6Current = true;
                Talking = true;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest7)
            {
                quest1passing = true;
                Quest7Current = true;
                Talking = true;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest8)
            {
                quest1passing = true;
                Quest8Current = true;
                Talking = true;
            }
        }
        foreach (Quest quest in QuestManager.Instance.completedQuests)
        {
            if (quest.goal.goalType == GoalType.BatholoQuest1)
            {
                quest1passing = true;
                Quest1Current = false;
                Quest1Completed = true;
                Talking = false;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest2)
            {
                quest1passing = true;
                Quest2Current = false;
                Quest2Completed = true;
                Talking = false;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest3)
            {
                quest1passing = true;
                Quest3Current = false;
                Quest3Completed = true;
                Talking = false;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest4)
            {
                quest1passing = true;
                Quest4Current = false;
                Quest4Completed = true;
               Talking = false;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest5)
            {
                quest1passing = true;
                Quest5Current = false;
                Quest5Completed = true;
                Talking = false;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest6)
            {
                quest1passing = true;
                Quest6Current = false;
                Quest6Completed = true;
                Talking = false;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest7)
            {
                quest1passing = true;
                Quest7Current = false;
                Quest7Completed = true;
                Talking = false;
            }
            else if (quest.goal.goalType == GoalType.BatholoQuest8)
            {
                quest1passing = true;
                Quest8Current = false;
                Quest8Completed = true;
                Talking = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.F) && batholo.playerinRange && !Talking && !chatting)
        {
            uiToggle.isChatting = true;
            notice.SetActive(false);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() => NextSentence());
            continueButton.enabled = false;
            Batholo_Name.text = NPCname;
            Player_Name.enabled = true;
            Player_Image.enabled = true;
            Batholo_Image.enabled = true;
            //   continueButton.onClick.RemoveAllListeners();
            //  Cursor.visible = true;
            uiToggle.ChatToggle();
            Batholo_Image.sprite = npcAsset.Batholo;


            chatting = true;
            chatUI.SetActive(true);
            nameText.text = this.NPCname.ToString();

            if (!Quest1Current && !Quest1Completed && !quest1passing)
            {
                if (batholo.typing == false)
                {
                 
                    batholo.indexQuest1 = 0;
                    StartCoroutine(Type());
                    batholo.typing = true;
                }
            }
            else if (!Quest1Current && Quest1Completed && quest1passing)
            {
                if (batholo.typing == false)
                {
                    batholo.indexQuest2 = 0;
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest1)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                    batholo.typing = true;
                }
            }
            else if (!Quest2Current && Quest2Completed && quest1passing)
            {
                if (batholo.typing == false)
                {
                    batholo.indexQuest3 = 0;
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest2)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                      foreach(Item item in Q2reward)
                        {
                            ItemWorld.SpawnItemWorld(player.transform.position, item);
                        }
                       
                    }
                    batholo.typing = true;
                }
            }
            else if (!Quest3Current && Quest3Completed && quest1passing)
            {
                if (batholo.typing == false)
                {

                    batholo.indexQuest4 = 0;
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest3)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                    batholo.typing = true;
                }
            }
            else if (!Quest4Current && Quest4Completed && quest1passing)
            {
                if (batholo.typing == false)
                {
                    batholo.indexQuest5 = 0;
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest4)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                    batholo.typing = true;
                }
            }
            else if (!Quest5Current && Quest5Completed && quest1passing)
            {
                if (batholo.typing == false)
                {
                    batholo.indexQuest6 = 0;
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest5)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                    batholo.typing = true;
                }
            }
            else if (!Quest6Current && Quest6Completed && quest1passing)
            {
                if (batholo.typing == false)
                {
                    batholo.indexQuest7 = 0;
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest6)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                    batholo.typing = true;
                }
            }

            else if (!Quest7Current && Quest7Completed && quest1passing)
            {
                if (batholo.typing == false)
                {
                    batholo.indexQuest8 = 0;
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest7)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                    batholo.typing = true;
                }
            }
            
    
        }
        if (!Quest8Current && Quest8Completed && quest1passing)
        {
               List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest8)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }

        }
        else if (Input.GetKeyDown(KeyCode.F) && batholo.playerinRange && Talking && !chatting)
        {
            uiToggle.isChatting = true;
            int i = Random.Range(0, 4);
            Debug.Log(i);
            notice.SetActive(false);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() => NextSentence());
            continueButton.enabled = false;
            Batholo_Name.text = NPCname;
            Player_Name.enabled = true;
            Player_Image.enabled = true;
            Batholo_Image.enabled = true;
            //   continueButton.onClick.RemoveAllListeners();
            //  Cursor.visible = true;
            uiToggle.ChatToggle();
            Batholo_Image.sprite = npcAsset.Batholo;


            chatting = true;
            chatUI.SetActive(true);
            nameText.text = this.NPCname.ToString();

            selectionTalking = i;
            if (selectionTalking == 0)
            {
                if (batholo.typing == false)
                {
                    batholo.indexTalking1 = 0;
                    StartCoroutine(TypeTalking(i));
                    batholo.typing = true;
                }
            }
            else if (selectionTalking == 1)
            {
                if (batholo.typing == false)
                {
                    batholo.indexTalking2 = 0;
                    StartCoroutine(TypeTalking(i));
                    batholo.typing = true;
                }
            }
            else if (selectionTalking == 2)
            {
                if (batholo.typing == false)
                {
                    batholo.indexTalking3 = 0;
                    StartCoroutine(TypeTalking(i));
                    batholo.typing = true;
                }
            }
            else if (selectionTalking == 3)
            {
                if (batholo.typing == false)
                {
                    batholo.indexTalking3 = 0;
                    StartCoroutine(TypeTalking(i));
                    batholo.typing = true;
                }
            }
          

        }
       
        if (this.batholo.typing == true)
        {

            if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest1[indexQuest1])
            {
                this.batholo.continueButton.enabled = true;
              
            }

            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest2[indexQuest2])
            {
                this.batholo.continueButton.enabled = true;
          
            }

            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest3[indexQuest3])
            {
                this.batholo.continueButton.enabled = true;
         
            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest4[indexQuest4])
            {
                this.batholo.continueButton.enabled = true;
             
            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest5[indexQuest5])
            {
                this.batholo.continueButton.enabled = true;

            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest6[indexQuest6])
            {
                this.batholo.continueButton.enabled = true;

            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest7[indexQuest7])
            {
                this.batholo.continueButton.enabled = true;

            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesQuest8[indexQuest8])
            {
                this.batholo.continueButton.enabled = true;

            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesTalking1[indexTalking1])
            {
                this.batholo.continueButton.enabled = true;

            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesTalking2[indexTalking2])
            {
                this.batholo.continueButton.enabled = true;

            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesTalking3[indexTalking3])
            {
                this.batholo.continueButton.enabled = true;

            }
            else if (this.batholo.TalkDisplay.text == this.batholo.sentencesTalking4[indexTalking4])
            {
                this.batholo.continueButton.enabled = true;

            }


        }
    }
    IEnumerator TypeTalking(int i)
    {
        if (i == 0)
        {
            foreach (char letter in batholo.sentencesTalking1[indexTalking1].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (i == 1)
        {
            foreach (char letter in batholo.sentencesTalking2[indexTalking2].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (i == 2)
        {
            foreach (char letter in batholo.sentencesTalking3[indexTalking3].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (i == 3)
        {
            foreach (char letter in batholo.sentencesTalking4[indexTalking4].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
      
    }
    IEnumerator Type()
    {
        if (!Quest1Current && !Quest1Completed && !quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest1[indexQuest1].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (!Quest1Current && Quest1Completed && quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest2[indexQuest2].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (!Quest2Current && Quest2Completed && quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest3[indexQuest3].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (!Quest3Current && Quest3Completed && quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest4[indexQuest4].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (!Quest4Current && Quest4Completed && quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest5[indexQuest5].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (!Quest5Current && Quest5Completed && quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest6[indexQuest6].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (!Quest6Current && Quest6Completed && quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest7[indexQuest7].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
        else if (!Quest7Current && Quest7Completed && quest1passing)
        {
            foreach (char letter in batholo.sentencesQuest8[indexQuest8].ToCharArray())
            {
                batholo.TalkDisplay.text += letter;
                yield return new WaitForSeconds(batholo.typingSpeed);

            }
        }
    }
    public void NextSentence()
    {
        if (!Talking)
        {
            this.batholo.continueButton.enabled = false;
            if (!Quest1Current && !Quest1Completed && !quest1passing)
            {

                if (this.batholo.indexQuest1 < this.batholo.sentencesQuest1.Length - 1)
                {
                    this.batholo.indexQuest1++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    this.batholo.indexQuest1 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                    questmanager.ActiveQuests(BatholoQuest1);
                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (!Quest1Current && Quest1Completed && quest1passing)
            {

                if (this.batholo.indexQuest2 < this.batholo.sentencesQuest2.Length - 1)
                {
                    this.batholo.indexQuest2++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    this.batholo.indexQuest2 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                    PlayerWeaponHolder.SetActive(true);
                   
                   // questmanager.FinishQuests(BatholoQuest1);
                    player.moneyCount += BatholoQuest1.moneyReward;
                    questmanager.ActiveQuests(BatholoQuest2);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (!Quest2Current && Quest2Completed && quest1passing)
            {

                if (this.batholo.indexQuest3 < this.batholo.sentencesQuest3.Length - 1)
                {
                    this.batholo.indexQuest3++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    this.batholo.indexQuest3 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                    PlayerWeaponHolder.SetActive(true);
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest2)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                    player.moneyCount += BatholoQuest2.moneyReward;
                    questmanager.ActiveQuests(BatholoQuest3);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (!Quest3Current && Quest3Completed && quest1passing)
            {

                if (this.batholo.indexQuest4 < this.batholo.sentencesQuest4.Length - 1)
                {
                    this.batholo.indexQuest4++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest3)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                }
                else
                {
                    this.batholo.indexQuest4 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                    PlayerWeaponHolder.SetActive(true);
                    uiToggle.isChatting = false;


                    player.moneyCount += BatholoQuest3.moneyReward;
                  //  GameManager.Instance.Pigman = true;
                    questmanager.ActiveQuests(BatholoQuest4);
                    Cursor.visible = false;
        
                }
            }
            else if (!Quest4Current && Quest4Completed && quest1passing)
            {

                if (this.batholo.indexQuest5 < this.batholo.sentencesQuest5.Length - 1)
                {
                    this.batholo.indexQuest5++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest4)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }


                }
                else
                {
                    this.batholo.indexQuest5 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                   
                    player.moneyCount += BatholoQuest4.moneyReward;
                    questmanager.ActiveQuests(BatholoQuest5);
                    this.uiToggle.IntoPlay();
                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;


                }
            }
            else if (!Quest5Current && Quest5Completed && quest1passing)
            {

                if (this.batholo.indexQuest6 < this.batholo.sentencesQuest6.Length - 1)
                {
                    this.batholo.indexQuest6++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest5)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                }
                else
                {
                    this.batholo.indexQuest6 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                  
                    player.moneyCount += BatholoQuest5.moneyReward;
                    questmanager.ActiveQuests(BatholoQuest6);
                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (!Quest6Current && Quest6Completed && quest1passing)
            {

                if (this.batholo.indexQuest7 < this.batholo.sentencesQuest7.Length - 1)
                {
                    this.batholo.indexQuest7++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest6)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                }
                else
                {
                    this.batholo.indexQuest7 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                   
                    player.moneyCount += BatholoQuest6.moneyReward;
                    questmanager.ActiveQuests(BatholoQuest7);
                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (!Quest7Current && Quest7Completed && quest1passing)
            {

                if (this.batholo.indexQuest8 < this.batholo.sentencesQuest8.Length - 1)
                {
                    this.batholo.indexQuest8++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(Type());
                    List<Quest> questing = new List<Quest>();

                    foreach (Quest quest in QuestManager.Instance.completedQuests)
                    {

                        if (quest.goal.goalType == GoalType.BatholoQuest7)
                        {


                            questing.Add(quest);
                            //inv.AddItem(npcQuest.itemReward);

                        }
                    }
                    foreach (Quest quest in questing)
                    {
                        QuestManager.Instance.FinishQuests(quest);
                    }
                }
                else
                {
                    this.batholo.indexQuest8 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();
                 
                    player.moneyCount += BatholoQuest7.moneyReward;
                    questmanager.ActiveQuests(BatholoQuest8);
                    GameManager.Instance.Eagle = true;
                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (!Quest8Current && Quest8Completed && quest1passing)
            {
                List<Quest> questing = new List<Quest>();

                foreach (Quest quest in QuestManager.Instance.completedQuests)
                {

                    if (quest.goal.goalType == GoalType.BatholoQuest8)
                    {


                        questing.Add(quest);
                        //inv.AddItem(npcQuest.itemReward);

                    }
                }
                foreach (Quest quest in questing)
                {
                    QuestManager.Instance.FinishQuests(quest);
                }
               
                player.moneyCount += BatholoQuest8.moneyReward;
                uiToggle.isChatting = false;

            }
        }
        else
        {
            if(selectionTalking == 0)
            {
                this.batholo.continueButton.enabled = false;
                if (this.batholo.indexTalking1 < this.batholo.sentencesTalking1.Length - 1)
                {
                    this.batholo.indexTalking1++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(TypeTalking(selectionTalking));

                }
                else
                {
                    this.batholo.indexTalking1 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();

                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (selectionTalking == 1)
            {
                this.batholo.continueButton.enabled = false;
                if (this.batholo.indexTalking2 < this.batholo.sentencesTalking2.Length - 1)
                {
                    this.batholo.indexTalking2++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(TypeTalking(selectionTalking));
                }
                else
                {
                    this.batholo.indexTalking2 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();

                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (selectionTalking == 2)
            {
                this.batholo.continueButton.enabled = false;
                if (this.batholo.indexTalking3 < this.batholo.sentencesTalking3.Length - 1)
                {
                    this.batholo.indexTalking3++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(TypeTalking(selectionTalking));
                }
                else
                {
                    this.batholo.indexTalking3 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();

                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;

                }
            }
            else if (selectionTalking == 3)
            {
                this.batholo.continueButton.enabled = false;
                if (this.batholo.indexTalking4 < this.batholo.sentencesTalking4.Length - 1)
                {
                    this.batholo.indexTalking4++;
                    this.batholo.TalkDisplay.text = "";
                    StartCoroutine(TypeTalking(selectionTalking));
                }
                else
                {
                    this.batholo.indexTalking4 = 0;
                    this.batholo.TalkDisplay.text = "";
                    this.batholo.chatting = false;
                    this.batholo.typing = false;
                    this.chatUI.SetActive(false);
                    this.uiToggle.IntoPlay();

                    PlayerWeaponHolder.SetActive(true);
                    Cursor.visible = false;
                    uiToggle.isChatting = false;
                }
            }

        }
       
    }
}
