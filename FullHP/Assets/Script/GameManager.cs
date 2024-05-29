using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
using System.IO;
public class GameManager : MonoBehaviour
{
    public GameObject SlingShot;
    public GameObject FlashBang;
    public GameObject Trapmine;
    public GameObject Ember;

    public Transform SkillCastPoint;
    public Image Q;
    public Image E;
    public Image R;
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI warningSkill;
    GameObject player;

    public bool Pigman;
    public bool Eagle;
    public bool Milady;
    public bool Nathan;

    public bool PigmanDefeated;
    public bool EagleDefeated;
    public bool MiladyDefeated;
    public bool NathanDefeated;
    public bool BatholoDead;

    public GameObject SmokeScreenEffect;

    public float skillCoolDownR;
    public float skillCoolDownE;
    public float skillCoolDownQ;

    public Slider skillCooldownCounterQ;
    public Slider skillCooldownCounterE;
    public Slider skillCooldownCounterR;

    PlayerSkills.SkillType Qskill;
    PlayerSkills.SkillType Eskill;
    PlayerSkills.SkillType Rskill;
    //  public SpriteRenderer PlayerSprite;
    private void Awake()
    {
        player = GameObject.Find("Player");
       
        Q.enabled = false;
        E.enabled = false;
        R.enabled = false;
        Instance = this;
       // LoadBossAccess();
    }
  

    private Dictionary<KeyCode, PlayerSkills.SkillType> skillKeyMappings = new Dictionary<KeyCode, PlayerSkills.SkillType>();
    IEnumerator ClearWarn()
    {
        warningSkill.text = "Replaced Skill Slot!";
        yield return new WaitForSeconds(1);
        warningSkill.text = "";
    }
    public void WarnedSkillPoint()
    {
        StartCoroutine(WarnedNoSkillPoint());
    }
    public IEnumerator WarnedNoSkillPoint()
    {
        warningSkill.text = "You have no skill point.";
        yield return new WaitForSeconds(2);
        warningSkill.text = "";
    }
    // Method to assign a key code to a skill
    public void AssignKeyCode(KeyCode keyCode, PlayerSkills.SkillType skill)
    {
        if (skillKeyMappings.ContainsValue(skill))
        {
           
            KeyCode existingKeyCode = skillKeyMappings.FirstOrDefault(x => x.Value == skill).Key;
            skillKeyMappings.Remove(existingKeyCode);
        }
        
        if (skillKeyMappings.ContainsKey(keyCode) && skillKeyMappings[keyCode] != skill)
        {
           // warningSkill.text = "Replaced Skill Slot!";
            skillKeyMappings.Remove(keyCode);
            StartCoroutine(ClearWarn());
        }
        if(keyCode == KeyCode.Q)
        {
            Q.enabled = true;
            Q.sprite = GetSkillIcon(skill);
            GetSkillSaveQ(skill);

         
        }
        if(keyCode == KeyCode.E)
        {
            E.enabled = true;
            E.sprite = GetSkillIcon(skill);
            GetSkillSaveE(skill);

        }
        if (keyCode == KeyCode.R)
        {
            R.enabled = true;
            R.sprite = GetSkillIcon(skill);
            GetSkillSaveR(skill);
        }
        skillKeyMappings[keyCode] = skill;

    }
    public PlayerSkills.SkillType GetSkillSaveQ(PlayerSkills.SkillType Q)
    {
        Qskill = Q;
        return Qskill;
    }
    public PlayerSkills.SkillType GetSkillSaveE(PlayerSkills.SkillType E)
    {
        Eskill = E;
        return Eskill;
    }
    public PlayerSkills.SkillType GetSkillSaveR(PlayerSkills.SkillType R)
    {
        Rskill = R;
        return Rskill;
    }
   public void Save()
    {
        if(Qskill != PlayerSkills.SkillType.None)
        {
            string savePath = Application.persistentDataPath + "/assignSkillDataQ.json";
            string json = JsonUtility.ToJson(new SkillAssignDataQ(KeyCode.Q, Qskill));
            File.WriteAllText(savePath, json);
        }
        if (Eskill != PlayerSkills.SkillType.None)
        {
            string savePathE = Application.persistentDataPath + "/assignSkillDataE.json";
            string jsonE = JsonUtility.ToJson(new SkillAssignDataE(KeyCode.E, Eskill));
            File.WriteAllText(savePathE, jsonE);
        }
        if (Rskill != PlayerSkills.SkillType.None)
        {
            string savePathR = Application.persistentDataPath + "/assignSkillDataR.json";
            string jsonR = JsonUtility.ToJson(new SkillAssignDataR(KeyCode.R, Rskill));
            File.WriteAllText(savePathR, jsonR);
        }
    }
  
