using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angles : MonoBehaviour
{
    public GameObject AngularTaskPrefab;
    public float YRotation1;
    public bool Anticlockwise1;
    public float YRotation2;
    public bool Anticlockwise2;
    public float YRotation3;
    public bool Anticlockwise3;

    void Start()
    {
        if (AngularTaskPrefab != null)
        {
            // Creating AngularTaskPrefab at the parent object position
            GameObject angularInstance1 = Instantiate(AngularTaskPrefab, transform.position, Quaternion.identity, transform);
            RotateChildObjects(angularInstance1, Anticlockwise1 ? -YRotation1 : YRotation1);
            angularInstance1.GetComponent<SpawnEndMark>().yRotation = YRotation1;

            // Creating AngularTaskPrefab at 0.5 distance in z direction
            GameObject angularInstance2 = Instantiate(AngularTaskPrefab, transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity, transform);
            angularInstance2.SetActive(false);
            RotateChildObjects(angularInstance2, Anticlockwise2 ? -YRotation2 : YRotation2);
            angularInstance2.GetComponent<SpawnEndMark>().yRotation = YRotation2;

            // Creating AngularTaskPrefab at 1 distance in z direction
            GameObject angularInstance3 = Instantiate(AngularTaskPrefab, transform.position + new Vector3(0, 0, 1f), Quaternion.identity, transform);
            angularInstance3.SetActive(false);
            RotateChildObjects(angularInstance3, Anticlockwise3 ? -YRotation3 : YRotation3);
            angularInstance3.GetComponent<SpawnEndMark>().yRotation = YRotation3;

            // Setting nextAngle and prevAngle fields
            var spawnEndMark2_1 = angularInstance1.GetComponent<SpawnEndMark>();
            var spawnEndMark2_2 = angularInstance2.GetComponent<SpawnEndMark>();
            var spawnEndMark2_3 = angularInstance3.GetComponent<SpawnEndMark>();

            if (spawnEndMark2_1 != null)
            {
                spawnEndMark2_1.nextAngle = angularInstance2;
                spawnEndMark2_1.prevAngle = null; // No previous instance for the first one
            }
            if (spawnEndMark2_2 != null)
            {
                spawnEndMark2_2.nextAngle = angularInstance3;
                spawnEndMark2_2.prevAngle = angularInstance1;
            }
            if (spawnEndMark2_3 != null)
            {
                spawnEndMark2_3.prevAngle = angularInstance2;
            }
        }
    }

    void RotateChildObjects(GameObject parent, float rotation)
    {
        Transform startCenter = parent.transform.Find("StartCenter");
        if (startCenter != null)
        {
            startCenter.Rotate(0, rotation, 0);
        }

        Transform firstCenter = parent.transform.Find("FirstCenter");
        if (firstCenter != null)
        {
            firstCenter.Rotate(0, rotation * 2, 0);
        }
    }
}
