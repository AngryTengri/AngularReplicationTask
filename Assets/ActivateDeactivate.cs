using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.Samples;

public class ActivateDeactivate : MonoBehaviour
{
    public GameObject objectToActivate1;
    public GameObject objectToActivate2;
    public GameObject objectToActivate3;
    public GameObject objectToDeactivate1;
    public GameObject objectToDeactivate2;
    public GameObject objectToDeactivate3;

    public float timeA;

    private bool canCheckInput;
    private bool hasActivated = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeA);
        canCheckInput = true;
    }

    private void Update()
    {
        if (!hasActivated && canCheckInput && (VRSInputManager.instance.GetButtonDown(VRSButtonReference.TriggerL) || Input.GetKeyDown(KeyCode.Space)))
        {
            ActivateObject();
            DeactivateObject();
            DeactivateObject2();

            hasActivated = true;
        }
    }

    private void ActivateObject()
    {
        objectToActivate1.SetActive(true);
        objectToActivate2.SetActive(true);
        objectToActivate3.SetActive(true);
    }

    private void DeactivateObject()
    {
        objectToDeactivate1.SetActive(false);
        objectToDeactivate2.SetActive(false);
    }

    private void DeactivateObject2()
    {
        objectToDeactivate3.SetActive(false);
    }
}
