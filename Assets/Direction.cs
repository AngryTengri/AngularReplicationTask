using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Direction : MonoBehaviour
{
    [SerializeField] private GameObject RightClockwiseDir;
    [SerializeField] private GameObject WrongClockwiseDir;
    [SerializeField] private GameObject RightAntiClockwiseDir;
    [SerializeField] private GameObject WrongAntiClockwiseDir;

    [SerializeField] private float alphaChange = 1f;
    [SerializeField] private float lerpChange = 0.01f;
    public bool Clockwise = true;
    private float previousYRotation;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
            previousYRotation = mainCamera.transform.eulerAngles.y;
        else
            Debug.LogError("No main camera found!");

        UpdateDirectionUI();
    }

    private void Update()
    {
        CheckRotationDirection();
    }

    private void CheckRotationDirection()
    {
        if (mainCamera == null)
            return;

        float currentYRotation = mainCamera.transform.eulerAngles.y;
        float rotationDelta = currentYRotation - previousYRotation;

        // Adjust for wrapping
        if (rotationDelta > 180) rotationDelta -= 360;
        else if (rotationDelta < -180) rotationDelta += 360;

        if (rotationDelta > 0) // Rotating Clockwise
        {
            AdjustAlpha(RightClockwiseDir, alphaChange * Time.deltaTime);
            AdjustAlpha(WrongAntiClockwiseDir, -alphaChange * Time.deltaTime);
            AdjustAlpha(WrongClockwiseDir, alphaChange * Time.deltaTime);
            AdjustAlpha(RightAntiClockwiseDir, -alphaChange * Time.deltaTime);

            
        }
        else if (rotationDelta < 0) // Rotating Anti-Clockwise
        {
            AdjustAlpha(RightClockwiseDir, -alphaChange * Time.deltaTime);
            AdjustAlpha(WrongAntiClockwiseDir, alphaChange * Time.deltaTime);
            AdjustAlpha(WrongClockwiseDir, -alphaChange * Time.deltaTime);
            AdjustAlpha(RightAntiClockwiseDir, alphaChange * Time.deltaTime);

            
        }

        if (Clockwise)
        {
            AdjustAlpha(RightClockwiseDir, lerpChange);
            AdjustAlpha(WrongAntiClockwiseDir, -lerpChange);
            AdjustAlpha(WrongClockwiseDir, lerpChange);
            AdjustAlpha(RightAntiClockwiseDir, -lerpChange);
        }
        else
        {
            AdjustAlpha(RightClockwiseDir, -lerpChange);
            AdjustAlpha(WrongAntiClockwiseDir, lerpChange);
            AdjustAlpha(WrongClockwiseDir, -lerpChange);
            AdjustAlpha(RightAntiClockwiseDir, lerpChange);
        }

        previousYRotation = currentYRotation;
    }

    private void AdjustAlpha(GameObject obj, float change)
    {
        Image imageComponent = obj.GetComponent<Image>();
        if (imageComponent != null)
        {
            Color color = imageComponent.color;
            color.a = Mathf.Clamp(color.a + change, 0, 1.5f);
            imageComponent.color = color;
        }
    }

    private void UpdateDirectionUI()
    {
        if (Clockwise)
        {
            RightClockwiseDir.SetActive(true);
            WrongAntiClockwiseDir.SetActive(true);
            RightAntiClockwiseDir.SetActive(false);
            WrongClockwiseDir.SetActive(false);
        }
        else
        {
            RightClockwiseDir.SetActive(false);
            WrongAntiClockwiseDir.SetActive(false);
            RightAntiClockwiseDir.SetActive(true);
            WrongClockwiseDir.SetActive(true);
        }
    }
}