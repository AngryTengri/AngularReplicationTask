using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour
{
    public bool right;

    void Update()
    {
        Vector3 _Target = Camera.main.transform.position;
        _Target.y = transform.position.y;
        transform.LookAt(_Target);

        // add 90 degrees to the Y rotation
        int direction = right ? -90 : 90;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                               transform.rotation.eulerAngles.y + direction,
                                               transform.rotation.eulerAngles.z);
    }
}
