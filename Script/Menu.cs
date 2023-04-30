using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject Game;
    public GameObject VideoSetting;
    public GameObject AudioSetting;
    public GameObject ControlSetting;
    private AudioSource trackBG;
    public AudioSource effectButton;
    public AudioClip bgTrack;
    public AudioClip buttonEffect;
    private void Start()
    {
        trackBG = GetComponent<AudioSource>();
        trackBG.clip = bgTrack;
        trackBG.Play();
        effectButton.clip = buttonEffect;
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Demo");
    }

    public void optionToggle()
    {
        effectButton.PlayOneShot(buttonEffect);
        DeactivatedAll();
        optionPanel.SetActive(!optionPanel.activeInHierarchy);
        Game.SetActive(!Game.activeInHierarchy);
    }
    public void AudioSet()
    {
        effectButton.PlayOneShot(buttonEffect);
        DeactivatedAll();
        AudioSetting.SetActive(true);
    }
    public void VideoSet()
    {
        effectButton.PlayOneShot(buttonEffect);
        DeactivatedAll();
        VideoSetting.SetActive(true);
    }
    public void ControlSet()
    {
        effectButton.PlayOneShot(buttonEffect);
        DeactivatedAll();
        ControlSetting.SetActive(true);
    }
    public void Exit()
    {
        effectButton.PlayOneShot(buttonEffect);
        Application.Quit();
    }

    void DeactivatedAll()
    {
        effectButton.PlayOneShot(buttonEffect);
        VideoSetting.SetActive(false);
        AudioSetting.SetActive(false);
        ControlSetting.SetActive(false);
    }


}
