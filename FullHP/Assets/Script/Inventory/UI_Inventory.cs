using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
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

    public Image GunUIinInventory;
    public Button DequipButton;

    private Inventory inventory;
    private Player player;
    private Storage storage;
    public Item itemConnect;
    public Item itemForSave;
    public TextMeshProUGUI ItemCount;
    bool ItemInHand;
    private void Awake()
    {
       
        storage = FindObjectOfType<Storage>();
        ScrollArea = transform.Find("ScrollArea");
        Scroll = ScrollArea.Find("Scroll");
        Container = Scroll.Find("Container");
        itemSlotContain = Container.Find("itemSlotContain");
        itemSlotTemplate = itemSlotContain.Find("itemSlotTemplate");
        
    }
  public void LoadItemInHand()
    {
        string savePath = Application.persistentDataPath + "/itemInHand.json";
        if (File.Exists(savePath))
        {

            string json = File.ReadAllText(savePath);
            ItemInHandData itemInHand = JsonUtility.FromJson<ItemInHandData>(json);
            itemForSave = itemInHand.itemInHand;
            player.UseItem(itemForSave);
            CheckItemEquip(itemForSave);
     

        }
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
        
    }

    public void DeleteSave()
    {
        string savePath = Application.persistentDataPath + "/itemInHand.json";
        if (File.Exists(savePath))
        {

            File.Delete(savePath);

        }
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        
        inventory.OnItemListChanged_Inventory += Inventory_OnItemListChanged;

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
        float itemSlotCelly = 20f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRect = Instantiate(itemSlotTemplate, itemSlotContain).GetComponent<RectTransform>();
            itemSlotRect.gameObject.SetActive(true);
            itemSlotRect.GetComponent<Button_UI>().ClickFunc = () =>
            {
                if (!player.uiStorage)
                {
                    if (!ItemInHand)
                    {
                       // itemConnect = item;
                        //UseItem_UI(item);
                        player.UseItem(item);
                        inventory.RemoveItem(item);
                        CheckItemEquip(item);

                    }
                    else
                    {

                        ConsumeItem(item);
                    }
                
                    //UseItem_UI(item);
                }
                else
               {
                   // itemConnect = item;
                    //UseItem_UI(item);
                    if(item != null)
                    {
                        inventory.TransferToStorage(item, player.storage);
                       // inventory.RemoveItem(item);
                    }
                   
                    //UseItem_UI(item);
                    //getReturn();
                    
                }
               
                
            };
            itemSlotRect.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
               // if (!storageUI)
           //     {
                   // ItemWorld.DropItem(item);
                    inventory.RemoveItem(item);
                      ItemWorld.DropItem(item);
                //   }
                //    

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
        ItemCount.text = $"{inventory.itemList.Count} / {inventory.maxCapacity} ";

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
    void CheckItemEquip(Item item)
    {
      
        
        switch (item.type)
        {
            case Item.ItemType.Firefly:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.FireFlySprite;
               itemForSave = item;
               // getReturn();
              //  SaveItemInHand(item);
                break;
            case Item.ItemType.Pistol:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.PistolSprite;
                itemForSave = item;
                // getReturn();
                // SaveItemInHand(item);
                break;
            case Item.ItemType.LaserBeam:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.LaserBeam;
                itemForSave = item;
                // itemConnect = item;
                // getReturn();
                //SaveItemInHand(item);
                break;
            case Item.ItemType.GrenadeLauncher:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.GrenadeLauncherSprite;
                itemForSave = item;
                //  itemConnect = item;
                //  getReturn();
                //  SaveItemInHand(item);
                break;
            case Item.ItemType.ShotGun:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.ShotGunSprite;
                itemForSave = item;
                //  itemConnect = item;
                //   SaveItemInHand(item);
                break;
            case Item.ItemType.Handler:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.HandlerSprite;
                itemForSave = item;
                //  itemConnect = item;
                //  getReturn();
                //  SaveItemInHand(item);
                break;
            case Item.ItemType.HK106:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.HK106Sprite;
                itemForSave = item;
                //   itemConnect = item;
                //   getReturn();
                //   SaveItemInHand(item);
                break;
            case Item.ItemType.DesertEagle:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.DesertEagleSprite;
                itemForSave = item;
                //  itemConnect = item;
                // getReturn();
                //   SaveItemInHand(item);
                break;
            case Item.ItemType.SilentKing:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.SilentKingSprite;
                itemForSave = item;
                //  itemConnect = item;
                //  getReturn();
                //   SaveItemInHand(item);
                break;
            case Item.ItemType.Eliminator:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.EliminatorSprite;
                itemForSave = item;
                //  itemConnect = item;
                //getReturn();
                // SaveItemInHand(item);
                break;
            case Item.ItemType.ClassicOne:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.ClassicOneSprite;
                itemForSave = item;
                //   itemConnect = item;
                //  getReturn();
                // SaveItemInHand(item);
                break;
            case Item.ItemType.Gatling:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.GatlingSprite;
                itemForSave = item;
                //  itemConnect = item;
                //  getReturn();
                //  SaveItemInHand(item);
                break;
            case Item.ItemType.FlameThrower:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.FlameThrowerSprite;
                itemForSave = item;
                //   itemConnect = item;
                //  getReturn();
                //  SaveItemInHand(item);
                break;
            case Item.ItemType.HuntingRifle:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.HuntingRifleSprite;
                itemForSave = item;
             //   itemConnect = item;
            //    getReturn();
            //    SaveItemInHand(item);
                break;
            case Item.ItemType.Watcher:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.WatcherSprite;
                itemForSave = item;
                //   itemConnect = item;
                //  getReturn();
                //  SaveItemInHand(item);
                break;
            case Item.ItemType.Dagon:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.DagonSprite;
                itemForSave = item;
                //  itemConnect = item;
                //  getReturn();
                //SaveItemInHand(item);
                break;
            case Item.ItemType.HeatLaser:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.HeatLaserSprite;
                itemForSave = item;
                //  itemConnect = item;
                //     getReturn();
                //SaveItemInHand(item);
                break;
            case Item.ItemType.LaserCharge:
                ItemInHand = true;
                GunUIinInventory.enabled = true;
                DequipButton.onClick.RemoveAllListeners();
                DequipButton.onClick.AddListener(() => GetItemFromDequip(item));
                DequipButton.enabled = true;
                GunUIinInventory.sprite = ItemAssets.Instance.LaserChargeSprite;
                itemForSave = item;
                //   itemConnect = item;
                //    getReturn();
                //SaveItemInHand(item);
                break;
            case Item.ItemType.LowHpPotion:
                return;
            case Item.ItemType.LargeHpPotion:
                return;
            case Item.ItemType.PistolAmmo:
                return;
            case Item.ItemType.ShotgunAmmo:
                return;
            case Item.ItemType.AutomaticAmmo:
                return;
            case Item.ItemType.GrenadeAmmo:
                return;
            case Item.ItemType.BatteryAmmo:
                return;
            case Item.ItemType.SniperAmmo:
                return;
        }
        
    }
    void ConsumeItem(Item item)
    {
        switch (item.type)
        {
            case Item.ItemType.Firefly:
                return;
            case Item.ItemType.Pistol:
                return;
            case Item.ItemType.LaserBeam:
                return;
            case Item.ItemType.GrenadeLauncher:
                return;
            case Item.ItemType.ShotGun:
                return;
            case Item.ItemType.Handler:
                return;
            case Item.ItemType.HK106:
                return;
            case Item.ItemType.DesertEagle:
                return;
            case Item.ItemType.SilentKing:
                return;
            case Item.ItemType.Watcher:
                return;
            case Item.ItemType.HuntingRifle:
                return;
            case Item.ItemType.Dagon:
                return;
            case Item.ItemType.Eliminator:
                return;
            case Item.ItemType.ClassicOne:
                return;
            case Item.ItemType.Gatling:
                return;
            case Item.ItemType.FlameThrower:
                return;
            case Item.ItemType.HeatLaser:
                return;
            case Item.ItemType.LaserCharge:
                return;


            case Item.ItemType.LowHpPotion:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.LargeHpPotion:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.PistolAmmo:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.ShotgunAmmo:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.AutomaticAmmo:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.GrenadeAmmo:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.BatteryAmmo:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.SniperAmmo:
                player.UseItem(item);
                inventory.RemoveItem(item);
                break;
        }
    }
   void GetItemFromDequip(Item item)
    {
        
            DequipButton.enabled = false;
            DequipButton.onClick.RemoveAllListeners();
            ItemInHand = false;
            player.UseItem(item);
            GunUIinInventory.enabled = false;
            inventory.AddItem(item);
            Debug.Log("Recive Item");
        
       
        //string saveFilePath = Application.persistentDataPath + "/itemInHand.json";
      //  if (File.Exists(saveFilePath))
     //   {
     //       File.Delete(saveFilePath);
     //   }
    }

    public void SaveItemInHand(Item item)
    {
        string savePath = Application.persistentDataPath + "/itemInHand.json";
        string json = JsonUtility.ToJson(new ItemInHandData(item));
        File.WriteAllText(savePath, json);
    }
  

}

[Serializable]
public class ItemInHandData
{
    public Item itemInHand;

    public ItemInHandData(Item itemInHand)
    {
        this.itemInHand = itemInHand;
    }
}
