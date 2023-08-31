using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextMarker : MonoBehaviour
{

    public GameObject nextMark;
    public GameObject lastMark;
    public Camera arCamera;

    void Update()
    {
        Ray ray = new Ray(arCamera.transform.position, arCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                changeText();
            }
        }
    }

    void changeText()
    {
        nextMark.gameObject.SetActive(true);
        lastMark.gameObject.SetActive(false);
    }
}
