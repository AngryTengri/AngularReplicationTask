using System.Collections;
using UnityEngine;

public class RotatePlane : MonoBehaviour
{
    public float rotationDuration = 2f; // Duration of each rotation in seconds
    public AnimationCurve rotationCurve; // Animation curve for easing in and out of the rotation
    public float delayBeforeRotation = 1f; // Delay before the rotation starts
    public GameObject objectToHide; // The game object to hide after the rotation
    public GameObject objectToReveal;
    private bool isRotating = false;

    void Start()
    {
        // Start the rotation after the delay
        Invoke("RotateClockwise", delayBeforeRotation);
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up, 360f * Time.deltaTime / rotationDuration);
        }
    }

    void RotateClockwise()
    {
        if (!isRotating) // Check if rotation is already in progress
        {
            isRotating = true;
            StartCoroutine(FlipAndRotate());
        }
    }

    IEnumerator FlipAndRotate()
    {
        yield return new WaitForSeconds(rotationDuration);

        // Flip the plane 180 degrees on the X-axis
        transform.Rotate(Vector3.right, 180f);

        // Reverse the rotation curve
        AnimationCurve reversedCurve = new AnimationCurve();
        for (int i = rotationCurve.length - 1; i >= 0; i--)
        {
            Keyframe keyframe = rotationCurve[i];
            keyframe.time = 1f - keyframe.time;
            reversedCurve.AddKey(keyframe);
        }

        // Rotate 360 degrees in the opposite direction
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            float rotationAngle = 360f * reversedCurve.Evaluate(t);

            transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime / rotationDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRotating = false;

        // Hide the game object
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }

        objectToReveal.SetActive(true);
    }
}
