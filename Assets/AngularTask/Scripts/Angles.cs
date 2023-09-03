using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angles : MonoBehaviour
{
    public GameObject AngularTaskPrefabClock;
    public GameObject AngularTaskPrefabAntiClock;
    public GameObject NextBlockRef;
    public float[] YRotations = new float[7];
    public bool[] Anticlockwise = new bool[7];
    public float Shift = 0.5f;

    private GameObject[] angularInstances = new GameObject[7];

    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            // Calculate shift. First 4 move forward, the last 3 move backward.
            float currentShift = (i < 4) ? Shift * i : Shift * 3 - Shift * (i - 3);

            // Determine which prefab to use based on Anticlockwise array
            GameObject prefabToUse = Anticlockwise[i] ? AngularTaskPrefabAntiClock : AngularTaskPrefabClock;
            if (prefabToUse != null)
            {
                // Instantiate the chosen prefab at specified positions
                angularInstances[i] = Instantiate(prefabToUse, transform.position + new Vector3(0, 0, currentShift), Quaternion.identity, transform);

                // If not the first instance, set active to false
                if (i != 0)
                {
                    angularInstances[i].SetActive(false);
                }

                // Set rotation
                RotateChildObjects(angularInstances[i], Anticlockwise[i] ? -YRotations[i] : YRotations[i]);

                // Set yRotation in SpawnEndMark
                angularInstances[i].GetComponent<SpawnEndMark>().yRotation = YRotations[i];
            }

            // If it's the 7th object, assign the NextBlockRef to it
            if (i == 6 && angularInstances[i] != null)
            {
                angularInstances[i].GetComponent<SpawnEndMark>().NextBlock = NextBlockRef;
            }
        }

        // Set nextAngle and prevAngle fields
        for (int i = 0; i < 6; i++) // Till 6 because the last instance doesn't have a next instance
        {
            var spawnEndMark = angularInstances[i].GetComponent<SpawnEndMark>();
            if (spawnEndMark != null)
            {
                spawnEndMark.nextAngle = angularInstances[i + 1];
            }
        }
    }

    void RotateChildObjects(GameObject parent, float rotation)
    {
        Transform center = parent.transform.Find("Center");
        if (center != null)
        {
            center.Rotate(0, rotation, 0);
        }

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