    public void LoadAssign()
    {
        Debug.Log("LoadAssign");
;        string savePathQ = Application.persistentDataPath + "/assignSkillDataQ.json";
        if (File.Exists(savePathQ))
        {
            string json = File.ReadAllText(savePathQ);
            SkillAssignDataQ QData = JsonUtility.FromJson<SkillAssignDataQ>(json);
            Q.enabled = true;
            AssignKeyCode(KeyCode.Q, QData.qSkill);
            Q.sprite = GetSkillIcon(QData.qSkill);
            
           

            //skillKeyMappings[KeyCode.Q] = QData.qSkill;
        }
        string savePathE = Application.persistentDataPath + "/assignSkillDataE.json";
        if (File.Exists(savePathE))
        {
            string json = File.ReadAllText(savePathE);
            SkillAssignDataE EData = JsonUtility.FromJson<SkillAssignDataE>(json);
            E.enabled = true;
            AssignKeyCode(KeyCode.E, EData.eSkill);
            E.sprite = GetSkillIcon(EData.eSkill);

            // skillKeyMappings[KeyCode.E] = EData.eSkill;
        }
        
        string savePathR = Application.persistentDataPath + "/assignSkillDataR.json";
        if (File.Exists(savePathR))
        {
            string json = File.ReadAllText(savePathR);
            SkillAssignDataR RData = JsonUtility.FromJson<SkillAssignDataR>(json);
            R.enabled = true;
            AssignKeyCode(KeyCode.R, RData.rSkill);
            R.sprite = GetSkillIcon(RData.rSkill);

            //  skillKeyMappings[KeyCode.R] = RData.rSkill;
        }
    }
    public void RemoveKeyCodeAssignment(PlayerSkills.SkillType skill)
    {
        KeyCode keyCode = skillKeyMappings.FirstOrDefault(x => x.Value == skill).Key;

  
        if (keyCode != KeyCode.None)
        {
            skillKeyMappings.Remove(keyCode);
        }
        if (keyCode == KeyCode.Q)
        {
            Q.enabled = false;
        }
        if (keyCode == KeyCode.E)
        {
            E.enabled = false;

        }
        if (keyCode == KeyCode.R)
        {
            R.enabled = false;

        }
    }
    public void DeleteAssign()
    {
        string savePathQ = Application.persistentDataPath + "/assignSkillDataQ.json";
        string savePathE = Application.persistentDataPath + "/assignSkillDataE.json";
        string savePathR = Application.persistentDataPath + "/assignSkillDataR.json";
        if (File.Exists(savePathQ))
        {
            File.Delete(savePathQ);
        }
        if (File.Exists(savePathE))
        {
            File.Delete(savePathE);
        }
        if (File.Exists(savePathR))
        {
            File.Delete(savePathR);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (!SaveManager.Instance.isLoading)
        {
            if (!DeathEffectInteract.Instance.Slowed)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                Time.timeScale = 0.3f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

            }
            skillCoolDownQ -= Time.deltaTime;
            skillCoolDownE -= Time.deltaTime;
            skillCoolDownR -= Time.deltaTime;
            skillCooldownCounterQ.value = skillCoolDownQ;
            skillCooldownCounterE.value = skillCoolDownE;
            skillCooldownCounterR.value = skillCoolDownR;

            foreach (KeyCode keyCode in skillKeyMappings.Keys)
            {

                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode == KeyCode.Q && skillCoolDownQ <= 0)
                    {
                        PlayerSkills.SkillType skill = skillKeyMappings[keyCode];
                        skillCoolDownQ = GetSkillMaxCoolDown(skill);
                        skillCooldownCounterQ.maxValue = GetSkillMaxCoolDown(skill);
                        UseSkill(skill);
                    }
                    else if (keyCode == KeyCode.E && skillCoolDownE <= 0)
                    {
                        PlayerSkills.SkillType skill = skillKeyMappings[keyCode];
                        skillCoolDownE = GetSkillMaxCoolDown(skill);
                        skillCooldownCounterE.maxValue = GetSkillMaxCoolDown(skill);
                        UseSkill(skill);
                    }
                    else if (keyCode == KeyCode.R && skillCoolDownR <= 0)
                    {
                        PlayerSkills.SkillType skill = skillKeyMappings[keyCode];
                        GetSkillMaxCoolDown(skill);
                        skillCoolDownR = GetSkillMaxCoolDown(skill);
                        skillCooldownCounterR.maxValue = GetSkillMaxCoolDown(skill);
                        UseSkill(skill);
                    }



                    // Call the UseSkill method with the associated skill

                }
            }
        }
          
    }

    float GetSkillMaxCoolDown(PlayerSkills.SkillType skill)
    {
        switch (skill)
        {
            default:
            case PlayerSkills.SkillType.Ember: return 15;
            case PlayerSkills.SkillType.TrapMine: return 15;
            case PlayerSkills.SkillType.GodLight: return 15;
            case PlayerSkills.SkillType.TakeAim: return 20;
            case PlayerSkills.SkillType.Camoflage: return 15;
            case PlayerSkills.SkillType.SlingShotArrow: return 2;
        }
    }
    Sprite GetSkillIcon(PlayerSkills.SkillType skill)
    {
        switch (skill)
        {
            default:
            case PlayerSkills.SkillType.Ember: return SkillIcon.Instance.Ember;
            case PlayerSkills.SkillType.TrapMine: return SkillIcon.Instance.TrapMine;
            case PlayerSkills.SkillType.GodLight: return SkillIcon.Instance.GodLight;
            case PlayerSkills.SkillType.TakeAim: return SkillIcon.Instance.TakeAim;
            case PlayerSkills.SkillType.Camoflage: return SkillIcon.Instance.Camoflage;
            case PlayerSkills.SkillType.SlingShotArrow: return SkillIcon.Instance.SlingShot;
        }
            
    }
    public void SaveBossAccess()
    {
        string savePath = Application.persistentDataPath + "/bossAccessData.json";
        string json = JsonUtility.ToJson(new BossAccessData(Pigman, Eagle, Milady, Nathan, PigmanDefeated, EagleDefeated, MiladyDefeated, NathanDefeated, BatholoDead));
        File.WriteAllText(savePath, json);
        Save();
     //   string savePathSkill = Application.persistentDataPath + "/skillAssignData.json";
    }
    public void DeleteBossAccess()
    {
        string savePath = Application.persistentDataPath + "/bossAccessData.json";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        DeleteAssign();
    }
    public void LoadBossAccess()
    {
        LoadAssign();
        string savePath = Application.persistentDataPath + "/bossAccessData.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            BossAccessData bossData = JsonUtility.FromJson<BossAccessData>(json);
            Pigman = bossData.Pigman;
            Eagle = bossData.Eagle;
            Milady = bossData.Milady;
            Nathan = bossData.Nathan;

            PigmanDefeated = bossData.PigmanDefeated;
            EagleDefeated = bossData.EagleDefeated;
            MiladyDefeated = bossData.MiladyDefeated;
            NathanDefeated = bossData.NathanDefeated;
            BatholoDead = bossData.BatholoDead;
        }
        
    }
    void UseSkill( PlayerSkills.SkillType skill)
    {

        switch (skill)
        {
            case PlayerSkills.SkillType.Ember:
                Debug.Log("Fire");
                Vector3 mousePosEmber = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 ThrowDirectionEmber = (mousePosEmber - player.transform.position).normalized;
                GameObject EmberObject = Instantiate(Ember, SkillCastPoint.position, Quaternion.identity);
                Rigidbody2D EmberRB = EmberObject.GetComponent<Rigidbody2D>();
                EmberRB.velocity = ThrowDirectionEmber.normalized * 5f;
                Destroy(EmberObject, EmberObject.GetComponent<Ember>().TimeCount + 0.2f);


                break;
            case PlayerSkills.SkillType.TrapMine:
                Vector3 position = player.transform.position;
                GameObject trapping = Instantiate(Trapmine, position, Quaternion.identity);
                Destroy(trapping, 15);
                Debug.Log("Mine Set!");
                SoundManager.Instance.TrapSound();
                break;
            case PlayerSkills.SkillType.GodLight:
                Vector3 mousePosGL = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 ThrowDirectionGL = (mousePosGL - player.transform.position).normalized;
                GameObject FlashBangObject = Instantiate(FlashBang, SkillCastPoint.position, Quaternion.identity);
                Rigidbody2D FlashBangRB = FlashBangObject.GetComponent<Rigidbody2D>();
                FlashBangRB.velocity = ThrowDirectionGL.normalized * 10f;

                Destroy(FlashBangObject, 2);

                Debug.Log("Flashbang!");
                break;
            case PlayerSkills.SkillType.TakeAim:
                player.GetComponent<Player>().Aim();
                Debug.Log("Aiming!");
                break;
            case PlayerSkills.SkillType.Camoflage:
                Debug.Log("Silently!");
                Player playerScript = player.GetComponent<Player>();

                if (!playerScript.stealth)
                {
                    Instantiate(SmokeScreenEffect, playerScript.gameObject.transform.position, Quaternion.identity);
                    StartCoroutine(playerScript.Camoflaging());
                    SoundManager.Instance.CamoflagingSound();
                }
                break;
            case PlayerSkills.SkillType.SlingShotArrow:
                Vector3 mousePosSSA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 ThrowDirectionSSA = (mousePosSSA - player.transform.position).normalized;
                GameObject SlingShotObject = Instantiate(SlingShot, SkillCastPoint.position, Quaternion.identity);
                Rigidbody2D SlingShotRB = SlingShotObject.GetComponent<Rigidbody2D>();
                SlingShotRB.velocity = ThrowDirectionSSA.normalized * 10;
                Debug.Log("Whoop!");
                break;

        }

    }
  
  
}
[Serializable]
public class BossAccessData
{
    public bool Pigman;
    public bool Eagle;
    public bool Milady;
    public bool Nathan;

