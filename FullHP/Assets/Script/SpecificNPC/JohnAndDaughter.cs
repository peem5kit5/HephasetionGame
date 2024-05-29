using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
using UnityEngine.UI;
public class JohnAndDaughter : MonoBehaviour
{
    public GameObject weapon;
    public string NPCname;
    public TextMeshPro TalkDisplay;
    public TextMeshPro npcName;
    public bool dialogueIsPlaying { get; private set; }
    public string sentences;

    public float typingSpeed;
    public bool chatting;
    private bool playerinRange;
    public bool typing;
    public GameObject notice;
    private ToggleUI uiToggle;
    public GameObject ChatInWorld;
    //public GameObject questUI;
    private UI_Shop uiShop;

    private Transform container;
    public Button QuitShopButton;

    public Camera cam;

    public Transform[] startPos;
    public Transform[] finalDestination;


    public Transform johnPos;
    public Transform daughterPos;
    AIPath path;
    public int posNum;
    Player player;

    bool followingDad;
    Rigidbody2D rb;
    Animator anim;
    
    
    public enum JohnOrDaughter
    {
        John,
        Daughter
    }
    public JohnOrDaughter whichone;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerinRange)
        {
           
                notice.SetActive(true);
            
            ChatInWorld.SetActive(true);
            Debug.Log("Chat");
            StartCoroutine(Type());
            playerinRange = true;
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
        
        rb = GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();
        player = FindObjectOfType<Player>();
        chatting = false;
        uiShop = FindObjectOfType<UI_Shop>();
        uiToggle = FindObjectOfType<ToggleUI>();
        npcName.text = NPCname;
        Transform ScrollArea = uiShop.transform.Find("ScrollArea");
        Transform Viewport = ScrollArea.Find("Viewport");
        Transform Containers = Viewport.Find("Containers");
        container = Containers.Find("Container");
        anim = GetComponentInChildren<Animator>();
        WanderStart();
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
        if (Input.GetKeyDown(KeyCode.F) && playerinRange)
        {
            SetShop();
        }
        WanderAfterStart();
        AnimController();


    }

    IEnumerator Type()
    {
      
            foreach (char letter in this.sentences.ToCharArray())
            {
                TalkDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        


    }
    public int GetPosNum(int num)
    {
        posNum = num;
        return posNum;
    }
    public void WanderStart()
    {
        switch (whichone)
        {
            default:
            case JohnOrDaughter.John:
                int randomPos = Random.Range(0, startPos.Length);
                int randomPatrolPos = Random.Range(0, finalDestination.Length);
                transform.position = startPos[randomPos].position;
                posNum = GetPosNum(randomPatrolPos);
                path.destination = finalDestination[randomPatrolPos].position;
                break;
            case JohnOrDaughter.Daughter:
                transform.position = johnPos.position;
                
                break;
        }
    }

    public void AnimController()
    {
        if(path.velocity.magnitude > 0.01f)
        {
            if (!followingDad)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (path.velocity.x > 0.1f)
        {
            anim.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void WanderAfterStart()
    {
        switch (whichone)
        {
            default:
            case JohnOrDaughter.John:
                if (!playerinRange)
                {
                    path.destination = finalDestination[posNum].position;
                    path.canMove = true;
                    if (path.reachedEndOfPath)
                    {
                        int randomPatrolPos = Random.Range(0, finalDestination.Length);
                        posNum = GetPosNum(randomPatrolPos);
                        path.destination = finalDestination[posNum].position * Random.insideUnitCircle * 2;
                    }
                    
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    path.canMove = false;
                }
                break;
            case JohnOrDaughter.Daughter:
               
                path.destination = johnPos.position;
                if (path.reachedEndOfPath)
                {
                    
                    StartCoroutine(waitingForDad());
                }
               

                break;
        }
    }

    IEnumerator waitingForDad()
    {
        yield return new WaitForSeconds(1.5f);
        followingDad = true;
        rb.velocity = Vector3.zero;
        path.canMove = false;
        
        yield return new WaitForSeconds(2f);
        path.canMove = true;
        followingDad = false;
    }
    public void SetShop()
    {
        
        switch (whichone)
        {
            default:
            case JohnOrDaughter.John:
                QuitShopButton.onClick.AddListener(() => CleanShop());
                ChatInWorld.SetActive(false);
                // npcName.text = name;
                uiToggle.ShopToggle();
                chatting = true;
                //uiShop.Show();
                uiShop.shopItemTemplate = uiShop.container.Find("ShopGunTemplate");
                uiShop.shopItemTemplate.gameObject.SetActive(true);
                uiShop.CreateItemShop("Grenade Launcher", 150, 1, Item.ItemType.GrenadeLauncher, ItemAssets.Instance.GrenadeLauncherSprite, Item.GetCost(Item.ItemType.GrenadeLauncher), 0);
                uiShop.CreateItemShop("Grenade Ammo", 0, 0, Item.ItemType.GrenadeAmmo, ItemAssets.Instance.GrenadeAmmo, Item.GetCost(Item.ItemType.GrenadeAmmo), 1);
                uiShop.CreateItemShop("Flame Thrower", 10, 20f, Item.ItemType.FlameThrower, ItemAssets.Instance.FlameThrowerSprite, Item.GetCost(Item.ItemType.FlameThrower), 2);
                uiShop.CreateItemShop("Eliminator", 10, 8, Item.ItemType.Eliminator, ItemAssets.Instance.EliminatorSprite, Item.GetCost(Item.ItemType.Eliminator), 3);
                uiShop.CreateItemShop("Laser Charge", 50, 0, Item.ItemType.LaserCharge, ItemAssets.Instance.LaserChargeSprite, Item.GetCost(Item.ItemType.LaserCharge), 4);
                uiShop.CreateItemShop("Heat Laser", 0, 0, Item.ItemType.HeatLaser, ItemAssets.Instance.HeatLaserSprite, Item.GetCost(Item.ItemType.HeatLaser), 5);
                uiShop.shopItemTemplate.gameObject.SetActive(false);
                break;
            case JohnOrDaughter.Daughter:
                return;

        }
    }
    public void CleanShop()
    {
        chatting = false;
        foreach (Transform shopItem in container)
        {
            if (shopItem.gameObject.name != "ShopGunTemplate" && shopItem.gameObject.name != "ShopMedicTemplate" && shopItem.gameObject.name != "ShopItemTemplate")
            {
                Destroy(shopItem.gameObject);
            }

        }
    }


}
