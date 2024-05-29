using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    
    public enum ItemType
    {
        None,

        //Gun
        ShotGun,
        Pistol,
        Firefly,
        GrenadeLauncher,
        LaserBeam,
        Handler,
        HK106,
        DesertEagle,
        SilentKing,
        Gatling,
        FlameThrower,
        Dagon,
        HuntingRifle,
        Watcher,
        Eliminator,
        ClassicOne,
        HeatLaser,
        LaserCharge,

        //Item Consume
        LargeHpPotion,
        MediumHpPotion,
        LowHpPotion,
        PistolAmmo,
        AutomaticAmmo,
        ShotgunAmmo,
        GrenadeAmmo,
        BatteryAmmo,
        SniperAmmo
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
            case ItemType.None: return 0;
            case ItemType.ShotGun: return 20;
            case ItemType.Pistol: return 0;
            case ItemType.Firefly: return 150;
            case ItemType.GrenadeLauncher: return 1000;
            case ItemType.LaserBeam: return 1000;
            case ItemType.LargeHpPotion: return 100;
            case ItemType.LowHpPotion: return 25;
            case ItemType.PistolAmmo: return 0;
            case ItemType.AutomaticAmmo: return 90;
            case ItemType.ShotgunAmmo: return 90;
            case ItemType.GrenadeAmmo: return 200;
            case ItemType.BatteryAmmo: return 300;
            case ItemType.SniperAmmo: return 250;

            case ItemType.Handler: return 300;
            case ItemType.HK106: return 600;
            case ItemType.DesertEagle: return 700;
            case ItemType.SilentKing: return 650;
            case ItemType.Gatling: return 4000;
            case ItemType.FlameThrower: return 1000;
            case ItemType.Dagon: return 700;
            case ItemType.HuntingRifle : return 500;
            case ItemType.Watcher: return 1000;
            case ItemType.Eliminator: return 500;
            case ItemType.ClassicOne: return 750;
            case ItemType.HeatLaser: return 1500;
            case ItemType.LaserCharge: return 1500;

        }
    }
    public Sprite GetSprite()
    {
        switch (type)
        {
            default:
            case ItemType.None: return null;
            case ItemType.ShotGun: return ItemAssets.Instance.ShotGunSprite;
            case ItemType.Pistol: return ItemAssets.Instance.PistolSprite;
            case ItemType.Firefly: return ItemAssets.Instance.FireFlySprite;
            case ItemType.GrenadeLauncher: return ItemAssets.Instance.GrenadeLauncherSprite;
            case ItemType.LaserBeam: return ItemAssets.Instance.LaserBeam;
            case ItemType.LargeHpPotion: return ItemAssets.Instance.LargeHpPotion;
            case ItemType.LowHpPotion: return ItemAssets.Instance.LowHpPotion;
            case ItemType.PistolAmmo: return ItemAssets.Instance.PistolAmmo;
            case ItemType.AutomaticAmmo: return ItemAssets.Instance.AutomaticAmmo;
            case ItemType.ShotgunAmmo: return ItemAssets.Instance.ShotgunAmmo;
            case ItemType.GrenadeAmmo: return ItemAssets.Instance.GrenadeAmmo;
            case ItemType.BatteryAmmo: return ItemAssets.Instance.BatteryAmmo;
            case ItemType.SniperAmmo: return ItemAssets.Instance.SniperAmmo;

            case ItemType.Handler: return ItemAssets.Instance.HandlerSprite;
            case ItemType.HK106: return ItemAssets.Instance.HK106Sprite;
            case ItemType.DesertEagle: return ItemAssets.Instance.DesertEagleSprite;
            case ItemType.SilentKing: return ItemAssets.Instance.SilentKingSprite;
            case ItemType.Gatling: return ItemAssets.Instance.GatlingSprite;
            case ItemType.FlameThrower: return ItemAssets.Instance.FlameThrowerSprite;
            case ItemType.Dagon: return ItemAssets.Instance.DagonSprite;
            case ItemType.HuntingRifle: return ItemAssets.Instance.HuntingRifleSprite;
            case ItemType.Watcher: return ItemAssets.Instance.WatcherSprite;
            case ItemType.Eliminator: return ItemAssets.Instance.EliminatorSprite;
            case ItemType.ClassicOne: return ItemAssets.Instance.ClassicOneSprite;
            case ItemType.HeatLaser: return ItemAssets.Instance.HeatLaserSprite;
            case ItemType.LaserCharge: return ItemAssets.Instance.LaserChargeSprite;
        }
    }
  
    public string GetName()
    {
        switch (type)
        {
            default:
            case ItemType.None: return null;
            case ItemType.ShotGun: return ItemAssets.Instance.ShotGunName;
            case ItemType.Pistol: return ItemAssets.Instance.PistolName;
            case ItemType.Firefly: return ItemAssets.Instance.FireFlyName;
            case ItemType.GrenadeLauncher: return ItemAssets.Instance.GrenadeLName;
            case ItemType.LaserBeam: return ItemAssets.Instance.LaserBeamName;
            case ItemType.LargeHpPotion: return ItemAssets.Instance.LargeHpPotionName;
            case ItemType.LowHpPotion: return ItemAssets.Instance.LowHpPotionName;
            case ItemType.PistolAmmo: return ItemAssets.Instance.PistolAmmoName;
            case ItemType.AutomaticAmmo: return ItemAssets.Instance.AutomaticAmmoName;
            case ItemType.ShotgunAmmo: return ItemAssets.Instance.ShotgunAmmoName;
            case ItemType.GrenadeAmmo: return ItemAssets.Instance.GrenadeAmmoName;
            case ItemType.BatteryAmmo: return ItemAssets.Instance.BatteryAmmoName;
            case ItemType.SniperAmmo: return ItemAssets.Instance.SniperAmmoName;

            case ItemType.Handler: return ItemAssets.Instance.HandlerName;
            case ItemType.HK106: return ItemAssets.Instance.HK106Name;
            case ItemType.DesertEagle: return ItemAssets.Instance.DesertEagleName;
            case ItemType.SilentKing: return ItemAssets.Instance.SilentKingName;
            case ItemType.Gatling: return ItemAssets.Instance.GatlingName;
            case ItemType.FlameThrower: return ItemAssets.Instance.FlameThrowerName;
            case ItemType.Dagon: return ItemAssets.Instance.DagonName;
            case ItemType.HuntingRifle: return ItemAssets.Instance.HuntingRifleName;
            case ItemType.Watcher: return ItemAssets.Instance.WatcherName;
            case ItemType.Eliminator: return ItemAssets.Instance.EliminatorName;
            case ItemType.ClassicOne: return ItemAssets.Instance.ClassicOneName;
            case ItemType.HeatLaser: return ItemAssets.Instance.HeatLaserName;
            case ItemType.LaserCharge: return ItemAssets.Instance.LaserChargeName;

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
            case ItemType.None: 
                return false;
            case ItemType.ShotGun:
                return false;
            case ItemType.Pistol:
                return false;
            case ItemType.Firefly:
                return false;
            case ItemType.GrenadeLauncher:
                return false;
            case ItemType.LaserBeam:
                return false;
            case ItemType.LargeHpPotion:
                return false;
            case ItemType.LowHpPotion:
                return false;
            case ItemType.PistolAmmo:
                return false;
            case ItemType.AutomaticAmmo:
                return false;
            case ItemType.ShotgunAmmo:
                return false;
            case ItemType.BatteryAmmo:
                return false;
            case ItemType.GrenadeAmmo:
                return false;
            case ItemType.SniperAmmo:
                return false;
            case ItemType.Handler: return false;
            case ItemType.HK106: return false;
            case ItemType.DesertEagle: return false;
            case ItemType.SilentKing: return false;
            case ItemType.Gatling: return false;
            case ItemType.FlameThrower: return false;
            case ItemType.Dagon: return false;
            case ItemType.HuntingRifle: return false;
            case ItemType.Watcher: return false;
            case ItemType.Eliminator: return false;
            case ItemType.ClassicOne: return false;
            case ItemType.HeatLaser: return false;
            case ItemType.LaserCharge: return false;
        }
    }

   
}
