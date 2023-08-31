using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeMarkerTutorial : MonoBehaviour
{
    public Camera arCamera;
    private gazeInteractionTutorial gazeScript;

    void Start()
    {
        gazeScript = GetComponent<gazeInteractionTutorial>();
    }

    void Update()
    {
        Ray ray = new Ray(arCamera.transform.position, arCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                if (gazeScript != null)
                {
                    gazeScript.OpenInfo();
                }
            }
            else
            {
                if (gazeScript != null)
                {
                    gazeScript.CloseInfo();
                }
            }
        }
    }
}
