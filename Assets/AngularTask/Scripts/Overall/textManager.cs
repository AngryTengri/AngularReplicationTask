using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class textManager : MonoBehaviour
{
    public TextMeshProUGUI text1; //Start hide
    public TextMeshProUGUI text2; //Instructions
    public TextMeshProUGUI text3; //Perceived Angle
    public GameObject start;
    //public Button button; //Press to confirm angle
    public GameObject nextMark;

    // Camera reference from where the ray is shot
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
        // Set the text and the button to active
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(true);
        text3.gameObject.SetActive(true);
        nextMark.gameObject.SetActive(true);
        start.gameObject.SetActive(false);
        //button.gameObject.SetActive(true);
    }
}
