using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillIcon : MonoBehaviour
{
    public static SkillIcon Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public Sprite CQC;
    public Sprite DoorSweeper;
    public Sprite TacticalStance;
    public Sprite WindLanguage;
    public Sprite Vengeance;
    public Sprite Ember;
    public Sprite TrapMine;
    public Sprite GodLight;
    public Sprite TakeDown;
    public Sprite TakeAim;
    public Sprite Camoflage;
    public Sprite SlingShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
