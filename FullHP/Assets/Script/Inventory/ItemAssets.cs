using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    public Transform prefabItemWorld;
    //Gun

    public Sprite FireFlySprite;
    public Sprite PistolSprite;
    public Sprite ShotGunSprite;
    public Sprite GrenadeLauncherSprite;
    public Sprite LaserBeam;
    public Sprite HandlerSprite;
    public Sprite HK106Sprite;
    public Sprite DesertEagleSprite;
    public Sprite SilentKingSprite;
    public Sprite GatlingSprite;
    public Sprite FlameThrowerSprite;
    public Sprite DagonSprite;
    public Sprite HuntingRifleSprite;
    public Sprite WatcherSprite;
    public Sprite LaserChargeSprite;
    public Sprite HeatLaserSprite;
    public Sprite EliminatorSprite;
    public Sprite ClassicOneSprite;

    //Item Consume
    public Sprite LargeHpPotion;
    public Sprite MediumHpPotion;
    public Sprite LowHpPotion;
    public Sprite PistolAmmo;
    public Sprite BatteryAmmo;
    public Sprite AutomaticAmmo;
    public Sprite GrenadeAmmo;
    public Sprite ShotgunAmmo;
    public Sprite SniperAmmo;





    public Image FireflyImage;
    public Image PistolImage;
    public Image ShotgunImage;
    public Image GrenadeLImage;
    public Image LaserBeamImage;
    public Image LargeHpPotionImage;
    public Image MediumHpPotionImage;
    public Image LowHpPotionImage;
    public Image PistolAmmoImage;
    public Image BatteryAmmoImage;
    public Image AutomaticAmmoImage;
    public Image GrenadeAmmoImage;
    public Image ShotgunAmmoImage;
    public Image SniperAmmoImage;

    public Image EliminatorImage;
    public Image ClassicOneImage;

    public Image HandlerImage;
    public Image HK106Image;
    public Image DesertEagleImage;
    public Image SilentKingImage;
    public Image GatlingImage;
    public Image FlameThrowerImage;
    public Image DagonImage;
    public Image HuntingRifleImage;
    public Image WatcherImage;
    public Image LaserChargeImage;
    public Image HeatLaserImage;


    public string FireFlyName;
    public string PistolName;
    public string ShotGunName;
    public string GrenadeLName;
    public string LaserBeamName;
    public string LargeHpPotionName;
    public string MediumHpPotionName;
    public string LowHpPotionName;
    public string PistolAmmoName;
    public string BatteryAmmoName;
    public string AutomaticAmmoName;
    public string GrenadeAmmoName;
    public string ShotgunAmmoName;
    public string SniperAmmoName;

    public string HandlerName;
    public string HK106Name;
    public string DesertEagleName;
    public string SilentKingName;
    public string GatlingName;
    public string FlameThrowerName;
    public string DagonName;
    public string HuntingRifleName;
    public string WatcherName;
    public string LaserChargeName;
    public string HeatLaserName;

    public string EliminatorName;
    public string ClassicOneName;
}
