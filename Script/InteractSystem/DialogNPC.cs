using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogNPC : MonoBehaviour
{
    public bool dialogueIsPlaying { get; private set;}
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool chatting = false;
    public bool playerinRange;
    public bool typing;
    //public GameObject questUI;
    public bool canQuest;

    public GameObject chat;
    public GameObject continueButton;


    private void Start()
    {
        
        
    }
    private void Awake()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            playerinRange = true;
            
            
            Debug.Log("Chat");

           
        }
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        playerinRange = false;
       
    }


    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F) && playerinRange)
        {
            chat.SetActive(true);
            if(typing == false)
            {
                StartCoroutine(Type());
                typing = true;
            }
            
            
        }
        else if (!playerinRange)
        {
            chat.SetActive(false);
            typing = false;
        }
        if(typing == true)
        {
            
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
        }
       
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }
    public void NextSentence()
    {
        continueButton.SetActive(false);
        if(index < sentences.Length - 1 && !canQuest)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else 
        {

            textDisplay.text = "";
            chatting = false;
            typing = false;
            chat.SetActive(false);

        }
    }
}
