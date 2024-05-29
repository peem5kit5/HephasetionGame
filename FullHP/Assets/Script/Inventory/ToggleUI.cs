using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ToggleUI : MonoBehaviour
{
    #region Inventory
    UI_Inventory ui_Inventory;
    public GameObject uiInventory;
    public GameObject uiInventoryBackground;
    public GameObject uiInventoryBorder;
    public GameObject uiInventoryButtonMap;
    public GameObject uiInventoryButtonSkill;
    public GameObject uiInventoryButtonQuest;
    public GameObject uiInventoryButtonSelected;
    #endregion
    #region Storage
    public GameObject uiStorage;
    public GameObject uiStorageBackground;
    #endregion
    #region GamePlay
    public GameObject HpFill;
   // public GameObject behindBar;
    public GameObject HPBG;
    Player player;
    public Image FillStamina;
    public Image FillStaminaBG;
    public GameObject AmmoText;
    public GameObject LevelText;
    public GameObject BackButton;
    public GameObject Gun_Slot;
    public GameObject Gun;
    public Image expFill;
    public Image expBorder;
    public TextMeshProUGUI levelText;
    
    public SpriteRenderer borderSkillQ;
    public SpriteRenderer borderSkillE;
    public SpriteRenderer borderSkillR;

    public Image GunSlot;
    public Image GunIcon;

    public Image SkillQ;
    public Image SkillE;
    public Image SkillR;

    public Image SkillFixQ;
    public Image SkillFixE;
    public Image SkillFixR;

    public TextMeshProUGUI QFont;
    public TextMeshProUGUI EFont;
    public TextMeshProUGUI RFont;
    //public GameObject UI_Des;
    #endregion

    public GameObject blurEffect;
    public GameObject AmmoCounter;
    #region Quest
    public GameObject Quest_UI;
    #endregion

    #region Skill
    UI_Skilltree skillTree;
    public GameObject Skill_UIBG;
    public Image skillCoolDownQ;
    public Image skillCoolDownE;
    public Image skillCoolDownR;


    #endregion

    #region Map
    public GameObject Map_UIBG;
    #endregion

    #region Chatting
    public GameObject Chat_UI;
    public GameObject AcceptButton;
    public GameObject RefuseButton;
    public bool isChatting;

    #endregion

    #region Shop
    public GameObject Shop_UI;
    public GameObject ShopContainer;
    #endregion
    public bool alreadyUI;
    public bool isLayout;
    public GameObject backButton;

    public SpriteRenderer crossHair;
    public GameObject FOV;


    public GameObject ESC_UI;
    private void Start()
    {
        ui_Inventory = FindObjectOfType<UI_Inventory>();
        isLayout = true;
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isLayout && !alreadyUI && !isChatting)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                InventoryToggle();
                alreadyUI = true;
            }
            else if (!isChatting)
            {
                IntoPlay();
                alreadyUI = false;
            }
            

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isLayout && !alreadyUI && !isChatting)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                QuestToggle();
                alreadyUI = true;
            }
            else if (!isChatting)
            {
                IntoPlay();
                alreadyUI = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isLayout && !alreadyUI && !isChatting)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                SkillToggle();
                
                alreadyUI = true;
            }
            else if (!isChatting)
            {
                IntoPlay();
                alreadyUI = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isLayout && !alreadyUI && !isChatting)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                MapToggle();
                alreadyUI = true;
            }
            else if (!isChatting)
            {
                IntoPlay();
                alreadyUI = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isLayout && !alreadyUI && !isChatting)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                OptionToggle();
                alreadyUI = true;
            }
            else if(!isChatting)
            {
                IntoPlay();
                alreadyUI = false;
            }
        }



    }
    public void UpdateSkillVisual()
    {

        
        foreach(PlayerSkills.SkillType playerSkill in player.skill.unlockedSkillTypeList)
        {
            if (playerSkill == PlayerSkills.SkillType.Ember)
            {
                //skillTree.Ember.SetActive(true);
                skillTree.EmberPath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.TrapMine)
            {
                // skillTree.TrapWires.SetActive(true);
                skillTree.MinePath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.DoorSweeper)
            {
                skillTree.DoorPath.SetActive(true);
                //skillTree.EmberPath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.TacticalStance)
            {
                skillTree.PathTacticalPath.SetActive(true);
                // skillTree.EmberPath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.Camoflage)
            {
                // skillTree.Camoflage.SetActive(true);
                skillTree.CamoPath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.TakeAim)
            {
                //  skillTree.TakeAim.SetActive(true);
                skillTree.TakeAimPath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.TakeDown)
            {
                skillTree.TakeDownPath.SetActive(true);
                //skillTree.EmberPath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.Vengeance)
            {
                skillTree.VengenPath.SetActive(true);
                //skillTree.EmberPath.SetActive(true);
            }
            if (playerSkill == PlayerSkills.SkillType.CQC)
            {
                skillTree.PathCQCPath.SetActive(true);
                //skillTree.EmberPath.SetActive(true);
            }
        }
       
    }
    public void StorageToggle()
    {
        Deactivated();
        Cursor.visible = true;
        Chat_UI.SetActive(false);
        player.uiStorage = true;
        backButton.SetActive(true);
        uiInventoryBackground.SetActive(true);
        uiInventory.SetActive(true);
        uiStorage.SetActive(true);
        uiStorageBackground.SetActive(true);
        uiInventoryButtonSkill.SetActive(false);
        uiInventoryButtonSelected.SetActive(false);
        uiInventoryButtonQuest.SetActive(false);
        uiInventoryButtonMap.SetActive(false);
        uiInventoryBorder.SetActive(false);
    }
    public void InventoryToggle()
    {
        Deactivated();
        uiInventoryBackground.SetActive(true);
        uiInventory.SetActive(true);
        Cursor.visible = true;
        crossHair.enabled = false;
        isLayout = false;
        backButton.SetActive(true);
        uiInventoryButtonSkill.SetActive(true);
        uiInventoryButtonSelected.SetActive(true);
        uiInventoryButtonQuest.SetActive(true);
        uiInventoryButtonMap.SetActive(true);
        blurEffect.SetActive(true);
        uiInventoryBorder.SetActive(true);
    }
    public void ShopToggle()
    {
        Deactivated();
        Shop_UI.SetActive(true);
        ShopContainer.SetActive(true);
        Cursor.visible = true;
        crossHair.enabled = false;
        isLayout = false;

    }

    public void QuestToggle()
    {
        Deactivated();
        Quest_UI.SetActive(true);
        Cursor.visible = true;
        crossHair.enabled = false;
        isLayout = false;
        blurEffect.SetActive(true);
        backButton.SetActive(true);
    }
    public void OptionToggle()
    {
        Deactivated();
        ESC_UI.SetActive(true);
        isLayout = false;
        Cursor.visible = true;
        crossHair.enabled = false;
        blurEffect.SetActive(true);
    }
    public void SkillToggle()
    {
        Deactivated();
        Skill_UIBG.SetActive(true);
        Cursor.visible = true;
        crossHair.enabled = false;
        isLayout = false;
        skillTree = FindObjectOfType<UI_Skilltree>();
        UpdateSkillVisual();
        blurEffect.SetActive(true);
        backButton.SetActive(true);
    }
    public void MapToggle()
    {
        Deactivated();
        Map_UIBG.SetActive(true);
        Cursor.visible = true;
        crossHair.enabled = false;
        isLayout = false;
        blurEffect.SetActive(true);
        backButton.SetActive(true);
    }

    public void ChatToggle()
    {
        Deactivated();
        Cursor.visible = true;
        crossHair.enabled = false;
        AcceptButton.SetActive(false);
        RefuseButton.SetActive(false);
        isLayout = false;
    }
    public void IntoPlay()
    {
        isChatting = false;
        ESC_UI.SetActive(false);
        skillCoolDownQ.enabled = true;
        skillCoolDownE.enabled = true;
        skillCoolDownR.enabled = true;
        //AmmoCounter.SetActive(true);
        blurEffect.SetActive(false);
        player.uiStorage = false;
        //  player.uiInventory.storageUI = false;
        AcceptButton.SetActive(false);
        RefuseButton.SetActive(false);
        uiStorage.SetActive(false);
        uiStorageBackground.SetActive(false);
        uiInventoryButtonSkill.SetActive(false);
        uiInventoryButtonSelected.SetActive(false);
        uiInventoryButtonQuest.SetActive(false);
        uiInventoryButtonMap.SetActive(false);
        uiInventoryBorder.SetActive(false);
        FillStamina.enabled = true;
        FillStaminaBG.enabled = true;
        FOV.SetActive(true);
        crossHair.enabled = true;
        Cursor.visible = false;
        uiInventoryBackground.SetActive(false);
        uiInventory.SetActive(false);
        Time.timeScale = 1f;
        HpFill.GetComponent<Image>().enabled = true;
        //   behindBar.GetComponent<Image>().enabled = false;
        HPBG.GetComponent<Image>().enabled = true;
        AmmoText.GetComponent<TextMeshProUGUI>().enabled = true;
        LevelText.GetComponent<TextMeshProUGUI>().enabled = true;
        Gun_Slot.GetComponent<Image>().enabled = true;
        levelText.enabled = true;
        expBorder.enabled = true;
        expFill.enabled = true;
        Quest_UI.SetActive(false);
        Skill_UIBG.SetActive(false);
        Chat_UI.SetActive(false);
        Map_UIBG.SetActive(false);
        backButton.SetActive(false);
        Shop_UI.SetActive(false);
        ShopContainer.SetActive(false);
   
        borderSkillE.enabled = true;
        borderSkillQ.enabled = true;
        borderSkillR.enabled = true;

        GunSlot.enabled = true;
        GunIcon.enabled = true;
        SkillFixQ.enabled = true;
        SkillFixE.enabled = true;
        SkillFixR.enabled = true;
        GameObject weaponHold = player.transform.Find("WeaponHolder").gameObject;
        weaponHold.SetActive(true);
        Color borderEcolor = borderSkillE.color;
        borderEcolor.a = 1f;
        borderSkillE.color = borderEcolor;


        Color borderQcolor = borderSkillQ.color;
        borderQcolor.a = 1f;
        borderSkillQ.color = borderQcolor;

        Color borderRcolor = borderSkillR.color;
        borderRcolor.a = 1f;
        borderSkillR.color = borderRcolor;

       Color SkillQcolor = SkillQ.color;
        SkillQcolor.a = 1f;
        SkillQ.color = SkillQcolor;
        Color SkillEcolor = SkillE.color;
        SkillEcolor.a = 1f;
        SkillE.color = SkillEcolor;

        Color SkillRcolor = SkillR.color;
        SkillRcolor.a = 1f;
        SkillR.color = SkillRcolor;

        Color SkillFixQcolor = SkillFixQ.color;
        SkillFixQcolor.a = 1f;
        SkillFixQ.color = SkillFixQcolor;
        Color SkillFixEcolor = SkillFixE.color;
        SkillFixEcolor.a = 1f;
        SkillFixE.color = SkillFixEcolor;
        Color SkillFixRcolor = SkillFixR.color;
        SkillFixRcolor.a = 1f;
        SkillFixR.color = SkillFixRcolor;
        QFont.enabled = true;
        EFont.enabled = true;
        RFont.enabled = true;
        isLayout = true;
    }
    public void Deactivated()
    {
        ESC_UI.SetActive(false);
        skillCoolDownQ.enabled = false;
        skillCoolDownE.enabled = false;
        skillCoolDownR.enabled = false;

        //AmmoCounter.SetActive(false);
        blurEffect.SetActive(false);
        player.uiStorage = false;
        // player.uiInventory.storageUI = false;
        AcceptButton.SetActive(false);
        RefuseButton.SetActive(false);
        uiStorage.SetActive(false);
        uiStorageBackground.SetActive(false);
        uiInventoryButtonSkill.SetActive(false);
        uiInventoryButtonSelected.SetActive(false);
        uiInventoryButtonQuest.SetActive(false);
        uiInventoryButtonMap.SetActive(false);
        uiInventoryBorder.SetActive(false);
        FOV.SetActive(false);

        crossHair.enabled = false;
        levelText.enabled = false;
        expBorder.enabled = false;
        expFill.enabled = false;
        uiInventoryBackground.SetActive(false);
        uiInventory.SetActive(false);
        Cursor.visible = false;
        // Time.timeScale = 1f;
        //HpFill.GetComponent<Image>().enabled = false;
        //   behindBar.GetComponent<Image>().enabled = false;
        //HPBG.GetComponent<Image>().enabled = false;
        //  ManaBar.GetComponent<Image>().enabled = false;
        //  AmmoText.GetComponent<TextMeshProUGUI>().enabled = false;
        //  Mana.GetComponent<Image>().enabled = false;
        //   LevelText.GetComponent<TextMeshProUGUI>().enabled = false;
        //   Gun_Slot.GetComponent<Image>().enabled = false;
        HPBG.GetComponent<Image>().enabled = false;
        HpFill.GetComponent<Image>().enabled = false;
        Quest_UI.SetActive(false);
        Skill_UIBG.SetActive(false);
        Map_UIBG.SetActive(false);
        Shop_UI.SetActive(false);
        ShopContainer.SetActive(false);
       
        Color borderEcolor = borderSkillE.color;
        borderEcolor.a = 0f;
        borderSkillE.color = borderEcolor;


        Color borderQcolor = borderSkillQ.color;
        borderQcolor.a = 0f;
        borderSkillQ.color = borderQcolor;

        Color borderRcolor = borderSkillR.color;
        borderRcolor.a = 0f;
        borderSkillR.color = borderRcolor;

        Color SkillQcolor = SkillQ.color;
        SkillQcolor.a = 0f;
        SkillQ.color = SkillQcolor;
        Color SkillEcolor = SkillE.color;
        SkillEcolor.a = 0f;
        SkillE.color = SkillEcolor;

        Color SkillRcolor = SkillR.color;
        SkillRcolor.a = 0f;
        SkillR.color = SkillRcolor;

        Color SkillFixQcolor = SkillFixQ.color;
        SkillFixQcolor.a = 0f;
        SkillFixQ.color = SkillFixQcolor;
        Color SkillFixEcolor = SkillFixE.color;
        SkillFixEcolor.a = 0f;
        SkillFixE.color = SkillFixEcolor;
        Color SkillFixRcolor = SkillFixR.color;
        SkillFixRcolor.a = 0f;
        SkillFixR.color = SkillFixRcolor;
        borderSkillE.enabled = false;
        borderSkillQ.enabled = false;
        borderSkillR.enabled = false;
        FillStamina.enabled = false;
        FillStaminaBG.enabled = false;
        
        GunSlot.enabled = false;
        GunIcon.enabled = false;

      
        SkillFixQ.enabled = false;
        SkillFixE.enabled = false;
        SkillFixR.enabled = false;

        QFont.enabled = false;
        EFont.enabled = false;
        RFont.enabled = false;
        // Chat_UI.SetActive(false);
    }
}
