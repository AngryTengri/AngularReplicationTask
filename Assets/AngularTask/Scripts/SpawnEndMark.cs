using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.Samples;

public class SpawnEndMark : MonoBehaviour
{
    public GameObject endMarker;
    public GameObject nextAngle;
    public GameObject NextBlock;
    public float delay = 2f;
    public float checkTime = 6f;
    private GameObject currentEndMarker;
    private Camera mainCamera;
    private bool buttonIsInCooldown = false;
    private bool buttonPressedAgain = false;
    private bool isSpaceButtonDown = false;
    private RotationLogger rotationLogger;
    public float yRotation;
    public bool Anticlockwise;

    void Start()
    {
        mainCamera = Camera.main;
        GameObject rotationLoggerObject = GameObject.Find("RotationLoggerManager");
        if (rotationLoggerObject != null)
        {
            rotationLogger = rotationLoggerObject.GetComponent<RotationLogger>();
        }
    }

    void Update()
    {
        bool isVrTriggerDown = VRSInputManager.instance.GetButtonDown(VRSButtonReference.TriggerL);
        bool isJumpButtonDown = Input.GetButtonDown("Jump");

        if (Input.GetButtonUp("Jump"))
        {
            isSpaceButtonDown = false;
        }

        if (!buttonIsInCooldown && (isVrTriggerDown || (isJumpButtonDown && !isSpaceButtonDown)))
        {
            isSpaceButtonDown = true;

            if (rotationLogger != null && rotationLogger.DoesCsvExist())
            {
                rotationLogger.LogRotation("MarkerSpawned", mainCamera.transform.rotation.eulerAngles, mainCamera.transform.position, yRotation, Anticlockwise);
            }

            buttonPressedAgain = true;

            if (mainCamera != null)
            {
                if (currentEndMarker != null)
                {
                    Destroy(currentEndMarker);
                }

                Vector3 cameraRotation = mainCamera.transform.rotation.eulerAngles;
                currentEndMarker = Instantiate(endMarker, this.transform.position, Quaternion.Euler(0, cameraRotation.y, 0));

                StartCoroutine(ButtonCooldown());
                StartCoroutine(CheckButtonPress());
            }
        }
    }

    IEnumerator ButtonCooldown()
    {
        buttonIsInCooldown = true;
        yield return new WaitForSeconds(delay);
        buttonIsInCooldown = false;
    }

    IEnumerator CheckButtonPress()
    {
        buttonPressedAgain = false;
        yield return new WaitForSeconds(checkTime);
        if (!buttonPressedAgain)
        {
            if (nextAngle != null)
            {
                nextAngle.SetActive(true);
                UIManager.Instance.StartFadeIn();
                gameObject.SetActive(false);
            }
            else if (NextBlock != null)
            {
                //UIManager.Instance.StartFadeIn(); // Change this to another UI element to say to have a break or something
                NextBlock.SetActive(true);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
