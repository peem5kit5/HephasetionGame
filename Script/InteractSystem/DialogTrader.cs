using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogTrader : MonoBehaviour
{
    public GameObject weapon;

    public TextMeshProUGUI TalkDisplay;
    public bool dialogueIsPlaying { get; private set; }
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool chatting = false;
    private bool playerinRange;
    public bool typing;
    public GameObject notice;
    private ToggleUI uiToggle;
    //public GameObject questUI;

    public GameObject chatUI;
    public GameObject continueButton;
    bool request = false;
    private UI_Shop uiShop;
    public TraderType Merchant;
    public enum TraderType
    {
        Gunsmith,
        ItemShop,
        FoodShop
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerinRange = true;
            notice.SetActive(true);
            Debug.Log("Chat");


        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        playerinRange = false;
        notice.SetActive(false);

    }
    void Start()
    {
        uiShop = FindObjectOfType<UI_Shop>();
        SetShop();
        uiToggle = FindObjectOfType<ToggleUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chatting)
        {
            weapon.SetActive(false);
        }
        else
        {
            weapon.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F) && playerinRange)
        {
            index = 0;
            chatUI.SetActive(true);
            StartCoroutine(Type());
            typing = true;
        }
        if (typing == true)
        {

            if (TalkDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
            else if (request)
            {
                continueButton.SetActive(false);
            }
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in this.sentences[index].ToCharArray())
        {
            TalkDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }
    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            TalkDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            TalkDisplay.text = "";
            chatting = false;
            typing = false;
            chatUI.SetActive(false);
            
            uiToggle.ShopToggle();
            CleanShop();
            //uiToggle.IntoPlay();
        }
    }

    public void SetShop()
    {
         
        switch (Merchant)
        {
            default:
            case TraderType.Gunsmith:
                //uiShop.Show();
                uiShop.shopItemTemplate = uiShop.container.Find("ShopGunTemplate");
                uiShop.shopItemTemplate.gameObject.SetActive(true);
                uiShop.CreateItemShop("Pistol", 7, 7, Item.ItemType.Pistol, ItemAssets.Instance.PistolSprite, Item.GetCost(Item.ItemType.Pistol), 0);
                uiShop.CreateItemShop("Shotgun", 8, 1, Item.ItemType.ShotGun, ItemAssets.Instance.ShotGunSprite, Item.GetCost(Item.ItemType.ShotGun), 1);
                break;

        }
    }
    public void CleanShop()
    {
        switch (Merchant)
        {
            default:
            case TraderType.Gunsmith:
                //uiShop.Show();
                uiShop.shopItemTemplate = uiShop.container.Find("ShopGunTemplate");
                uiShop.shopItemTemplate.gameObject.SetActive(true);
                break;

        }
    }
  
}
