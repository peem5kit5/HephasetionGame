using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class Player : MonoBehaviour
{
   
    #region Public variables
    public float moveSpeed = 5.0f;
    public float rollSpeed = 5f;
    public float rollCurrent;
    public float startRollTimer;
    public bool canRoll = true;
    public float rollCooldown;
   // public HealthBar healthBar;
    public GameObject currentVehicle;
    public Transform currentVehicleTransform;
    public bool isDriving;

    public List<Quest> quests = new List<Quest>();
    public List<Quest> activeQuest = new List<Quest>();
    public List<Quest> completedQuest = new List<Quest>();
    public List<Quest> finishedQuest = new List<Quest>();
   
    public Quest quest;
    public GameObject bulletUI;
    public List<GameObject> equipSkillButtonList;
    
    #endregion

    #region Gun List
   
    public GameObject Pistol;
    public GameObject Firefly;
    public GameObject Shotgun;
    public GameObject GrenadeLauncher;
    #endregion

    #region Private variables
    private Dictionary<string, GameObject> buttonDict;
    private Vector2 dashDir;
    private Rigidbody2D rb;
    private float moveH, moveV;
    bool isRolling;
    bool shiftReplacement;
    public Inventory inventory;
    private Vector3 offset;
    private LevelSystem level;
    private PlayerHP HP;
    private HealthBar hpBar;
    [SerializeField] private UI_Inventory uiInventory;
    private List<EnemyHP> nearbyEnemies = new List<EnemyHP>();


    #endregion

    #region SKill
    private PlayerSkills skill;

    #endregion

    public bool cri; // Tactical Stance
    public bool silent; // Door Sweeper
    public bool takedown; // Takedown
    public bool language; // Wind Language
    public bool vengeance; // Vengeance
    public bool cqc; // CQC

    public int moneyCount;
    public TextMeshProUGUI moneyText;
    // Start is called before the first frame update

    void Start()
    {
        buttonDict = new Dictionary<string, GameObject>();
        foreach(GameObject button in equipSkillButtonList)
        {
            string buttonName = button.name;
            buttonDict[buttonName] = button;
            button.SetActive(false);
        }
        cri = false;
        EnemyHP[] enemies = FindObjectsOfType<EnemyHP>();
        foreach (EnemyHP enemy in enemies)
        {
            nearbyEnemies.Add(enemy);
        }
        hpBar = FindObjectOfType<HealthBar>();
        HP = GetComponent<PlayerHP>();
        level = GetComponent<LevelSystem>();
        rb = GetComponent<Rigidbody2D>();
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        
    }
    private void Awake()
    {
        
        //uiSkill.SetPlayerSkills(gameObject.GetComponent<Player>().GetPlayerSkills());
        inventory = new Inventory(UseItem);
        inventory = new Inventory(RemoveItem);
        skill = new PlayerSkills();
        skill.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        
    }
    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case PlayerSkills.SkillType.TacticalStance:
                Crit();
                break;
            case PlayerSkills.SkillType.DoorSweeper:
                Silent();
                break;
            case PlayerSkills.SkillType.TakeDown:
                TakeDown();
                break;
            case PlayerSkills.SkillType.WindLanguage:
                Wind();
                break;
            case PlayerSkills.SkillType.Vengeance:
                Vengeance();
                break;
            case PlayerSkills.SkillType.CQC:
                CQC();
                break;
                
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //·µ–‰Õ‡∑¡
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
       
    }
    public void RemoveItem(Item item)
    {
        switch (item.type)
        {
            case Item.ItemType.Pistol:
                Pistol.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                
                break;
            case Item.ItemType.Firefly:
                Firefly.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.ShotGun:
                Shotgun.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.GrenadeLauncher:
                GrenadeLauncher.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            

        }
    }
    public void UseItem(Item item)
    {
        switch (item.type)
        {
            case Item.ItemType.Pistol:
                Pistol.SetActive(!Pistol.activeInHierarchy);
                uiInventory.GunUsing.SetActive(!uiInventory.GunUsing.activeInHierarchy);
                uiInventory.GunImage.sprite = item.GetSprite();
                bulletUI.SetActive(!bulletUI.activeInHierarchy);
                break;
            case Item.ItemType.Firefly:
                Firefly.SetActive(!Firefly.activeInHierarchy);
                uiInventory.GunUsing.SetActive(!uiInventory.GunUsing.activeInHierarchy);
                uiInventory.GunImage.sprite = item.GetSprite();
                bulletUI.SetActive(!bulletUI.activeInHierarchy);
                break;
            case Item.ItemType.ShotGun:
                Shotgun.SetActive(!Shotgun.activeInHierarchy);
                uiInventory.GunUsing.SetActive(!uiInventory.GunUsing.activeInHierarchy);
                uiInventory.GunImage.sprite = item.GetSprite();
                bulletUI.SetActive(!bulletUI.activeInHierarchy);
                break;
            case Item.ItemType.GrenadeLauncher:
                GrenadeLauncher.SetActive(!GrenadeLauncher.activeInHierarchy);
                uiInventory.GunUsing.SetActive(!uiInventory.GunUsing.activeInHierarchy);
                uiInventory.GunImage.sprite = item.GetSprite();
                bulletUI.SetActive(!bulletUI.activeInHierarchy);
                break;

        }
    }


    private void Update()
    {
        moneyText.text = moneyCount.ToString();
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                shiftReplacement = true;
            }
       
        
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //level.AddExp(50);
        // }

        //  if(healthBar.currentHP <= 0)
        //  {
        //       Destroy(this.gameObject);
        //   }

    }
  
    void FixedUpdate()
    {
       
           if (!isDriving)
           {
               moveH = Input.GetAxis("Horizontal") * moveSpeed;
               moveV = Input.GetAxis("Vertical") * moveSpeed;
               rb.velocity = new Vector2(moveH, moveV);
               Vector2 direction = new Vector2(moveH, moveV);

               Dash();
           }
           else
           {
               transform.position = currentVehicleTransform.transform.position;
           }

        //while(npc.typing == true)
        //{
          //  gameObject.transform.position = Vector2.zero;
        //}/
            
       
        

    }
    void Dash()
    {
        if (!isRolling)
        {
            if (shiftReplacement && canRoll)
            {
                isRolling = true; //initiate dash
                rollCurrent = startRollTimer;
                rb.velocity = Vector2.zero;

                // according to your Input settings this can already cover both arrow keys and WASD, by default that is the case
                var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                // you want to limit the vector to a maximum magnitude of 1 so diagonal movement is not faster than movement on a single axis
                dashDir = Vector2.ClampMagnitude(input, 1f);
                StartCoroutine(CanRoll());
            }
        }
        else
        {
            rb.velocity = dashDir * rollSpeed;

            rollCurrent -= Time.deltaTime;

            if (rollCurrent <= 0)
            {
                isRolling = false;
            }
        }
        shiftReplacement = false;
    }
    private void SetModSpeed(float speed)
    {
        moveSpeed += speed;
    }
    public void SetHealingMod(float hp)
    {
        HP.Healing(hp);
    }
    public PlayerSkills GetPlayerSkills()
    {
        return skill;
    }
    void Silent()
    {
        silent = true;
    }
    void TakeDown()
    {
        takedown = true;
    }
    void Wind()
    {
        language = true;
    }
    void Vengeance()
    {
        vengeance = true;
    }
    void CQC()
    {
        cqc = true;
    }
    void Crit()
    {
        cri = true;
    }
    IEnumerator CanRoll()
    {
        canRoll = false;
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }
    public bool CanUseGodLight()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.GodLight);
    }
    public bool CanUseTheDoorSweeper()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.DoorSweeper);
    }
    public bool CanUseTacticalStance()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.TacticalStance);
    }
    public bool CanUseTrapwires()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.TrapMine);
    }
    public bool CanUseTakeDown()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.TakeDown);
    }
    public bool CanUseWindLanguage()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.WindLanguage);
    }
    public bool CanUseCamoflage()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.Camoflage);
    }
    public bool CanUseVengeance()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.Vengeance);
    }
    public bool CanUseEmber()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.Ember);
    }
    public bool CanUseCQC()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.CQC);
    }
    public bool CanUseTakeAim()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.TakeAim);
    }
    public bool CanUseSlingShot()
    {
        return skill.IsSkillUnlocked(PlayerSkills.SkillType.SlingShotArrow);
    }

    public void BougthItem(Item.ItemType itemType)
    {
        Debug.Log("Bought item:" + itemType);
    }
}
