using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity;
using System.IO;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    #region Public variables
    public Slider staminaSlider;
    public float runStamina;
    public float maxStamina;
    public bool Run;
    public float moveSpeed;
    public float rollSpeed = 5f;
    public float rollCurrent;
    public float startRollTimer;
    public bool canRoll = true;
    public float rollCooldown;
    // public HealthBar healthBar;
    ///public GameObject currentVehicle;
    //public Transform currentVehicleTransform;
    // public bool isDriving;
   
    public List<Quest> quests = new List<Quest>();
    public List<Quest> activeQuest = new List<Quest>();
    public List<Quest> completedQuest = new List<Quest>();
    public List<Quest> finishedQuest = new List<Quest>();
   
    public Quest quest;
    public GameObject bulletUI;
    public List<GameObject> equipSkillButtonList;
    public bool Stunned = false;
    public float slingMagnitude;
    public GameObject SpritePlayer;
    public GameObject WeaponHolder;
    public bool uiStorage = false;
    #endregion

    #region Gun List

    public GameObject Pistol;
    public GameObject Firefly;
    public GameObject Shotgun;
    public GameObject GrenadeLauncher;
    public GameObject LaserBeam;
    public GameObject Handler;
    public GameObject HK106;
    public GameObject DesertEagle;
    public GameObject SilentKing;
    public GameObject Gatling;
    public GameObject FlameThrower;
    public GameObject Eliminator;
    public GameObject ClassicOne;
    public GameObject HuntingRifle;
    public GameObject Watcher;
    public GameObject Dagon;
    public GameObject HeatLaser;
    public GameObject ChargeLaser;
    //   public GameObject laser;
    #endregion

    #region Private variables
    private Dictionary<string, GameObject> buttonDict;
    private Vector2 dashDir;
    private Rigidbody2D rb;
    private float moveH, moveV;
    public bool isRolling;
    bool ctrlReplacement;
    [SerializeField] public Inventory inventory;
    private Vector3 offset;
    private LevelSystem level;
    private PlayerHP HP;
    private HealthBar hpBar;
     [SerializeField] public UI_Inventory uiInventory;
    private List<EnemyHP> nearbyEnemies = new List<EnemyHP>();
    

    #endregion
    

    #region SKill
    public PlayerSkills skill;

    #endregion

    public bool cri; // Tactical Stance
    public bool silent; // Door Sweeper
    public bool takedown; // Takedown
    public bool language; // Wind Language
    public bool vengeance; // Vengeance
    public bool cqc; // CQC
    public bool TakeAim;
    public int moneyCount;
    public TextMeshProUGUI moneyText;
    public bool stealth = false;
    #region SwordRegion
    public SwordHandle Sword;
    public bool slashed = false;
    float swordTiming;
    public float swordCooldown;
    public float swordMaxCooldown;
    public float StartSwordTimer;
    bool spaceReplacement;
    bool canSlash = true;
    #endregion
    // Start is called before the first frame update
    bool Slinging;
   public  bool Gunning;
    public Item itemforTest;
    Transform playerSprite;
    private Transform SlingDirection;
    public Storage storage;
    [SerializeField] public UI_Storage ui_Storage;
    ToggleUI uiToggle;
    public Animator changingSceneAnim;
    public int pistolAmmo;
    public int automaticAmmo;
    public int shotgunAmmo;
    public int batteryAmmo;
    public int sniperAmmo;
    public int GrenadeAmmo;

    public bool ShootingGun;
    Vector2 gunpos;
    float forceGun;

    bool isTutorial;

    public GameObject Tutorial;
    public GameObject PressH;
    bool death;
    int Combo = 0;
    float lastAttack;
    public Animator Danger;
    bool Sensed;

    public float BlockCooldown;
    public float maxBlockCoolDown;
    AudioSource audioSouce;
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip dodgeSound;
    public void AnimSetDanger()
    {
       
            Danger.SetTrigger("Sense");
        StartCoroutine(CooldownSense());
        
    }
    IEnumerator CooldownSense()
    {
        Sensed = true ;
        yield return new WaitForSeconds(5);
        Sensed = false;
    }
   
    void Start()
    {
        Cursor.visible = false;
        audioSouce.GetComponent<AudioSource>();
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        ui_Storage.SetPlayer(this);
        ui_Storage.SetInventory(storage);
        isTutorial = false;
        staminaSlider.maxValue = maxStamina;
        //skillTree = FindObjectOfType<UI_Skilltree>();
        uiToggle = FindObjectOfType<ToggleUI>();
        maxBlockCoolDown = 15;
        playerSprite = GetComponentInChildren<SpriteRenderer>().transform;
       // ItemWorld.SpawnItemWorld(gameObject.transform.position, itemforTest);
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
       
       
      //    ItemWorld.SpawnItemWorld(transform.position, itemforTest);
       //
       //
      // inventory.AddItem(itemforTest);


    }
    private void Awake()
    {
        audioSouce = GetComponent<AudioSource>();
        changingSceneAnim.SetTrigger("ChangeCome");
        audioSouce.volume = 0.5f;
        //uiSkill.SetPlayerSkills(gameObject.GetComponent<Player>().GetPlayerSkills());
        inventory = new Inventory(UseItem);
        inventory = new Inventory(RemoveItem);
        //inventory = new Inventory(TransferItemToStorage);
       // storage = new Storage(TransferItemToInventory);
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
                if (inventory.itemList.Count <= inventory.maxCapacity)
                {
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }
                //áµÐäÍà·Á

            }
        
       
        
       
    }
    void TacticalDamage()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 3);
        foreach(Collider2D col in collider)
        {
            if(col != null)
            {
                BulletScript bullet = col.GetComponent<BulletScript>();
                if (bullet != null)
                {
                    bullet.damage += 1;
                }
            }
          
        }
    }
    void BlockSkill()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 10);
        foreach (Collider2D col in collider)
        {
            if (col != null)
            {
                EnemyBulletScript bullet = col.GetComponent<EnemyBulletScript>();
                if (bullet != null)
                {

                    Destroy(bullet.gameObject);
                    BlockCooldown = maxBlockCoolDown;
                }
            }

        }
    }
    public void KnockbackFromGun(Vector2 pos, float force)
    {
        gunpos = pos;
        forceGun = force;
        
    }
    public void SlingShot(Transform direction, float duration)
    {
        StartCoroutine(SlingTime(duration));
        Vector2 directionSling = direction.position - transform.position;
        directionSling.Normalize();
        rb.velocity = directionSling * 10;
       // Vector2 newPos = Vector2.Lerp(direction, direction, 0.2f);
        //transform.position = Vector2.MoveTowards(transform.position, newPos, moveSpeed * 2);
    }
    IEnumerator SlingTime(float duration)
    {
        Slinging = true;
       // rb.isKinematic = true;
        yield return new WaitForSeconds(duration);
       // rb.isKinematic = false;
        Slinging = false;
    }

    public void SenseDanger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 10);
        foreach(Collider2D col in collider)
        {
            if(col != null)
            {
                if (col.CompareTag("Enemy") && !Sensed)
                {
                    AnimSetDanger();
                }
                
            }
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
              //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Handler:
                Handler.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.HK106:
                HK106.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.DesertEagle:
                DesertEagle.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.SilentKing:
                SilentKing.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Eliminator:
                Eliminator.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.ClassicOne:
                ClassicOne.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Gatling:
                Gatling.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.FlameThrower:
                FlameThrower.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.HuntingRifle:
                HuntingRifle.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Watcher:
                Watcher.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Dagon:
                Dagon.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.HeatLaser:
                HeatLaser.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.LaserCharge:
                ChargeLaser.SetActive(false);
                //  laser.SetActive(false);
                uiInventory.GunUsing.SetActive(false);
                uiInventory.GunImage.sprite = null;
                bulletUI.SetActive(false);
                inventory.RemoveItem(item);
                break;


        }
    }
    
    public void Stun(float stun)
    {
        StartCoroutine(GetStunned(stun));
    }
     IEnumerator GetStunned(float beingStunned)
    {
        Stunned = true;
        DeathEffectInteract.Instance.StunnedEffect(beingStunned);
        yield return new WaitForSeconds(beingStunned);
        Stunned = false;
    }

   public void TransferItemToInventory(Item item)
    {
        storage.TransferToInventory(item, inventory);
    }
    public void TransferItemToStorage(Item item)
    {
        inventory.TransferToStorage(item, storage);
    }
    public IEnumerator Camoflaging()
    {
        stealth = true;
        yield return new WaitForSeconds(5);
        stealth = false;
    }
   void Sprint()
    {
        if(runStamina > 0)
        {
            if (!DeathEffectInteract.Instance.Slowed)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                Time.timeScale = 0.3f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

            }
            audioSouce.clip = runSound;
            if (!audioSouce.isPlaying)
            {
                audioSouce.Play();
            }
            moveSpeed = 4;
            runStamina -= Time.deltaTime;
        }
        else
        {
           
        
            moveSpeed = 2.5f;
            Run = false;
            
        }
       
    }
    public void UseItem(Item item)
    {
        switch (item.type)
        {
            case Item.ItemType.Pistol:
                if (!Gunning)
                {
                    DeactiveGun();
                    Pistol.SetActive(true);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(true);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
               
                break;
            case Item.ItemType.Firefly:
                if (!Gunning)
                {
                    DeactiveGun();
                    Firefly.SetActive(true);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(true);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                
                break;
            case Item.ItemType.ShotGun:
                if (!Gunning)
                {
                    DeactiveGun();
                    Shotgun.SetActive(true);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(true);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
               
                break;
            case Item.ItemType.GrenadeLauncher:
                if (!Gunning)
                {
                    DeactiveGun();
                    GrenadeLauncher.SetActive(true);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.LaserBeam:
                if (!Gunning)
                {
                    DeactiveGun();
                    LaserBeam.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }

                break;
            case Item.ItemType.Handler:
                if (!Gunning)
                {
                    DeactiveGun();
                    Handler.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(true);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.HK106:
                if (!Gunning)
                {
                    DeactiveGun();
                    HK106.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(true);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.DesertEagle:
                if (!Gunning)
                {
                    DeactiveGun();
                    DesertEagle.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.SilentKing:
                if (!Gunning)
                {
                    DeactiveGun();
                    SilentKing.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.Eliminator:
                if (!Gunning)
                {
                    DeactiveGun();
                    Eliminator.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.ClassicOne:
                if (!Gunning)
                {
                    DeactiveGun();
                    ClassicOne.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(true);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.Gatling:
                if (!Gunning)
                {
                    DeactiveGun();
                    Gatling.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(true);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.FlameThrower:
                if (!Gunning)
                {
                    DeactiveGun();
                    FlameThrower.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.HuntingRifle:
                if (!Gunning)
                {
                    DeactiveGun();
                    HuntingRifle.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.Watcher:
                if (!Gunning)
                {
                    DeactiveGun();
                    Watcher.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.Dagon:
                if (!Gunning)
                {
                    DeactiveGun();
                    Dagon.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.HeatLaser:
                if (!Gunning)
                {
                    DeactiveGun();
                    HeatLaser.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.LaserCharge:
                if (!Gunning)
                {
                    DeactiveGun();
                    ChargeLaser.SetActive(true);
                    //laser.SetActive(!laser.activeInHierarchy);
                    uiInventory.GunUsing.SetActive(true);
                    uiInventory.GunImage.sprite = item.GetSprite();
                    bulletUI.SetActive(!bulletUI.activeInHierarchy);
                    Gunning = true;
                }
                else
                {
                    Gunning = false;
                    DeactiveGun();
                }
                break;
            case Item.ItemType.LargeHpPotion:
                Debug.Log("Heal!");
                if(HP.currentHealth < HP.maxHealth)
                {
                    StartCoroutine(HP.Healing(3, 25));
                }
                break;
            case Item.ItemType.MediumHpPotion:
                if (HP.currentHealth < HP.maxHealth)
                {
                    StartCoroutine(HP.Healing(3, 20));
                }
                break;
            case Item.ItemType.LowHpPotion:
                if (HP.currentHealth < HP.maxHealth)
                {
                    StartCoroutine(HP.Healing(2, 15));
                }
                break;
            case Item.ItemType.PistolAmmo:
                pistolAmmo += 50;
                break;
            case Item.ItemType.AutomaticAmmo:
                automaticAmmo += 90;
                break;
            case Item.ItemType.ShotgunAmmo:
                shotgunAmmo += 20;
                break;
            case Item.ItemType.BatteryAmmo:
                batteryAmmo += 300;
                break;
            case Item.ItemType.SniperAmmo:
                sniperAmmo += 25;
                break;
            case Item.ItemType.GrenadeAmmo:
                GrenadeAmmo += 10;
                break;


        }
    }
    public void DeactiveGun()
    {
        Pistol.SetActive(false);
        Firefly.SetActive(false);
        Shotgun.SetActive(false);
        GrenadeLauncher.SetActive(false);
        LaserBeam.SetActive(false);
        Handler.SetActive(false);
        HK106.SetActive(false);
        DesertEagle.SetActive(false);
   SilentKing.SetActive(false);
   Gatling.SetActive(false);
    FlameThrower.SetActive(false);
    Eliminator.SetActive(false);
ClassicOne.SetActive(false);
    HuntingRifle.SetActive(false);
    Watcher.SetActive(false);
    Dagon.SetActive(false);
    HeatLaser.SetActive(false);
    ChargeLaser.SetActive(false);
    //  laser.SetActive(false);

    uiInventory.GunUsing.SetActive(false);
        uiInventory.GunImage.sprite = null;
        bulletUI.SetActive(false);
    }

    private void Update()
    {
        if (!SaveManager.Instance.isLoading)
        {
            if (silent)
            {

                if (!DeathEffectInteract.Instance.Slowed)
                {
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                }
                else
                {
                    Time.timeScale = 0.3f;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;

                }
                BlockCooldown -= Time.deltaTime;
                if (BlockCooldown <= 0)
                {
                    BlockSkill();
                }
            }
            if (cri)
            {
                TacticalDamage();
            }
            if (isTutorial)
            {
                Tutorial.SetActive(true);
                PressH.SetActive(false);
            }
            else
            {
                Tutorial.SetActive(false);
                PressH.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (isTutorial)
                {
                    isTutorial = false;
                }
                else
                {
                    isTutorial = true;
                }
            }
            staminaSlider.value = runStamina;
            if (stealth)
            {
                Color colorSprite = playerSprite.GetComponent<SpriteRenderer>().color;
                colorSprite.a = 0.2f;
                playerSprite.GetComponent<SpriteRenderer>().color = colorSprite;
                gameObject.tag = "Invisible";
                gameObject.layer = 6;
            }
            else
            {
                Color colorSprite = playerSprite.GetComponent<SpriteRenderer>().color;
                colorSprite.a = 1f;
                playerSprite.GetComponent<SpriteRenderer>().color = colorSprite;
                gameObject.tag = "Player";
                gameObject.layer = 6;
            }
            if (Run)
            {

                Sprint();
            }
            else
            {
                if (runStamina < maxStamina)
                {
                    if (!DeathEffectInteract.Instance.Slowed)
                    {
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    }
                    else
                    {
                        Time.timeScale = 0.3f;
                        Time.fixedDeltaTime = 0.02f * Time.timeScale;

                    }
                    runStamina += Time.deltaTime;

                }
                moveSpeed = 2;
                Run = false;

            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && runStamina > 0)
            {
                Run = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) | runStamina <= 0)
            {
                Run = false;
            }
            moneyText.text = moneyCount.ToString();
            if (Input.GetKeyDown(KeyCode.LeftControl) && runStamina > 2)
            {

                Run = false;
                ctrlReplacement = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spaceReplacement = true;
            }
            if (Input.GetMouseButtonDown(0) | Input.GetKeyDown(KeyCode.Space))
            {
                stealth = false;
            }
            if (death)
            {
                rb.velocity = Vector2.zero;
                GameObject WeapoHolder = transform.Find("WeaponHolder").gameObject;
                WeapoHolder.SetActive(false);

            }
            if (HP.currentHealth <= 0)
            {
                death = true;
            }
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
        if (!SaveManager.Instance.isLoading)
        {
            if (rb.velocity.magnitude >= 0.01f && !Run)
            {
                audioSouce.clip = walkSound;
                if (!audioSouce.isPlaying)
                {
                    audioSouce.Play();
                }
            }
            else if (rb.velocity.magnitude <= 0 && !isRolling)
            {
                audioSouce.Stop();
            }

            if (!Stunned && !Slinging && uiToggle.isLayout && !ShootingGun)
            {
                moveH = Input.GetAxis("Horizontal") * moveSpeed;
                moveV = Input.GetAxis("Vertical") * moveSpeed;
                rb.velocity = new Vector2(moveH, moveV);
                Vector2 direction = new Vector2(moveH, moveV);

                Dash();

            }
            if (!Stunned)
            {
                SwordLogic();
            }

            else if (Stunned | !uiToggle.isLayout)
            {
                rb.velocity = Vector3.zero;
            }
            else if (ShootingGun)
            {

                rb.AddForce(-gunpos * forceGun, ForceMode2D.Impulse);
            }
            AimSkillLogic();
        }
       
           //  else if(!Stunned && Slinging)
       //   {
       //      rb.velocity = Vector3.zero;
        //  }
      // 
        
       
         //  else
         //  {
         //      transform.position = currentVehicleTransform.transform.position;
         //  }

        //while(npc.typing == true)
        //{
          //  gameObject.transform.position = Vector2.zero;
        //}/
            
       
        

    }
    void SwordLogic()
    {
        if (!cqc)
        {
            swordCooldown -= Time.deltaTime;

            if (!slashed && swordCooldown <= 0)
            {
                if (spaceReplacement && canSlash)
                {
                    swordCooldown = swordMaxCooldown;
                    Sword.gameObject.SetActive(true);
                    slashed = true;
                    swordTiming = StartSwordTimer;
                    Sword.AnimatedSword();
                }
            }
            else
            {
                swordTiming -= Time.deltaTime;
                if (swordTiming <= 0)
                {
                    slashed = false;
                    Sword.gameObject.SetActive(false);
                }
            }
            //Sword.gameObject.SetActive(false);
            spaceReplacement = false;
        }
        else
        {
            swordCooldown -= Time.deltaTime;
            swordMaxCooldown = 3;
            StartSwordTimer = 3;
          //  swordMaxCooldown = 5;
            if (!slashed && swordCooldown <= 0)
            {
                if (spaceReplacement && canSlash)
                {
                    swordCooldown = swordMaxCooldown;
                    Sword.gameObject.SetActive(true);
                    slashed = true;
                    swordTiming = StartSwordTimer;

                }
            }
            else
            {
                swordTiming -= Time.deltaTime;
                if (swordTiming <= 0)
                {
                    slashed = false;
                    Sword.gameObject.SetActive(false);
                }
            }
            //Sword.gameObject.SetActive(false);
            spaceReplacement = false;
        }
       
    }
    void Dash()
    {
       
        if (!isRolling)
        {
            if (ctrlReplacement && canRoll)
            {
                audioSouce.PlayOneShot(dodgeSound);
                isRolling = true; //initiate dash
                rollCurrent = startRollTimer;
                rb.velocity = Vector2.zero;

                // according to your Input settings this can already cover both arrow keys and WASD, by default that is the case
                var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                // you want to limit the vector to a maximum magnitude of 1 so diagonal movement is not faster than movement on a single axis
                dashDir = Vector2.ClampMagnitude(input, 1f);
                runStamina -= 2;
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
        ctrlReplacement = false;
    }

    void AimSkillLogic()
    {
        if (TakeAim)
        {
            Shooting[] gun = WeaponHolder.transform.GetComponentsInChildren<Shooting>();
            ShootingSMG[] smggun = WeaponHolder.transform.GetComponentsInChildren<ShootingSMG>();
            if (gun != null)
            {
                foreach(Shooting weapon in gun)
                {
                    
                        weapon.TakeAim = true;
                   
                    
                }
            }
            if(smggun != null)
            {
                foreach(ShootingSMG weaponSMG in smggun)
                {
                    weaponSMG.TakeAim = true;
                }
            }
        }
        else
        {
            Shooting[] gun = WeaponHolder.transform.GetComponentsInChildren<Shooting>();
            ShootingSMG[] smggun = WeaponHolder.transform.GetComponentsInChildren<ShootingSMG>();
            if (gun != null)
            {
                foreach (Shooting weapon in gun)
                {

                    weapon.TakeAim = false;


                }
            }
            if (smggun != null)
            {
                foreach (ShootingSMG weaponSMG in smggun)
                {
                    weaponSMG.TakeAim = false;
                }
            }
        }
    }
    IEnumerator TakeAiming()
    {
        TakeAim = true;
        yield return new WaitForSeconds(5);
        TakeAim = false;
    }
    public void Aim()
    {
        StartCoroutine(TakeAiming());
    }
    private void SetModSpeed(float speed)
    {
        moveSpeed += speed;
    }
    public void SetHealingMod(float hp)
    {
    
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

   public void Save()
    {
        skill.Save();
        string savePath = Application.persistentDataPath + "/playerData.json";
        string json = JsonUtility.ToJson(new PlayerData( moneyCount, pistolAmmo, automaticAmmo, shotgunAmmo, batteryAmmo, sniperAmmo,GrenadeAmmo, skill.unlockedSkillTypeList));
        File.WriteAllText(savePath, json);
    }
    public void DeleteSave()
    {

        string savePath = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(savePath))
        {
            
            File.Delete(savePath);
        }
        skill.DeleteSave();
    }
    public void Load()
    {
        string savePath = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            moneyCount = playerData.money;
            pistolAmmo = playerData.pistolAmmo;
            automaticAmmo = playerData.automaticAmmo;
            shotgunAmmo = playerData.shotgunAmmo;
            batteryAmmo = playerData.batteryAmmo;
            GrenadeAmmo = playerData.GrenadeAmmo;

            sniperAmmo = playerData.sniperAmmo;
            skill.Load();
           
          //  this.transform.position = playerData.playerPos;
            Debug.Log(savePath);
        }
    }
    
}
[Serializable]
public class PlayerData
{
    public int money;
    public int pistolAmmo;
    public int automaticAmmo;
    public int shotgunAmmo;
    public int batteryAmmo;
    public int sniperAmmo;
    public int GrenadeAmmo;
    public List<PlayerSkills.SkillType> playerSkill;
    public PlayerData(int money, int pistolAmmo, int automaticAmmo, int shotgunAmmo, int batteryAmmo, int sniperAmmo, int GrenadeAmmo, List<PlayerSkills.SkillType> playerSkill)
    {
      
        this.money = money;
        this.pistolAmmo = pistolAmmo;
        this.automaticAmmo = automaticAmmo;
        this.shotgunAmmo = shotgunAmmo;
        this.batteryAmmo = batteryAmmo;
        this.sniperAmmo = sniperAmmo;
        this.GrenadeAmmo = GrenadeAmmo;
        this.playerSkill = playerSkill;
    }
}

