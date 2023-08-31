using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.Samples;

public class MenuHide : MonoBehaviour
{
    public GameObject menuObject; // The GameObject that you want to toggle on and off
    public float time = 5f;  // The amount of time to wait before deactivating the menu at start

    private AudioSource[] audioSources;

    void Start()
    {
        // Get all AudioSource components in the scene
        audioSources = FindObjectsOfType<AudioSource>();
        //StartCoroutine(DeactivateMenuAfterTime());
    }

    void Update()
    {
        if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.A) || Input.GetKeyDown(KeyCode.E))
        {
            menuObject.SetActive(!menuObject.activeSelf);

            if (menuObject.activeSelf)
            {
                Time.timeScale = 0;
                PauseAudio();
            }
            else
            {
                Time.timeScale = 1;
                UnPauseAudio();
            }
        }
    }

    private void PauseAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    private void UnPauseAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
    }

    IEnumerator DeactivateMenuAfterTime()
    {
        yield return new WaitForSeconds(time);

        if (menuObject != null)
        {
            menuObject.SetActive(false);
        }
    }
}
