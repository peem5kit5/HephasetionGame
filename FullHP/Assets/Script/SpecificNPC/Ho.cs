using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ho : MonoBehaviour
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
    public bool typingsentence;
    public ToggleUI uiToggle;
    public GameObject notice;
    //public string Tag;
    public GameObject chatUI;
    public GameObject AcceptButton;
    public GameObject RefuseButton;
    //for Quest
    public string[] sentencesAsk;
    private int indexAsk;

    //  public int questLine;

    // public ItemAssets itemAssetNPC;
    public Image PlayerImage;
    public Image NPCImage;
    // end

    //this for disabled gun and camera

    public TextMeshProUGUI NPC_Name;
    public TextMeshProUGUI Player_Name;
    public Button continueButton;

    public Ho thisnpc;

    private bool isChatting;
    public Camera cam;

    public GameObject chatInWorld;
    public string sentenceInWorld;
    public TextMeshPro textChatInWorld;
    public TextMeshPro nameInWorld;
    public GameObject FOV;
    UI_Inventory uiInventory;

    public bool BatholoQuest1;
    public bool BatholoCompleted;
    public string sentencesBatholoQuest;

    public GameObject ChatInWorld;
    public TextMeshPro NameInWorld;

    public QuestDetecter questDetecter;


    // Start is called before the first frame update
    void Start()
    {
        BatholoQuest1 = false;
        this.thisnpc = this;
        //completedquest = false;
        player = FindObjectOfType<Player>();
        uiToggle = FindObjectOfType<ToggleUI>();
        uiInventory = FindObjectOfType<UI_Inventory>();
        questDetecter = FindObjectOfType<QuestDetecter>();
    }
    IEnumerator TypeChatinWorld()
    {

        if (BatholoQuest1)
        {
            foreach (char letter in this.sentencesBatholoQuest.ToCharArray())
            {
                textChatInWorld.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        else 
        {
            foreach (char letter in this.sentenceInWorld.ToCharArray())
            {
                textChatInWorld.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
       



    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !typing)
        {
            notice.SetActive(true);
            gameObject.GetComponent<Ho>().thisnpc.playerinRange = true;
            chatInWorld.SetActive(true);
            nameInWorld.text = NPCname;
            StartCoroutine(TypeChatinWorld());
            typing = true;
            Debug.Log("Chat");


        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        thisnpc.playerinRange = false;
        notice.SetActive(false);
        chatInWorld.SetActive(false);
        textChatInWorld.text = "";
        typing = false;
        nameInWorld.text = "";
    }


    // Update is called once per frame
    void Update()
    {
        
        
        if (QuestManager.Instance.activeQuests.Count > 0)
        {
            List<Quest> questsCopy = new List<Quest>(QuestManager.Instance.activeQuests);
            foreach (Quest quest in questsCopy)
            {


                if (quest.goal.goalType == GoalType.BatholoQuest1 && !BatholoCompleted)
                {
                    if (quest != null)
                    {
                        BatholoQuest1 = true;
                    }

                }
                if (textChatInWorld.text == sentencesBatholoQuest && BatholoQuest1 && quest.goal.goalType == GoalType.BatholoQuest1)
                {
                    if (quest != null)
                    {




                        if (quest.goal.goalType == GoalType.BatholoQuest1)
                        {
                            quest.goal.BatholoTrack1();
                            questDetecter.GetArrowToDestroy(this.transform.position);
             
                            if (quest.goal.IsReached())
                            {
                                QuestManager.Instance.activeQuests.Remove(quest);
                                QuestManager.Instance.CompletedQuests(quest);


                                //inv.AddItem(npcQuest.itemReward);
                            }
                        }


                        BatholoCompleted = true;
                        BatholoQuest1 = false;
                    }


                }
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
        if (Input.GetKeyDown(KeyCode.F) && playerinRange)
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
            if (this.thisnpc.typingsentence == false)
            {
                thisnpc.indexAsk = 0;
                StartCoroutine(Type());
                thisnpc.typingsentence = true;
            }
            chatting = true;
            chatUI.SetActive(true);
            nameText.text = this.NPCname.ToString();
        }
        if (this.thisnpc.typingsentence == true)
        {

            if (this.thisnpc.TalkDisplay.text == this.thisnpc.sentencesAsk[indexAsk])
            {
                this.thisnpc.continueButton.enabled = true;

            }


        }
        
    }
   

    IEnumerator Type()
    {

        foreach (char letter in this.thisnpc.sentencesAsk[indexAsk].ToCharArray())
        {
            this.thisnpc.TalkDisplay.text += letter;
            yield return new WaitForSeconds(thisnpc.typingSpeed);

        }
    }
    public void NextSentence()
    {
        if (this.thisnpc.indexAsk < this.thisnpc.sentencesAsk.Length - 1)
        {
            this.thisnpc.indexAsk++;
            this.thisnpc.TalkDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            this.thisnpc.indexAsk = 0;
            this.thisnpc.TalkDisplay.text = "";
            this.thisnpc.chatting = false;
            this.thisnpc.typingsentence = false;
            
            AcceptButton.SetActive(true);
            RefuseButton.SetActive(true);
            TextMeshProUGUI AcceptBText = AcceptButton.GetComponent<TextMeshProUGUI>();
            AcceptBText.text = "Storage";
            TextMeshProUGUI RefuseBText = RefuseButton.GetComponent<TextMeshProUGUI>();
            RefuseBText.text = "Save";
            Button AcceptB = AcceptButton.GetComponent<Button>();
            AcceptB.onClick.RemoveAllListeners();
            AcceptB.onClick.AddListener(() => Storage_OpenUI());
            Button AcceptR = RefuseButton.GetComponent<Button>();
            AcceptR.onClick.RemoveAllListeners();
            AcceptR.onClick.AddListener(() => SaveGame());
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() => uiToggle.IntoPlay());
        }
    }
    public void Storage_OpenUI()
    {
        uiToggle.StorageToggle();
        //uiInventory.storageUI = true;
    }
    public void SaveGame()
    {
        SaveManager.Instance.SaveAll();
        uiToggle.IntoPlay();
    }
}