using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Wave.OpenXR.Toolkit.Samples;

public class displayNewAngle : MonoBehaviour
{
    public Transform vrCameraTransform;
    public TextMeshProUGUI newAngle;
    public float setAngle = 300f;

    void Update()
    {
        if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.TriggerL))
        {
            checkAngle();
        }
    }

    public void checkAngle()
    {
        float rotation = setAngle - vrCameraTransform.rotation.eulerAngles.y;
        rotation = Mathf.Abs(rotation);  // always positive
        newAngle.text = "Y Rotation: " + rotation.ToString("F2");
    }
}
