using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ToggleUI : MonoBehaviour
{
    #region Inventory
    public GameObject uiInventory;
    public GameObject uiInventoryBackground;
    #endregion

    #region GamePlay
    public GameObject HpFill;
   // public GameObject behindBar;
    public GameObject HPBG;
    public GameObject AmmoText;
    public GameObject LevelText;
    public GameObject BackButton;
    public GameObject Gun_Slot;
    public GameObject Gun;
    public Image expFill;
    public Image expBorder;
    public TextMeshProUGUI levelText;
    public Image levelBorder;
    //public GameObject UI_Des;
    #endregion

    #region Quest
    public GameObject Quest_UI;
    #endregion

    #region Skill
    public GameObject Skill_UIBG;
    #endregion

    #region Map
    public GameObject Map_UIBG;
    #endregion

    #region Chatting
    public GameObject Chat_UI;
    #endregion

    #region Shop
    public GameObject Shop_UI;
    public GameObject ShopContainer;
    #endregion
    public bool alreadyUI;
    public bool isLayout;
    public GameObject backButton;

    private void Start()
    {
        isLayout = true;
    }
    private void Update()
    {
       
        if (Input.GetKeyDown("i"))
        {
            if (isLayout && !alreadyUI)
            {
                InventoryToggle();
                alreadyUI = true;
            }
            else
            {
                IntoPlay();
                alreadyUI = false;
            }
            

        }
        if (Input.GetKeyDown("j"))
        {
            if (isLayout && !alreadyUI)
            {
                QuestToggle();
                alreadyUI = true;
            }
            else
            {
                IntoPlay();
                alreadyUI = false;
            }
        }
        if (Input.GetKeyDown("k"))
        {
            if (isLayout && !alreadyUI)
            {
                SkillToggle();
                alreadyUI = true;
            }
            else
            {
                IntoPlay();
                alreadyUI = false;
            }
        }
        if (Input.GetKeyDown("u"))
        {
            if (isLayout && !alreadyUI)
            {
                MapToggle();
                alreadyUI = true;
            }
            else
            {
                IntoPlay();
                alreadyUI = false;
            }
        }



    }
    
    public void InventoryToggle()
    {
        Deactivated();
        uiInventoryBackground.SetActive(true);
        uiInventory.SetActive(true);
        isLayout = false;
        backButton.SetActive(true);
        
        Time.timeScale = 0f;
    }
    public void ShopToggle()
    {
        Deactivated();
        Shop_UI.SetActive(true);
        ShopContainer.SetActive(true);
        isLayout = false;

    }

    public void QuestToggle()
    {
        Deactivated();
        Quest_UI.SetActive(true);
        isLayout = false;
        backButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SkillToggle()
    {
        Deactivated();
        Skill_UIBG.SetActive(true);
        isLayout = false;
        backButton.SetActive(true);
        Time.timeScale = 0f;
    }
    public void MapToggle()
    {
        Deactivated();
        Map_UIBG.SetActive(true);
        isLayout = false;
        backButton.SetActive(true);
        Time.timeScale = 0f;
    }


    public void IntoPlay()
    {

        uiInventoryBackground.SetActive(false);
        uiInventory.SetActive(false);
        Time.timeScale = 1f;
        HpFill.GetComponent<Image>().enabled = true;
        //   behindBar.GetComponent<Image>().enabled = false;
        HPBG.GetComponent<Image>().enabled = true;
        AmmoText.GetComponent<TextMeshProUGUI>().enabled = true;
        LevelText.GetComponent<TextMeshProUGUI>().enabled = true;
        Gun_Slot.GetComponent<Image>().enabled = true;
        levelBorder.enabled = true;
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
        isLayout = true;
    }
    public void Deactivated()
    {
        levelBorder.enabled = false;
        levelText.enabled = false;
        expBorder.enabled = false;
        expFill.enabled = false;
        uiInventoryBackground.SetActive(false);
        uiInventory.SetActive(false);
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
        // Chat_UI.SetActive(false);
    }
}
