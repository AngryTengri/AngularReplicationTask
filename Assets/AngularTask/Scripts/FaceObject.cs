using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObject : MonoBehaviour
{
    public GameObject mark; // The object that the arrow will face

    void Update()
    {
        Vector3 _Target = mark.transform.position;

        // Calculate the distance between the arrow and the mark
        float distance = Vector3.Distance(transform.position, _Target);

        // Rotate the arrow to face the mark
        transform.LookAt(_Target);

        // Create a ray from the main camera forward
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit the target
            if (hit.collider.gameObject == mark)
            {
                // If it did, deactivate the arrow
                gameObject.SetActive(false);
            }
        }
    }
}
