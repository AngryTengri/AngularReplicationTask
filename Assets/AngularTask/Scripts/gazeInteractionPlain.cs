using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeInteractionPlain : MonoBehaviour
{
    const float SPEED = 6f;
    public GameObject nextMark;
    public GameObject lastMark;
    [SerializeField] Transform SectionInfo;
    [SerializeField] AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] AudioClip gazeSound; // Audio clip to be played

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
            hasInstructionsPlayed = true;

            if (nextMark != null)
            {
                nextMark.SetActive(true);
            }

            if (lastMark != null)
            {
                //Debug.Log("Setting lastMark inactive");
                lastMark.SetActive(false);
                //Debug.Log("lastMark activeSelf: " + lastMark.activeSelf);
            }

        }
    }

    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }

    void PlayGazeSound()
    {
        // If the sound has not been played and audioSource and gazeSound are not null, play the sound
        if (!hasGazeSoundPlayed && audioSource != null && gazeSound != null)
        {
            audioSource.clip = gazeSound; // Assign the audio clip to the AudioSource component
            audioSource.Play();

            // Set hasGazeSoundPlayed to true so the sound is not played again
            hasGazeSoundPlayed = true;
        }
    }
}
