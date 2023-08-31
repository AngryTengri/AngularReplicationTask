using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmountRotationOneWay : MonoBehaviour
{
    public Image fillImage;
    public float minYRotation = 80f;
    public float maxYRotation = 160f;
    private float currentMaxFillAmount = 0f;

    // Use this for initialization
    void Start()
    {
        if (fillImage == null)
        {
            Debug.LogError("Please assign an Image object in the inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = Camera.main.transform.eulerAngles.y;

        float fillAmount = 0f;

        // If the camera's Y rotation is below the minimum or above 300, fill amount is 0.
        if (yRotation < minYRotation || yRotation >= 300f)
        {
            fillAmount = 0f;
        }
        // If the camera's Y rotation is above the maximum, fill amount is (maxYRotation - minYRotation) / 360.
        else if (yRotation > maxYRotation)
        {
            fillAmount = (maxYRotation - minYRotation) / 360f;
        }
        // If the camera's Y rotation is between the minimum and maximum,
        // map the Y rotation to a fill amount from 0 (at minYRotation) to (maxYRotation - minYRotation) / 360 (at maxYRotation).
        else
        {
            fillAmount = (yRotation - minYRotation) / (maxYRotation - minYRotation) * ((maxYRotation - minYRotation) / 360f);
        }

        // If the calculated fillAmount is greater than the currentMaxFillAmount, update currentMaxFillAmount.
        if (fillAmount > currentMaxFillAmount)
        {
            currentMaxFillAmount = fillAmount;
        }

        // Set the fillImage's fillAmount to the currentMaxFillAmount.
        fillImage.fillAmount = currentMaxFillAmount;
    }
}








