using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using Newtonsoft.Json;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;
    private Action<Item> removeItemAction;
    public Inventory(Action<Item> useItemAction)
    {
        itemList = new List<Item>();
        this.useItemAction = useItemAction;
        Debug.Log("Inventory");
        Debug.Log(itemList.Count);
        
        Load();
    }
    void Start()
    {

    }
    public void AddItem(Item item)
    {
        
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.type == item.type)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
              
            }
        }
        else
        {
            itemList.Add(item);
           
        }
        Save();
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        
    }
    public void RemoveItem(Item item)
    {
       
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.type == item.type)
                {

                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
                
                    
            }
        }
        else
        {
           
            itemList.Remove(item);
        }
        Save();
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
       // Save();
    }
    public void UseItem(Item item)
    {
        useItemAction(item);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
    public void Save()
    {
        string savePath = Application.persistentDataPath + "/invData.json";
        string json = JsonUtility.ToJson(new InventoryData(itemList));
        File.WriteAllText(savePath, json);
    }
    public void Load()
    {
        string savePath = Application.persistentDataPath + "/invData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            InventoryData saveData = JsonUtility.FromJson<InventoryData>(json);
            itemList = saveData.itemList;
            Debug.Log(savePath);
        }
    }


    //  public void Save()
    //  {
    //      InventorySave.SaveInventory(itemList);
    //  }
    //public void Load()
    // {
    //     if (InventorySave.HasInventory())
    //     {
    //         itemList = InventorySave.LoadInventory();
    ////     }
    //    else
    //     {
    //         Save();
    //     }
    //  }
}

[Serializable]
public class InventoryData
{
    public List<Item> itemList;
    
    public InventoryData(List<Item> itemList)
    {
        this.itemList = itemList;
    }
     
}




