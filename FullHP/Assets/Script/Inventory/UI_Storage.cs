using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_Storage : MonoBehaviour
{
    public Transform ScrollArea;
    public Transform Scroll;
    public Transform Container;
    public Transform itemSlotContain;
    public Transform itemSlotTemplate;

    private Storage storage;
    private Player player;
    private Item itemConnect;
    Inventory inventory;

    public TextMeshProUGUI itemCounter;
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
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
    public void SetInventory(Storage storage)
    {
        this.storage = storage;


        storage.OnItemListChanged_Storage += Storage_OnItemListChanged;

        RefreshStorageItem();
    }
    private void Storage_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshStorageItem();
    }
    private void RefreshStorageItem()
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
        float itemSlotCelly = 15f;
        foreach (Item item in storage.GetItemList())
        {
            RectTransform itemSlotRect = Instantiate(itemSlotTemplate, itemSlotContain).GetComponent<RectTransform>();
            itemSlotRect.gameObject.SetActive(true);

            itemSlotRect.GetComponent<Button_UI>().ClickFunc = () =>
            {

                storage.TransferToInventory(item, player.inventory);
        
              //  ItemWorld.SpawnItemWorld(player.transform.position, item);
              //  inventory.AddItem(item);
               // storage.RemoveItem(item);
                //storage.TransferToInventory(item, player.inventory);
                Debug.Log("Left Click");

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

        itemCounter.text = $"{storage.itemListStorage.Count} / {storage.maxCapacity}";

    }
    private Item getReturn()
    {
        return itemConnect;
    }
}
