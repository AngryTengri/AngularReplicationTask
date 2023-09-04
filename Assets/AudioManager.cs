using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioObject; // Assign the GameObject you wish to activate in the inspector

    private static bool isFirstLoad = true; // This static variable will remember its value across scenes

    private void Start()
    {
        // Check if it's the first time the game has started
        if (isFirstLoad)
        {
            isFirstLoad = false; // Mark that the game has started at least once

            if (audioObject != null)
            {
                audioObject.SetActive(true);
            }
        }

    }
}
