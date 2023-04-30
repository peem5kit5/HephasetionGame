using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    //  public Image itemDescriptionImage;
    // public TextMeshProUGUI itemDescriptionName;
    // public TextMeshProUGUI itemDesctiptionText;
    //  public Button UseButton;
    // public ToggleUI UI_Des;
    public Transform ScrollArea;
    public Transform Scroll;
    public Transform Container;
    public Transform itemSlotContain;
    public Transform itemSlotTemplate;
    public GameObject GunUsing;
    public Image GunImage;

    public GameObject InventoryPop;
    

    private Inventory inventory;
    private Player player;
    private Item itemConnect;

    private void Awake()
    {
         ScrollArea = transform.Find("ScrollArea");
         Scroll = ScrollArea.Find("Scroll");
         Container = Scroll.Find("Container");
         itemSlotContain = Container.Find("itemSlotContain");
         itemSlotTemplate = itemSlotContain.Find("itemSlotTemplate");
    }
  
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItem();
    }
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItem();
    }
    private void RefreshInventoryItem()
    {
        foreach (Transform child in itemSlotContain)
        {
            if (child == itemSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellx = 40f;
        float itemSlotCelly = 25f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRect = Instantiate(itemSlotTemplate, itemSlotContain).GetComponent<RectTransform>();
            itemSlotRect.gameObject.SetActive(true);
            
            itemSlotRect.GetComponent<Button_UI>().ClickFunc = () =>
            {
                itemConnect = item;
                //UseItem_UI(item);
                player.UseItem(item);
                //UseItem_UI(item);
                getReturn();
                
            };
            itemSlotRect.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                player.RemoveItem(item);
                
            };
            itemSlotRect.anchoredPosition = new Vector2(x * itemSlotCellx, y * itemSlotCelly);
            Image image = itemSlotRect.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uitext = itemSlotRect.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uitext.SetText(item.amount.ToString());
            }
            else
            {
                uitext.SetText("");
            }

            x++;
            if (x > 2)
            {
                x = 0;
                y--;
            }
            
        }

    }
    public void DisplayDescription(Item item)
    {
        //UI_Des.UI_Des.SetActive(true);
      //  itemDescriptionImage.enabled = true;
      //  itemDescriptionName.enabled = true;
      //  itemDesctiptionText.enabled = true;

     //   itemDesctiptionText.text = item.GetDes();
     //   itemDescriptionName.text = item.GetName();
     //   itemDescriptionImage.sprite = item.GetSprite();

     //   UseButton.enabled = true;


    }
    private Item getReturn()
    {
       return itemConnect;
    }
   
}
