using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchAngle : MonoBehaviour
{
    private Camera mainCamera; // The main camera whose y angle we want to match

    void Start()
    {
        mainCamera = Camera.main; // Find and store the reference to the main camera
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Get the current rotation of this object and the camera
            Vector3 currentRotation = transform.rotation.eulerAngles;
            Vector3 cameraRotation = mainCamera.transform.rotation.eulerAngles;

            // Set the y rotation of this object to match the camera's y rotation
            transform.rotation = Quaternion.Euler(currentRotation.x, cameraRotation.y, currentRotation.z);
        }
        else
        {
            Debug.LogError("Main camera not found");
        }
    }
}
