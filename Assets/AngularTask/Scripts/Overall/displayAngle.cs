using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayAngle : MonoBehaviour
{
    public Transform vrCameraTransform;
    public TextMeshProUGUI textMesh;

    // Update is called once per frame
    void Update()
    {
        if (vrCameraTransform != null && textMesh != null)
        {
            var rotation = vrCameraTransform.GetComponent<Transform>().rotation.eulerAngles.y;
            textMesh.text = "Y Rotation: " + rotation.ToString("F2");
        }
    }
}