    public bool PigmanDefeated;
    public bool EagleDefeated;
    public bool MiladyDefeated;
    public bool NathanDefeated;

    public bool BatholoDead;

    public BossAccessData(bool Pigman, bool Eagle, bool Milady, bool Nathan, bool PigmanDefeated, bool EagleDefeated, bool MiladyDefeated, bool NathanDefeated, bool BatholoDead)
    {
        this.Pigman = Pigman;
        this.Eagle = Eagle;
        this.Milady = Milady;
        this.Nathan = Nathan;

        this.PigmanDefeated = PigmanDefeated;
        this.EagleDefeated = EagleDefeated;
        this.MiladyDefeated = MiladyDefeated;
        this.NathanDefeated = NathanDefeated;
        this.BatholoDead = BatholoDead;
    }
}
[Serializable]
public class SkillAssignDataQ
{

    public KeyCode keyQ;
   
    public PlayerSkills.SkillType qSkill;

    public SkillAssignDataQ(KeyCode keyQ,PlayerSkills.SkillType qSkill)
    {
        this.keyQ = keyQ;
      
        this.qSkill = qSkill;

    }
}
[Serializable]
public class SkillAssignDataE
{

    public KeyCode keyE;
    public PlayerSkills.SkillType eSkill;
    public SkillAssignDataE(KeyCode keyE,  PlayerSkills.SkillType eSkill)
    {
     this.keyE = keyE;
 
        this.eSkill = eSkill;

    }
}
[Serializable]
public class SkillAssignDataR
{

    public KeyCode keyR;
    public PlayerSkills.SkillType rSkill;
    public SkillAssignDataR(KeyCode keyR,  PlayerSkills.SkillType rSkill)
    {
        this.keyR = keyR;

        this.rSkill = rSkill;

    }
}

