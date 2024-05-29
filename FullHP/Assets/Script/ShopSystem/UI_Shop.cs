using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
public class UI_Shop : MonoBehaviour
{
    public Transform container;
    private Transform ScrollArea;
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

    ToggleUI uiToggle;
    private void Awake()
    {
        ItemShow.enabled = false;
        ScrollArea = transform.Find("ScrollArea");
        Transform Viewport = ScrollArea.Find("Viewport");
        Transform Containers = Viewport.Find("Containers");
        container = Containers.Find("Container");
        //  shopItemTemplate = container.Find("ShopItemTemplate");
        // shopItemTemplate.gameObject.SetActive(false);
        uiToggle = FindObjectOfType<ToggleUI>();
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
    public void CreateItemShop(string Itemname,int damages,float firerate,Item.ItemType type,Sprite itemSprite,int cost, int positionIndex)
    {
       
        
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 75f;
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
        if(player.inventory.itemList.Count < player.inventory.maxCapacity)
        {
            List<Item> itemList = new List<Item>();
            if (player.moneyCount >= cost)
            {
                player.moneyCount -= cost;
                Item item = new Item();
                item.type = itemType;
                item.amount = 1;
                player.inventory.AddItem(item);
                UpdateMoneyCountText();
            }
        }
        
       
    }
   
    
    public void Hide()
    {
        uiToggle.isChatting = false;
        ItemNameText.text = "";
        DamagesText.text = "";
        FireRateText.text = "";
        PriceText.text = "";
        ItemShow.enabled = false;
        shopItemTemplate = container.Find("ShopGunTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        shopItemTemplate = container.Find("ShopMedicTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    void UpdateMoneyCountText()
    {
        moneyCountText.text = player.moneyCount.ToString();
    }
}
