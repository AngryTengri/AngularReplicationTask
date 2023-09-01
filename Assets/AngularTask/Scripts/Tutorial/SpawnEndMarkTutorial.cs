using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.Samples;

public class SpawnEndMarkTutorial : MonoBehaviour
{
    public GameObject endMarker;
    public GameObject nextAngle;
    public GameObject arrow;
    public float delay = 2f;  // Delay between allowed button presses
    public float checkTime = 6f; // Time within which the button should be pressed again
    public float delaySpawn = 3f; // Time to wait after FirstCenter becomes active before canSpawn is set to true
    public bool canSpawn = false; // Determines if the endMarker can be spawned or not

    private GameObject currentEndMarker;  // A reference to the currently spawned endMarker
    private Camera mainCamera;  // A private field to hold the reference to the main camera
    private bool buttonIsInCooldown = false; // Whether the button is currently in cooldown
    private bool buttonPressedAgain = false; // Whether the button was pressed again within checkTime seconds
    private GameObject firstCenter; // FirstCenter child object

    // Use the Start method to initialize the mainCamera and firstCenter fields
    void Start()
    {
        mainCamera = Camera.main;  // Find the main camera and store the reference
        firstCenter = transform.Find("FirstCenter").gameObject; // Find the FirstCenter child object and store the reference
    }

    void Update()
    {
        if (firstCenter.activeInHierarchy && !canSpawn)
        {
            StartCoroutine(DelayCanSpawn());
        }

        if (canSpawn && !buttonIsInCooldown && (VRSInputManager.instance.GetButtonDown(VRSButtonReference.TriggerL) || Input.GetButtonDown("Jump")))
        {
            buttonPressedAgain = true;  // Indicate that the button was pressed
            if (mainCamera != null)
            {
                // If there is already an endMarker instance, destroy it
                if (currentEndMarker != null)
                {
                    Destroy(currentEndMarker);
                }

                // Get the y rotation of the mainCamera
                Vector3 cameraRotation = mainCamera.transform.rotation.eulerAngles;

                // Instantiate the endMarker at the center of the GameObject that this script is attached to
                // with the mainCamera's y rotation, and assign the new instance to currentEndMarker
                currentEndMarker = Instantiate(endMarker, this.transform.position, Quaternion.Euler(0, cameraRotation.y, 0));

                // Start the cooldown and check routines
                StartCoroutine(ButtonCooldown());
                StartCoroutine(CheckButtonPress());
            }
        }
    }

    IEnumerator ButtonCooldown()
    {
        buttonIsInCooldown = true;  // Set the flag to indicate the button is in cooldown
        yield return new WaitForSeconds(delay);  // Wait for delay seconds
        buttonIsInCooldown = false;  // Reset the flag
    }

    IEnumerator CheckButtonPress()
    {
        buttonPressedAgain = false;  // Reset the flag
        yield return new WaitForSeconds(checkTime);  // Wait for checkTime seconds
        if (!buttonPressedAgain && nextAngle != null)  // If the button was not pressed again and nextAngle is not null
        {
            nextAngle.SetActive(true);  // Set nextAngle to active
            arrow.SetActive(true);

            // Deactivate the current GameObject
            gameObject.SetActive(false);
        }
    }

    IEnumerator DelayCanSpawn()
    {
        yield return new WaitForSeconds(delaySpawn);
        canSpawn = true;
    }
}