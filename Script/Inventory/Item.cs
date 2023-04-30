using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    
    public enum ItemType
    {
        ShotGun,
        Pistol,
        Firefly,
        GrenadeLauncher,
        LaserBeam,
        
    }
    public ItemType type;
    public int amount;
    public string itemName;
    public string description;
    public Image itemImage;

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.ShotGun: return 20;
            case ItemType.Pistol: return 0;
            case ItemType.Firefly: return 50;
            case ItemType.GrenadeLauncher: return 100;
            case ItemType.LaserBeam: return 200;
        }
    }
    public Sprite GetSprite()
    {
        switch (type)
        {
            default:
            case ItemType.ShotGun: return ItemAssets.Instance.ShotGunSprite;
            case ItemType.Pistol: return ItemAssets.Instance.PistolSprite;
            case ItemType.Firefly: return ItemAssets.Instance.FireFlySprite;
            case ItemType.GrenadeLauncher: return ItemAssets.Instance.GrenadeLauncherSprite;
            case ItemType.LaserBeam: return ItemAssets.Instance.LaserBeam;
        }
    }
  
    public string GetName()
    {
        switch (type)
        {
            default:
            case ItemType.ShotGun: return ItemAssets.Instance.ShotGunName;
            case ItemType.Pistol: return ItemAssets.Instance.PistolName;
            case ItemType.Firefly: return ItemAssets.Instance.FireFlyName;
            case ItemType.GrenadeLauncher: return ItemAssets.Instance.GrenadeLName;
            case ItemType.LaserBeam: return ItemAssets.Instance.LaserBeamName;

        }
    }
   // public Image GetImage()
   // {
  //      switch (type)
  //      {
  //          default:
    //        case ItemType.ShotGun: return ItemAssets.Instance.ShotunSprite;
  //          case ItemType.Pistol: return ItemAssets.Instance.PistolImage;
   //         case ItemType.Firefly: return ItemAssets.Instance.FireflyImage;
   //         case ItemType.GrenadeLauncher: return ItemAssets.Instance.GrenadeLImage;
  //      }
  //  }
   // public string GetDes()
  //  {
   //     switch (type)
   //     {
   //         default:
   //         case ItemType.ShotGun: return ItemAssets.Instance.ShotGunDes;
   //         case ItemType.Pistol: return ItemAssets.Instance.PistolDes;
    //        case ItemType.Firefly: return ItemAssets.Instance.FireFlyDes;
    //        case ItemType.GrenadeLauncher: return ItemAssets.Instance.GrenadeLDes;

   //     }
  //  }
    public bool IsStackable()
    {
        switch (type)
        {
            default:
            case ItemType.ShotGun:
                return false;
            case ItemType.Pistol:
                return false;
            case ItemType.Firefly:
                return false;
            case ItemType.GrenadeLauncher:
                return false;
        }
    }

   
}
