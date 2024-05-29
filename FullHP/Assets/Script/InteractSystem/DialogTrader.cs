using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogTrader : MonoBehaviour
{
    public GameObject weapon;
    public string NPCname;
    public TextMeshPro TalkDisplay;
    public TextMeshPro npcName;
    public bool dialogueIsPlaying { get; private set; }
    public string sentences;
    public string sentencesQuest;
    public float typingSpeed;
    public bool chatting;
    private bool playerinRange;
    public bool typing;
    public GameObject notice;
    private ToggleUI uiToggle;
    public GameObject ChatInWorld;
    //public GameObject questUI;
    private UI_Shop uiShop;
    public TraderType Merchant;
    public Scrollbar sliderItem;
    private Transform container;
    public Button QuitShopButton;

    public Camera cam;
    public bool BatholoQuest1;
    public bool BatholoCompleted;

    Player player;

    public QuestDetecter questDetecter;
    public enum TraderType
    {
        Gunsmith,
        ItemShop,
        Doctor
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerinRange = true;
            if (!BatholoQuest1 && !typing)
            {
                notice.SetActive(true);
                TalkDisplay.text = "";
                StartCoroutine(Type());
                typing = true;
            }
            else if(BatholoQuest1 && !typing)
            {
                TalkDisplay.text = "";
                StartCoroutine(TypeQuest());
                typing = true;
            }
            ChatInWorld.SetActive(true);
            Debug.Log("Chat");
            
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerinRange = false;
            TalkDisplay.text = "";
            notice.SetActive(false);
            ChatInWorld.SetActive(false);
            typing = false;
        }
    }
    void Start()
    {
        BatholoQuest1 = false;
        player = FindObjectOfType<Player>();
        chatting = false;
        uiShop = FindObjectOfType<UI_Shop>();
        uiToggle = FindObjectOfType<ToggleUI>();
        npcName.text = NPCname;
        Transform ScrollArea = uiShop.transform.Find("ScrollArea");
        Transform Viewport = ScrollArea.Find("Viewport");
        Transform Containers = Viewport.Find("Containers");
        container = Containers.Find("Container");
        questDetecter = FindObjectOfType<QuestDetecter>();
        //container = uiShop.transform.Find("Container");

    }

    // Update is called once per frame
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
            Debug.Log("This Error");
            Vector2 thisPosition = new Vector3(transform.position.x, transform.position.y - 0.2f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, thisPosition, Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.F) && playerinRange && !BatholoQuest1)
        {
            uiToggle.isChatting = true;
            QuitShopButton.onClick.AddListener(() => CleanShop());
            ChatInWorld.SetActive(false);
            // npcName.text = name;
            uiToggle.ShopToggle();
            sliderItem.value = 1;
            notice.SetActive(false);
            chatting = true;
            SetShop();
        }
        List<Quest> questing = new List<Quest>();

        if (QuestManager.Instance.activeQuests.Count > 0)
        {
            List<Quest> questsCopy = new List<Quest>(QuestManager.Instance.activeQuests);
            foreach (Quest quest in questsCopy)
            {

              
                    if (quest != null)
                    {
                        if (quest.goal.goalType == GoalType.BatholoQuest1 && !BatholoCompleted)
                        {
                            if (quest != null)
                            {
                                BatholoQuest1 = true;
                            }
                        }
                        if (TalkDisplay.text == sentencesQuest && BatholoQuest1 && quest.goal.goalType == GoalType.BatholoQuest1)
                        {
                            if (quest != null)
                            {
                                quest.goal.BatholoTrack1();
                            BatholoCompleted = true;
                            BatholoQuest1 = false;
                            questDetecter.GetArrowToDestroy(this.transform.position);
                                if (quest.goal.IsReached())
                                {
                                    QuestManager.Instance.activeQuests.Remove(quest);
                                    QuestManager.Instance.CompletedQuests(quest);
                                }
                           

                            }
                          
                        }
                    }
                
            }


        }
        else
        {
            return;
        }

        

    }
    IEnumerator TypeQuest()
    {


        foreach (char letter in this.sentencesQuest.ToCharArray())
        {
            TalkDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

        IEnumerator Type()
        {


            foreach (char letter in this.sentences.ToCharArray())
            {
                TalkDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }

    
  

    public void SetShop()
    {
        chatting = true;
        switch (Merchant)
        {
            default:
            case TraderType.Gunsmith:
                //uiShop.Show();
                uiShop.shopItemTemplate = uiShop.container.Find("ShopGunTemplate");
                uiShop.shopItemTemplate.gameObject.SetActive(true);
                uiShop.CreateItemShop("Pistol", 15, 2, Item.ItemType.Pistol, ItemAssets.Instance.PistolSprite, Item.GetCost(Item.ItemType.Pistol), 0);
                uiShop.CreateItemShop("Desert Eagle", 30, 1.5f, Item.ItemType.DesertEagle, ItemAssets.Instance.DesertEagleSprite, Item.GetCost(Item.ItemType.DesertEagle), 1);
                uiShop.CreateItemShop("Silent King", 15, 2, Item.ItemType.SilentKing, ItemAssets.Instance.SilentKingSprite, Item.GetCost(Item.ItemType.SilentKing), 2);
                uiShop.CreateItemShop("Pistol Ammo", 0, 0, Item.ItemType.PistolAmmo, ItemAssets.Instance.PistolAmmo, Item.GetCost(Item.ItemType.PistolAmmo), 3);
                uiShop.CreateItemShop("Classic One", 20, 0.5f, Item.ItemType.ClassicOne, ItemAssets.Instance.ClassicOneSprite, Item.GetCost(Item.ItemType.ClassicOne), 4);
                uiShop.CreateItemShop("Shotgun Ammo", 0, 0, Item.ItemType.ShotgunAmmo, ItemAssets.Instance.ShotgunAmmo, Item.GetCost(Item.ItemType.ShotgunAmmo), 5);
                uiShop.CreateItemShop("Firefly", 20, 8, Item.ItemType.Firefly, ItemAssets.Instance.FireFlySprite, Item.GetCost(Item.ItemType.Firefly), 6);
                uiShop.CreateItemShop("Automatic Ammo", 0, 0, Item.ItemType.AutomaticAmmo, ItemAssets.Instance.AutomaticAmmo, Item.GetCost(Item.ItemType.AutomaticAmmo), 7);
                uiShop.CreateItemShop("Dagon", 30, 0.5f, Item.ItemType.Dagon, ItemAssets.Instance.DagonSprite, Item.GetCost(Item.ItemType.Dagon), 8);
                uiShop.CreateItemShop("Watcher", 50, 0.3f, Item.ItemType.Watcher, ItemAssets.Instance.WatcherSprite, Item.GetCost(Item.ItemType.Watcher), 9);
                uiShop.CreateItemShop("Sniper Ammo", 0, 0, Item.ItemType.SniperAmmo, ItemAssets.Instance.SniperAmmo, Item.GetCost(Item.ItemType.SniperAmmo), 10);
                uiShop.CreateItemShop("Laser Beam", 10, 10f, Item.ItemType.LaserBeam, ItemAssets.Instance.LaserBeam, Item.GetCost(Item.ItemType.LaserBeam), 11);
                uiShop.CreateItemShop("Battery Ammo ", 0, 0, Item.ItemType.BatteryAmmo, ItemAssets.Instance.BatteryAmmo, Item.GetCost(Item.ItemType.BatteryAmmo), 12);
                uiShop.shopItemTemplate.gameObject.SetActive(false);
                break;
            case TraderType.Doctor:
                uiShop.shopItemTemplate = uiShop.container.Find("ShopMedicTemplate");
                uiShop.shopItemTemplate.gameObject.SetActive(true);
                uiShop.CreateItemShop("Large Potion", 0, 0, Item.ItemType.LargeHpPotion, ItemAssets.Instance.LargeHpPotion, Item.GetCost(Item.ItemType.LargeHpPotion), 0);
                uiShop.CreateItemShop("Low Potion", 0, 0, Item.ItemType.LowHpPotion, ItemAssets.Instance.LowHpPotion, Item.GetCost(Item.ItemType.LowHpPotion), 1);
                uiShop.shopItemTemplate.gameObject.SetActive(false);
                Debug.Log("Medic Here");
                break;

        }
    }
    public void CleanShop()
    {
        chatting = false;
        foreach(Transform shopItem in container)
        {
            if(shopItem.gameObject.name != "ShopGunTemplate" && shopItem.gameObject.name != "ShopMedicTemplate" && shopItem.gameObject.name != "ShopItemTemplate")
            {
                Destroy(shopItem.gameObject);
            }

        }
     
    }
    

  
}
