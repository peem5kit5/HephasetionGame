using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
public class UI_Shop : MonoBehaviour
{
    public Transform container;
    public Transform shopItemTemplate;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI DamagesText;
    public TextMeshProUGUI FireRateText;
    public TextMeshProUGUI PriceText;

    public TextMeshProUGUI moneyCountText;
    public Image ItemShow;
    public Button BuyButton;
    private Player player;
    private Inventory inv;
    private void Awake()
    {
        ItemShow.enabled = false;
        container = transform.Find("Container");
      //  shopItemTemplate = container.Find("ShopItemTemplate");
       // shopItemTemplate.gameObject.SetActive(false);
        Hide();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        inv = FindObjectOfType<Inventory>();
        
    }
    private void Update()
    {
        moneyCountText.text = player.moneyCount.ToString();
    }
    public void CreateItemShop(string Itemname,int damages,int firerate,Item.ItemType type,Sprite itemSprite,int cost, int positionIndex)
    {
       
 
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            OpenButton(type,cost);
            ItemNameText.text = Itemname;
            DamagesText.text = damages.ToString();
            FireRateText.text = firerate.ToString();
            PriceText.text = cost.ToString();
            ItemShow.enabled = true;
            ItemShow.sprite = itemSprite;
        };
    }
    void OpenButton(Item.ItemType type, int cost)
    {
        BuyButton.onClick.RemoveAllListeners();
        BuyButton.onClick.AddListener(() => TryBuyItem(type, cost));
    }
    void TryBuyItem(Item.ItemType itemType, int cost)
    {
        if(player.moneyCount >= cost)
        {
            UpdateMoneyCountText();
            player.moneyCount -= cost;
            Item item = new Item();
            item.type = itemType;
            ItemWorld.SpawnItemWorld(player.gameObject.transform.position, item);
        }
    }
   
    
    public void Hide()
    {
        ItemNameText.text = "";
        DamagesText.text = "";
        FireRateText.text = "";
        PriceText.text = "";
        ItemShow.enabled = false;
        shopItemTemplate = container.Find("ShopGunTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        shopItemTemplate = container.Find("ShopFoodTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    void UpdateMoneyCountText()
    {
        moneyCountText.text = player.moneyCount.ToString();
    }
}
