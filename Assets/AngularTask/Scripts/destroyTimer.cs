using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTimer : MonoBehaviour
{
    public float delay = 10f;  // Delay after which the object will be deactivated

    // Use the Start method to begin the countdown
    void Start()
    {
        StartCoroutine(DeactivateAfterDelay());
    }

    // Deactivate the object after delay seconds
    System.Collections.IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
