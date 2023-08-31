using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Wave.OpenXR.Toolkit.Samples;

public class readAngle : MonoBehaviour
{
    public TextMeshProUGUI newAngle;

    //Check the angle and write to text
    void Update()
    {
       writeAngle();
    }

    public void writeAngle()
    {
        // Read the y rotation of the gameObject this script is attached to
        float rotation = this.transform.rotation.eulerAngles.y;
        newAngle.text = "Y Rotation: " + rotation.ToString("F2");
    }
}
