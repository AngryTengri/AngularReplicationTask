using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchDisatance : MonoBehaviour
{
    private Camera mainCamera;  // A private field to hold the reference to the main camera
    public float distanceFromCamera = 3f;  // The desired distance from the camera
    public float speed = 3f;  // The speed at which the object moves to the desired position

    void Start()
    {
        mainCamera = Camera.main;  // Find the main camera and store the reference
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Get the current position of this object
            Vector3 currentPosition = transform.position;

            // Calculate the desired Z position
            float desiredZ = mainCamera.transform.position.z + distanceFromCamera;

            // Create the desired position, only changing the Z value
            Vector3 desiredPosition = new Vector3(currentPosition.x, currentPosition.y, desiredZ);

            // Smoothly move this object towards the desired position
            transform.position = Vector3.Lerp(currentPosition, desiredPosition, speed * Time.unscaledDeltaTime);
        }
    }
}
