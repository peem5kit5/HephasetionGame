using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class Storage : MonoBehaviour
{
    public List<Item> itemListStorage = new List<Item>();

    public event EventHandler OnItemListChanged_Storage;
    private Action<Item> StoreItemAction;

    private Action<Item> removeItemAction;
    public int maxCapacity = 30;
    public Storage(Action<Item> StoreItemAction)
    {
        itemListStorage = new List<Item>();
        this.StoreItemAction = StoreItemAction;
        Debug.Log("Inventory");
        Debug.Log(itemListStorage.Count);
        maxCapacity = 30;
        //Load();
    }
    void Start()
    {
        //Load();
    }
    public void AddItem(Item item)
    {
        if (itemListStorage.Count > maxCapacity)
        {
            throw new InvalidOperationException("Storage Full!");
        }

        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemListStorage)
            {
                if (inventoryItem.type == item.type)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemListStorage.Add(item);

            }
        }
        else
        {
            itemListStorage.Add(item);
            Debug.Log("Storage Add" + item);

        }
      //  Debug.Log("Storage Add" + item);
        Save();
        OnItemListChanged_Storage?.Invoke(this, EventArgs.Empty);

    }
    public void RemoveItem(Item item)
    {

        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemListStorage)
            {
                if (inventoryItem.type == item.type)
                {

                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemListStorage.Remove(itemInInventory);
               


            }
        }
        else
        {

           
            itemListStorage.Remove(item);
        }
        Save();
        OnItemListChanged_Storage?.Invoke(this, EventArgs.Empty);
        // Save();
    }
    public void TransferToInventory(Item item, Inventory inv)
    {
        inv.AddItem(item);
        RemoveItem(item);
        //Save();
       // OnItemListChanged_Storage?.Invoke(this, EventArgs.Empty);
    }
    public void TransferItem(Item item)
    {
        StoreItemAction(item);
    }
    public List<Item> GetItemList()
    {
        return itemListStorage;
    }
    public void Save()
    {
        string savePath = Application.persistentDataPath + "/storeData.json";
        string json = JsonUtility.ToJson(new StorageData(itemListStorage));
        File.WriteAllText(savePath, json);
    }
    public void DeleteSave()
    {
        string savePath = Application.persistentDataPath + "/storeData.json";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
    public void Load()
    {
        string savePath = Application.persistentDataPath + "/storeData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            StorageData saveData = JsonUtility.FromJson<StorageData>(json);
            itemListStorage = saveData.itemList;
            Debug.Log(savePath);
        }
        OnItemListChanged_Storage?.Invoke(this, EventArgs.Empty);
    }
}

[Serializable]
public class StorageData
{
    public List<Item> itemList;

    public StorageData(List<Item> itemList)
    {
        this.itemList = itemList;
    }
}
