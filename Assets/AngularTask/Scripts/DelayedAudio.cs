using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    public float delay = 1f; // Delay in seconds
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the game object!");
        }
        else
        {
            // Play the audio after the specified delay
            Invoke("PlayDelayedAudio", delay);
        }
    }

    private void PlayDelayedAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
