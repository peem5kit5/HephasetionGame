using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MapSound : MonoBehaviour
{
    public static MapSound Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip originalSound;
    public AudioClip newSound;
    public AudioMixer mixer;
    public float transitionDuration = 2.0f; // Duration of the volume transition in seconds

    private bool isTransitioning = false;

    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        // Ensure you have an AudioSource component attached to the GameObject.
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // If there's no AudioSource component, create one.
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        // Play the original sound
        PlayOriginalSound();
    }

    public void PlayOriginalSound()
    {
        audioSource.clip = originalSound;
        audioSource.Play();
    }

    public void PlayNewSound()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToNewSound());
        }
    }

    private IEnumerator TransitionToNewSound()
    {
        isTransitioning = true;

        // Gradually reduce the volume to 0 over the transition duration
        float startVolume = audioSource.volume;
        float startTime = Time.time;
        while (Time.time - startTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        // Ensure the volume is set to 0 at the end of the transition
        audioSource.volume = 0f;

        // Play the new sound
        audioSource.clip = newSound;
        audioSource.Play();

        // Gradually increase the volume of the new sound over the same duration
        startTime = Time.time;
        while (Time.time - startTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            audioSource.volume = Mathf.Lerp(0f, startVolume, t);
            yield return null;
        }

        // Ensure the volume is set to the original volume at the end of the transition
        audioSource.volume = startVolume;

        isTransitioning = false;
    }
    public void PlayOriginalSoundWithTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToOriginalSound());
        }
    }

    private IEnumerator TransitionToOriginalSound()
    {
        isTransitioning = true;

        // Gradually reduce the volume to 0 over the transition duration
        float startVolume = audioSource.volume;
        float startTime = Time.time;
        while (Time.time - startTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        // Ensure the volume is set to 0 at the end of the transition
        audioSource.volume = 0f;

        // Play the original sound
        audioSource.clip = originalSound;
        audioSource.Play();

        // Gradually increase the volume of the original sound over the same duration
        startTime = Time.time;
        while (Time.time - startTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            audioSource.volume = Mathf.Lerp(0f, startVolume, t);
            yield return null;
        }

        // Ensure the volume is set to the original volume at the end of the transition
        audioSource.volume = startVolume;

        isTransitioning = false;
    }
}
