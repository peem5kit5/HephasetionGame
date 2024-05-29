using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance { get; private set; }
    public TextMeshProUGUI levelText;

    [SerializeField]
    private Slider xpBar;

    private int level = 0;
    public int exp = 0;
    public int expToNextLevel = 80;
    private int expMod;
    public int skillPoint = 0;
    public TextMeshProUGUI skillPointText;
    void Awake()
    {
        if(this != null)
        {
            Instance = this;
        }
        level = 0;
        exp = 0;
        skillPoint = 0;
        expToNextLevel = 100;
       
    }
    private void Update()
    {

        skillPointText.text = $"{skillPoint}";
     // xpBar.value = exp;
     
        
    }
    public void AddExp(int amount)
    {
        exp += amount;

        if (exp >= expToNextLevel)
        {
            level++;
            skillPoint += 1;
            expToNextLevel += 20;
            exp = 0;
        }
        SetXP(exp);
        UpdateXPText();
     //   Save();
    }
    
    public int GetLevelNumber()
    {
        return level;
    }
    public void SetXP(float xp)
    {
        xpBar.maxValue = expToNextLevel;
        xpBar.value = xp;
    }
    private void UpdateXPText()
    {
        levelText.text = level.ToString();
    }
    public void Save()
    {
        string savePath = Application.persistentDataPath + "/expData.json";
        string json = JsonUtility.ToJson(new LevelData(level, exp, expToNextLevel, expMod, skillPoint));
        File.WriteAllText(savePath, json);

    }
    public void Load()
    {
        string savePath = Application.persistentDataPath + "/expData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            LevelData saveData = JsonUtility.FromJson<LevelData>(json);
            level = saveData.level;
            exp = saveData.exp;
            expToNextLevel = saveData.expToNextLevel;
            expMod = saveData.expMod;
            skillPoint = saveData.skillPoint;
            SetXP(exp);
            UpdateXPText();
            Debug.Log(savePath);
        }
    }
    public void Delete()
    {
        string savePath = Application.persistentDataPath + "/expData.json";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }

}
[Serializable]
public class LevelData
{
    public int level;
    public int exp;
    public int expToNextLevel;
    public int expMod;
    public int skillPoint;

    public LevelData(int level, int exp, int expToNextLevel,int expMod, int skillPoint)
    {
        this.level = level;
        this.exp = exp;
        this.expToNextLevel = expToNextLevel;
        this.expMod = expMod;
        this.skillPoint = skillPoint;
    }
}
