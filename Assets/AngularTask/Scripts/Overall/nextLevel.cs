using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wave.OpenXR.Toolkit.Samples;

public class nextLevel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Listen for "1" key press
        {
            LoadSceneByIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Listen for "2" key press
        {
            LoadSceneByIndex(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Listen for "3" key press
        {
            LoadSceneByIndex(2);
        }

        if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.B) || Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(0);
        }
    }

    // Function to load a scene by its index
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadCurrentScene()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}
