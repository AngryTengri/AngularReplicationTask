using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.Samples;

public class SpawnEndMark : MonoBehaviour
{
    public GameObject endMarker;
    public GameObject prevAngle;
    public GameObject nextAngle;
    public GameObject arrow;
    public float delay = 2f;  // Delay between allowed button presses
    public float checkTime = 6f; // Time within which the button should be pressed again
    private GameObject currentEndMarker;  // A reference to the currently spawned endMarker
    private Camera mainCamera;  // A private field to hold the reference to the main camera
    private bool buttonIsInCooldown = false; // Whether the button is currently in cooldown
    private bool buttonPressedAgain = false; // Whether the button was pressed again within checkTime seconds
    private bool isSpaceButtonDown = false;
    private RotationLogger rotationLogger;  // Reference to the RotationLogger script
    public float yRotation; // Value to be passed to the rotation logger

    // Use the Start method to initialize the mainCamera field
    void Start()
    {
        mainCamera = Camera.main;  // Find the main camera and store the reference

        // Find the GameObject with the RotationLogger script attached
        GameObject rotationLoggerObject = GameObject.Find("RotationLoggerManager");

        // If the GameObject was found, get the RotationLogger script from it
        if (rotationLoggerObject != null)
        {
            rotationLogger = rotationLoggerObject.GetComponent<RotationLogger>();
        }
        else
        {
            Debug.LogError("No GameObject named 'RotationLoggerManager' was found. Make sure you have a GameObject with this name and the RotationLogger script attached in your scene.");
        }
    }

    void Update()
    {
        bool isVrTriggerDown = VRSInputManager.instance.GetButtonDown(VRSButtonReference.TriggerL);
        bool isJumpButtonDown = Input.GetButtonDown("Jump");

        // Check if the space button was released
        if (Input.GetButtonUp("Jump"))
        {
            isSpaceButtonDown = false;
        }

        if (!buttonIsInCooldown && (isVrTriggerDown || (isJumpButtonDown && !isSpaceButtonDown)))
        {
            Debug.Log("Trigger button pressed");
            isSpaceButtonDown = true;  // Set the flag if space button was just pressed

            // Log the rotation ONLY if the CSV file exists
            if (rotationLogger != null && rotationLogger.DoesCsvExist())
            {
                rotationLogger.LogRotation("MarkerSpawned", mainCamera.transform.rotation.eulerAngles);
            }

            buttonPressedAgain = true;  // Indicate that the button was pressed
            if (mainCamera != null)
            {
                // If there is already an endMarker instance, destroy it
                if (currentEndMarker != null)
                {
                    Debug.Log("Previous endMarker instance found. Destroying it.");
                    Destroy(currentEndMarker);
                }

                // Get the y rotation of the mainCamera
                Vector3 cameraRotation = mainCamera.transform.rotation.eulerAngles;
                Debug.Log("2 NEW Main camera rotation retrieved: " + cameraRotation.y);

                // Instantiate the endMarker at the center of the GameObject that this script is attached to
                // with the mainCamera's y rotation, and assign the new instance to currentEndMarker
                currentEndMarker = Instantiate(endMarker, this.transform.position, Quaternion.Euler(0, cameraRotation.y, 0));
                Debug.Log("2 NEW! endMarker instance spawned at: " + this.transform.position);

                // Start the cooldown and check routines
                StartCoroutine(ButtonCooldown());
                StartCoroutine(CheckButtonPress());
            }
            else
            {
                Debug.LogError("Main camera is null.");
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
            // Deactivate the current GameObject
            gameObject.SetActive(false);
            Debug.Log("Deactivated current GameObject.");

            if (prevAngle != null) // If there is a previous angle instance
            {
                prevAngle.SetActive(false);  // Deactivate it
                Debug.Log("Deactivated previous angle instance.");
            }

            nextAngle.SetActive(true);  // Set nextAngle to active
            arrow.SetActive(true);
            Debug.Log("Button was not pressed again within " + checkTime + " seconds. Activating nextAngle.");
        }
    }

}
