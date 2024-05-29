using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject Game;
    public GameObject VideoSetting;
    public GameObject AudioSetting;
    private AudioSource trackBG;
    public AudioSource effectButton;
    public AudioClip bgTrack;
    public AudioClip buttonEffect;
    public AudioMixer mixer;
    public GameObject Credit;

    public void SetVolumeMain(float amount)
    {
        mixer.SetFloat("Main", amount);
    }

    public void SetVolumeSFX(float amount)
    {
        mixer.SetFloat("SFX", amount);
    }
    public void SetVolumeMusic(float amount)
    {
        mixer.SetFloat("Music", amount);
    }
    private void Start()
    {
        trackBG = GetComponent<AudioSource>();
        trackBG.clip = bgTrack;
        if(effectButton != null)
        effectButton.clip = buttonEffect;
    }
    public void CreditButton()
    {
        Credit.SetActive(true);
    }

    public void HideCredit()
    {
        Credit.SetActive(false);
    }
    public void NewGame()
    {


        SceneManager.LoadScene("VideoIntro");
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
    }
    public void Go_MainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
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
    }

    List<int> widths = new List<int> { 1920, 1280, 960, 568 };
    List<int> heights = new List<int> { 1080, 800, 540, 329 };

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);

    }
    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

}
