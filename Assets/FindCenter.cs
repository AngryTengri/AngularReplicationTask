using System.Collections;
using UnityEngine;

public class FindCenter : MonoBehaviour
{
    public float zone = 5.0f; // You can define the value in Unity Editor
    public GameObject marktoActivate; // Set this to the target GameObject in Unity Editor
    public float time = 2.0f; // Time delay in seconds
    public float rotationTolerance = 10.0f; // Tolerance for rotation matching

    private bool hasActivated = false; // Flag to track whether we've activated the GameObject

    private void Update()
    {
        // Early exit if we've already activated the GameObject
        if (hasActivated) return;

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 myPosition = this.transform.position;

        // Ignore Y difference
        Vector2 cameraPos2D = new Vector2(cameraPosition.x, cameraPosition.z);
        Vector2 myPos2D = new Vector2(myPosition.x, myPosition.z);

        float cameraRotation = Camera.main.transform.eulerAngles.y;
        float myRotation = this.transform.eulerAngles.y;

        bool isWithinRotationTolerance = Mathf.Abs(Mathf.DeltaAngle(cameraRotation, myRotation)) <= rotationTolerance;

        if (Vector2.Distance(cameraPos2D, myPos2D) <= zone && isWithinRotationTolerance)
        {
            // Start Coroutine for delayed activation
            StartCoroutine(ActivateAfterDelay());
        }
    }

    IEnumerator ActivateAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(time);

        // Activate the GameObject
        marktoActivate.SetActive(true);

        // Set flag to true, so we don't activate again
        hasActivated = true;
    }
}
