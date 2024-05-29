using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    Player player;
    Inventory inv;
    Storage storage;
    UI_Inventory uiInv;
    //   ToggleUI uiToggle;
    // UI_Skilltree skills;
    public Button continueButton;
    public bool isLoading;
    private void Awake()
    {
        continueButton.enabled = false;
        continueButton.interactable = false;
        player = FindObjectOfType<Player>();
        inv = FindObjectOfType<Inventory>();
        storage = FindObjectOfType<Storage>();
        uiInv = FindObjectOfType<UI_Inventory>();
        //  uiToggle = FindObjectOfType<ToggleUI>();
        string savePath = Application.persistentDataPath + "/posData.json";
        if (File.Exists(savePath))
        {
            continueButton.enabled = true;
            continueButton.interactable = true;
        }
        //    uiSkill = FindObjectOfType<UI_Skilltree>();
        Instance = this;

        DontDestroyOnLoad(this);

    }
    public void SaveAll()
    {
        //  uiToggle = FindObjectOfType<ToggleUI>();

        player = FindObjectOfType<Player>();
        inv = FindObjectOfType<Inventory>();
        storage = FindObjectOfType<Storage>();
        uiInv = FindObjectOfType<UI_Inventory>();
        uiInv.SaveItemInHand(uiInv.itemForSave);
        player.Save();
        inv.Save();
        storage.Save();
        player.skill.Save();
        GameManager.Instance.SaveBossAccess();
        QuestManager.Instance.Save();
        
        //    uiToggle.SkillToggle();
        //   skills = FindObjectOfType<UI_Skilltree>();
        LevelSystem.Instance.Save();
        player.inventory.Save();
        player.storage.Save();
        player.skill.Save();
        
        //  uiToggle.IntoPlay();


    }
    public IEnumerator LoadAll()
    {
        isLoading = true;
        yield return new WaitForSeconds(0.01f);

        //uiToggle = FindObjectOfType<ToggleUI>();
        uiInv = FindObjectOfType<UI_Inventory>();
        uiInv.LoadItemInHand();
        player = FindObjectOfType<Player>();
        player.Load();
        player.skill.Load();
        inv = FindObjectOfType<Inventory>();
        inv.Load();
        GameManager.Instance.LoadBossAccess();
        QuestManager.Instance.Load();
        LevelSystem.Instance.Load();
        storage = FindObjectOfType<Storage>();
        storage.Load();
        player.inventory.Load();
        player.storage.Load();
        //  uiToggle.SkillToggle();
        //   skills = FindObjectOfType<UI_Skilltree>();
        player.skill.Load();
        yield return new WaitForSeconds(1f);
        isLoading = false;
    }
    public void LoadGame()
    {
        LoadToScene();
     //   StartCoroutine(LoadAll());
    }
    public void NewGame()
    {

        SceneManager.LoadScene("Bar");
        StartCoroutine(DeletedAll());
    }

    public IEnumerator DeletedAll()
    {
        isLoading = true;
        yield return new WaitForSeconds(0.01f);
        // uiToggle = FindObjectOfType<ToggleUI>();
        player = FindObjectOfType<Player>();
        player.DeleteSave();


        inv = FindObjectOfType<Inventory>();
        inv.DeleteSave();

        storage = FindObjectOfType<Storage>();
        storage.DeleteSave();



        uiInv = FindObjectOfType<UI_Inventory>();
        uiInv.DeleteSave();

        Debug.Log("New Game!");
        GameManager.Instance.DeleteBossAccess();
        QuestManager.Instance.DeleteSave();
        LevelSystem.Instance.Delete();
        //uiToggle.SkillToggle();
        //   skills = FindObjectOfType<UI_Skilltree>();
        player.inventory.DeleteSave();
        player.storage.DeleteSave();
        player.skill.DeleteSave();
        DeletedPosSave();
        isLoading = false;
        //  uiToggle.IntoPlay();
    }
    public void LoadToScene()
    {
        StartCoroutine(LoadToTheScene());
    }
   
    public IEnumerator LoadToTheScene()
    {
        string savePath = Application.persistentDataPath + "/posData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerPositionData saveData = JsonUtility.FromJson<PlayerPositionData>(json);
            SceneManager.LoadScene(saveData.sceneName);
            yield return new WaitForSeconds(0.01f);
            player = FindObjectOfType<Player>();
            player.transform.position = new Vector3(saveData.x, saveData.y, saveData.z);
            StartCoroutine(LoadAll());
            // SaveAll();
            // LoadAll();


        }
    }
  
    public void Bar_Slum()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Slum", 4.1f, 10.25f, 0.04735798f));
        File.WriteAllText(savePath, json);


    }

    public void OutSide_Pigman()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("SlumBattleZone", 129.3f, -65.71f, 0));
        File.WriteAllText(savePath, json);
    }
    public void Battle_Slum()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Slum", -40.14f, 30.94f, 0));
        File.WriteAllText(savePath, json);


    }
    public void Slum_Battle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("SlumBattleZone", -32.34f, 28.76f, 0));
        File.WriteAllText(savePath, json);


    }

    public void Slum_Bar()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Bar", -3.68f, -3.6f, 0));
        File.WriteAllText(savePath, json);

    }
    public void MiladyBattle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";
        string json = JsonUtility.ToJson(new PlayerPositionData("Milady_Battle", 239.83f, -20.07f, 0));
        File.WriteAllText(savePath, json);
    }
    public void City_Battle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("SlumBattleZone", 263.17f, 13.58f, 0));
        File.WriteAllText(savePath, json);

    }
    public void Desert_Battle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("SlumBattleZone", 218.3f, -230.8f, 0));
        File.WriteAllText(savePath, json);

    }

    public void PigmanBattle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Pigman_Battle", 0f, -3.43f, 0));
        File.WriteAllText(savePath, json);
    }
    public void EagleBattle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";
        string json = JsonUtility.ToJson(new PlayerPositionData("Fox_Battle", -34.33f, 18.64f, 0));
        File.WriteAllText(savePath, json);
    }
    public void Battle_Desert()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Desert", -4.84f, 2.23f, 0));
        File.WriteAllText(savePath, json);
    }
    public void Outside_Eagle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Desert", 146.82f, -42.8f, 0));
        File.WriteAllText(savePath, json);
    }
    public void Battle_Tanium()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Tanium", 253.81f, -25.28f, 0));
        File.WriteAllText(savePath, json);
    }
    public void Outside_Milady()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Tanium", 318.53f, 22.4f, 0));
        File.WriteAllText(savePath, json);
    }
    public void NathanBattle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("Nathan_Battle", 0f, -2.27f, 0));
        File.WriteAllText(savePath, json);
    }
    public void Tanium_Battle()
    {
        string savePath = Application.persistentDataPath + "/posData.json";

        string json = JsonUtility.ToJson(new PlayerPositionData("SlumBattleZone", 263.17f, 9.82f, 0));
        File.WriteAllText(savePath, json);
    }

    private void Update()
    {
        DontDestroyOnLoad(this);
    }

    void DeletedPosSave()
    {
        string savePath = Application.persistentDataPath + "/posData.json";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
}
[Serializable]
public class PlayerPositionData
{
    public string sceneName;
    public float x;
    public float y;
    public float z;

    public PlayerPositionData(string sceneName, float x, float y, float z)
    {
        this.sceneName = sceneName;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

