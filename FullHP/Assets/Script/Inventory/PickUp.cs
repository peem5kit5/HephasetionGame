using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
   // private string itemDescription;
    [SerializeField] private UI_Inventory uiInventory;

    private void Start()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);

    }
    private void UseItem(Item item)
    {

        switch (item.type)
        {
            case Item.ItemType.ShotGun:
                //����繢ͧ���������������������
                //inventory.RemoveItem(new Item {type = Item.ItemType.����, amount =1});
                break;
            case Item.ItemType.Pistol:
                break;
            case Item.ItemType.Firefly:
                break;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.gameObject.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}
