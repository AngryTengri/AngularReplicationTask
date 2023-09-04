using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public GameObject audioObject; // Assign the GameObject you wish to activate in the inspector

    private static bool isFirstLoad = true; // This static variable will remember its value across scenes

    private void Start()
    {
        // Check if it's the first time this scene has been loaded
        if (isFirstLoad)
        {
            isFirstLoad = false; // Mark that the scene has been loaded at least once

            if (audioObject != null)
            {
                audioObject.SetActive(true);
            }
        }
        else
        {
            if (audioObject != null)
            {
                audioObject.SetActive(false);
            }
        }
    }

    // Reset the isFirstLoad flag when the scene changes
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != gameObject.scene.name) // Check if the loaded scene is different from the current scene
        {
            isFirstLoad = true;
        }
    }
}
