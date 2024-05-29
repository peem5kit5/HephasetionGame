using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Boss_Nathan : MonoBehaviour
{
    public GameObject puppets;

    public Transform puppetPlace;
    public int puppetsCount = 0;
    public int maxCount;
    public Animator JumpScareAnim;
    public GameObject Gasing;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public TextMeshProUGUI Question;
    public GameObject AskingUI;
    ToggleUI toggleUI;
    public int nathanHP;
    bool jump;
    public float maxCooldownAsking;
    public float currentCooldownAsking;
    public GameObject smokeNathan;
    public Transform Center;
    bool spawned;
    bool defeated;
    float maxSceneDead;
    float currentSceneDead;
    public bool isAsking;

    AudioSource audioSound;
    public AudioClip jumpScare;
    public AudioClip GasSound;
    void Start()
    {
        toggleUI = FindObjectOfType<ToggleUI>();
        nathanHP = 10;
        audioSound = GetComponent<AudioSource>();
    }
    IEnumerator Ending()
    {
        GameManager.Instance.NathanDefeated = true;

        yield return new WaitForSeconds(3.5f);

        //GameManager.Instance.Pigman = false;
        //Instantiate(deathEffect, this.transform.position, Quaternion.identity);
        Destroy(gameObject, 0.01f);

        SaveManager.Instance.SaveAll();
        SaveManager.Instance.Bar_Slum();
        SceneManager.LoadScene("Ending");
       

    }
    // Update is called once per frame
    void Update()
    {
        Logic();
        if (nathanHP <= 0)
        {
            
            NathanMimic[] gameObjectNathan = FindObjectsOfType<NathanMimic>();
            foreach(NathanMimic nathan in gameObjectNathan)
            {
                if(nathan != null)
                {
                    Destroy(nathan.gameObject);
                }
            }
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
            maxSceneDead = 1.5f;
            currentSceneDead -= Time.deltaTime;
            if (currentSceneDead <= 0)
            {

                DeathEffectInteract.Instance.KilledEffect();
                currentSceneDead = maxSceneDead;
            }
            List<Quest> questing = new List<Quest>();
            foreach(Quest quest in QuestManager.Instance.activeQuests)
            {
                if(quest.goal.goalType == GoalType.BatholoQuest12)
                {
                    questing.Add(quest);
                }

            }
            foreach(Quest quest in questing)
            {
                QuestManager.Instance.FinishQuests(quest);
            }
           // linerenderer.enabled = false;
            if (!defeated)
            {
                StartCoroutine(Ending());
                defeated = true;
            }
        }
    }
    IEnumerator StartSpawn()
    {
        while(puppetsCount < maxCount)
        {
            SpawnPuppet();
            yield return new WaitForSeconds(1);
          
        }
    }
    void Logic()
    {
        if (!GameManager.Instance.NathanDefeated)
        {
            if (puppetsCount <= 0)
            {
                spawned = true;
                if (spawned)
                {
                    StartCoroutine(StartSpawn());
                }

            }
            else if (puppetsCount > 0)
            {
                spawned = true;
            }
            if (jump)
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
                currentCooldownAsking -= Time.deltaTime;
                if (currentCooldownAsking <= 0)
                {
                    JumpScare();
                    toggleUI.isLayout = false;
                    jump = false;
                }
            }
        }
        
    }
 
    void SpawnPuppet()
    {
        if (!defeated)
        {
            int i = Random.Range(0, 3);

            Instantiate(puppets, puppetPlace.position, Quaternion.identity);
            Instantiate(smokeNathan, puppetPlace.position, Quaternion.identity);
            puppetsCount++;
        }
           
        
    }
    public void MinusCount()
    {
        puppetsCount--;
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            Gas();
        }
        else if(random == 1)
        {
            
            Asking();
            
          
        }
        else if(random == 2)
        {
            Gas();
        }
        

    }
    public void JumpScare()
    {
        JumpScareAnim.SetTrigger("JumpScare");
        audioSound.PlayOneShot(jumpScare);
        AskingUI.SetActive(false);
        toggleUI.isLayout = true;
        DamagePlayer();
        jump = false;
        currentCooldownAsking = maxCooldownAsking;
        isAsking = false;
    }
    public void Asking()
    {
        isAsking = true;
        AskingUI.SetActive(true);
        currentCooldownAsking = maxCooldownAsking;
        jump = true;
        toggleUI.isLayout = false;
        int randomRange = Random.Range(0, 9);
        if(randomRange == 0)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "I Have Billions Of Eyes, Yet Live In Darkness. I Have Millions Of Ears, Yet Only Four Lobes. I Have No Muscle, Yet Rule Two Hemispheres. What Am I?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Brain";
            button1.onClick.RemoveAllListeners();
            button1.onClick.AddListener(() => DamageNathan());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Ears";
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(() => JumpScare());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Eyes";
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "God";
            button4.onClick.RemoveAllListeners();
            button4.onClick.AddListener(() => JumpScare());

        }
        else if(randomRange == 1)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "You Can Be Cracked. I Can Be Made. I Can Be Told. I Can Be Played. What Are You?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Mordekai";
            
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Egg";
           
            button2.onClick.AddListener(() => JumpScare());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Joke";
         
            button3.onClick.AddListener(() => DamageNathan());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Mordred";
           
            button4.onClick.AddListener(() => JumpScare());
        }
        else if (randomRange == 2)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "What Does A Liar Do When He's Dead?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "...";
            button1.onClick.RemoveAllListeners();
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Dead.";
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(() => JumpScare());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Cry still.";
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Lies still.";
            button4.onClick.RemoveAllListeners();
            button4.onClick.AddListener(() => DamageNathan());
        }
        else if (randomRange == 3)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "Sometimes You Are Pain, Sometimes You Are Perfect. What Are You?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Hatred.";
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Love.";
            button2.onClick.AddListener(() => DamageNathan());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Perfection.";
            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Brave.";
            button4.onClick.AddListener(() => JumpScare());
        }
        else if (randomRange == 4)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "What Is This Game 2D Artist Name?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Phapwarot.";
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Phobwarat.";
            button2.onClick.AddListener(() => DamageNathan());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Popwarat.";
            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Hotaka.";
            button4.onClick.AddListener(() => JumpScare());
        }
        else if (randomRange == 5)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "What Is This Game Game Design Name?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "KornAlong";
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Alongkarn";
  
            button2.onClick.AddListener(() => JumpScare());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Rachata";

            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Alongkorn";
     
            button4.onClick.AddListener(() => DamageNathan());
        }
        else if (randomRange == 6)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "You Are Me After, But I'm Not You Before, What Am I?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Me.";
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Present.";
            button2.onClick.AddListener(() => DamageNathan());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Future.";
            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Will.";
            button4.onClick.AddListener(() => JumpScare());
        }
        else if (randomRange == 7)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "If I'm A Mistake Even I'm At My Best , What Am I?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Villain.";
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Criminal.";
            button2.onClick.AddListener(() => JumpScare());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "War.";
            button3.onClick.AddListener(() => DamageNathan());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Tanium";
            button4.onClick.AddListener(() => JumpScare());
        }
        else if (randomRange == 8)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "Are You Going To Save 1 Girl Over Billion Peoples?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "No";
            button2.onClick.AddListener(() => DamageNathan());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Maybe";
            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "...";
            button4.onClick.AddListener(() => JumpScare());
        }
        else if (randomRange == 9)
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
            Question.text = "Who Are You?";
            button1.GetComponentInChildren<TextMeshProUGUI>().text = "Vengeance.";
            button1.onClick.AddListener(() => JumpScare());

            button2.GetComponentInChildren<TextMeshProUGUI>().text = "Savior.";
            button2.onClick.AddListener(() => DamageNathan());

            button3.GetComponentInChildren<TextMeshProUGUI>().text = "Your Nightmare.";
            button3.onClick.AddListener(() => JumpScare());

            button4.GetComponentInChildren<TextMeshProUGUI>().text = "Just Overprotective Brother.";
            button4.onClick.AddListener(() => DamageNathan());
        }
      
     


    }
  

    public void Gas()
    {
        audioSound.PlayOneShot(GasSound);
        Player player = FindObjectOfType<Player>();
        GameObject obj = Instantiate(Gasing, player.transform.position, Quaternion.identity);
        Destroy(obj, 3);
        StartCoroutine(CooldownAsking());
    }
    IEnumerator CooldownAsking()
    {
        isAsking = true;
        yield return new WaitForSeconds(3);
        isAsking = false;
    }
    public void DamagePlayer()
    {
        toggleUI.isLayout = true;
        PlayerHP hp = FindObjectOfType<PlayerHP>();
        hp.TakeDamage(20);
        isAsking = false;
    }
    public void DamageNathan()
    {
        DeathEffectInteract.Instance.KilledEffect();
        nathanHP--;
        toggleUI.isLayout = true;
        AskingUI.SetActive(false);
        currentCooldownAsking = maxCooldownAsking;
        jump = false;
        isAsking = false;
    }
}
