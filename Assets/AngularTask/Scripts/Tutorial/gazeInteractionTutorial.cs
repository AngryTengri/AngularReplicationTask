using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeInteractionTutorial : MonoBehaviour
{
    const float SPEED = 6f;
    public GameObject nextMark;
    public GameObject lastMark;
    [SerializeField] Transform SectionInfo;
    [SerializeField] AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] AudioSource instructionsAudioSource; // Additional AudioSource for instructions
    [SerializeField] AudioClip gazeSound; // Audio clip to be played
    [SerializeField] AudioClip instructionsSound; // Audio clip for instructions
    public bool Stay; // Control this from the inspector

    Vector3 desiredScale = Vector3.zero;

    // Create a bool to ensure that the instructions sound only plays once.
    private bool hasInstructionsPlayed = false;
    private bool hasGazeSoundPlayed = false;

    void Update()
    {
            SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
      
    }

    public void OpenInfo()
    {
        desiredScale = Vector3.one;
        PlayGazeSound(); // Play the sound when the info is opened

        // At the start of the scene, play the instructions sound
        if (!hasInstructionsPlayed)
        {
            PlayInstructionsSound();
            hasInstructionsPlayed = true;

            if (nextMark != null)
            {
                nextMark.SetActive(true);
            }

            if (lastMark != null)
            {
                lastMark.SetActive(false);
            }
        }
    }

    public void CloseInfo()
    {
        if (!Stay)
        {
            desiredScale = Vector3.zero;
        }
    }

    void PlayGazeSound()
    {
        if (!hasGazeSoundPlayed && audioSource != null && gazeSound != null)
        {
            audioSource.clip = gazeSound;
            audioSource.Play();
            hasGazeSoundPlayed = true;
        }
    }

    void PlayInstructionsSound()
    {
        if (instructionsAudioSource != null && instructionsSound != null && !instructionsAudioSource.isPlaying)
        {
            instructionsAudioSource.clip = instructionsSound; // Assign the instructions audio clip to the AudioSource component
            instructionsAudioSource.Play();
        }
    }
}
