using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    [SerializeField]
    private Slider xpBar;

    private int level;
    public int exp;
    public int expToNextLevel;
    private int expMod;
    void Start()
    {
        level = 0;
        exp = 0;
        expToNextLevel = 100;
    }
    private void Update()
    {
       
      
     // xpBar.value = exp;
     
        
    }
    public void AddExp(int amount)
    {
        exp += amount;

        if (exp >= expToNextLevel)
        {
            level++;
            expToNextLevel += expMod;
            exp -= expToNextLevel;
            UpdateXPText();
        }
    }
        //  SetXP(exp);
    
    public int GetLevelNumber()
    {
        return level;
    }
    public void SetXP(float xp)
    {
     //   xpBar.maxValue = expToNextLevel;
    //    xpBar.value = xp;
    }
    private void UpdateXPText()
    {
        levelText.text = level.ToString();
    }
}
