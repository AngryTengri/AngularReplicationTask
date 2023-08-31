using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public float delay = 3f;  // Delay before the object will start to fade out
    public float fadeDuration = 1f;  // Time it takes to fully fade in or out

    private Image imageComponent;  // A reference to the image component

    private void Awake()
    {
        imageComponent = GetComponent<Image>();  // Get the reference to the Image component
    }

    // Called every time the object becomes enabled
    private void OnEnable()
    {
        // Start the fade in process
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        // Initialize color and elapsed time
        Color color = imageComponent.color;
        float elapsed = 0;

        // Fade in over fadeDuration seconds
        while (elapsed <= fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            imageComponent.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Make sure it ends up fully opaque
        color.a = 1;
        imageComponent.color = color;

        // Wait for the delay period
        yield return new WaitForSeconds(delay);

        // Start the fade out process
        StartCoroutine(Fadeout());
    }

    IEnumerator Fadeout()
    {
        // Initialize color and elapsed time
        Color color = imageComponent.color;
        float elapsed = 0;

        // Fade out over fadeDuration seconds
        while (elapsed <= fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            imageComponent.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Make sure it ends up fully transparent
        color.a = 0;
        imageComponent.color = color;

        // Finally, deactivate the object
        gameObject.SetActive(false);
    }
}
