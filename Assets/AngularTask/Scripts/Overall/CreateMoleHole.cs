using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.Samples;

public class CreateMoleHole : MonoBehaviour
{
    [SerializeField] GameObject Hole_Prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.TriggerL))
        {
            Instantiate(Hole_Prefab, VRSInputManager.instance.GetPosition(VRSPositionReference.AimL), Hole_Prefab.transform.rotation);
        }
    }
}
