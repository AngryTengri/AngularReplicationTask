using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional, if you want the UIManager to persist across scenes.
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartFadeIn()
    {
        FadeOut fadeOutScript = FindObjectOfType<FadeOut>();
        if (fadeOutScript != null)
        {
            StartCoroutine(fadeOutScript.FadeIn());
            Debug.Log("FADEOUT");
        }
        else
        {
            Debug.LogError("No FadeOut script found in the scene!");
        }
    }
}
