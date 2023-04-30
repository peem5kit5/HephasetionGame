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

    public Sprite FireFlySprite;
    public Sprite PistolSprite;
    public Sprite ShotGunSprite;
    public Sprite GrenadeLauncherSprite;
    public Sprite LaserBeam;

    public Image FireflyImage;
    public Image PistolImage;
    public Image ShotgunImage;
    public Image GrenadeLImage;
    public Image LaserBeamImage;

    public string FireFlyName;
    public string PistolName;
    public string ShotGunName;
    public string GrenadeLName;
    public string LaserBeamName;
    
}
