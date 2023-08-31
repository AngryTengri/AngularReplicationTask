using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.CompositionLayer.Passthrough;
using Wave.OpenXR.CompositionLayer;

public class Passthrough : MonoBehaviour
{
    int ID;
    void Start()
    {
        ID = CompositionLayerPassthroughAPI.CreatePlanarPassthrough(LayerType.Underlay);
    }

    void Update()
    {
        if (Time.realtimeSinceStartup > 10)
        {
            CompositionLayerPassthroughAPI.DestroyPassthrough(ID);
        }
    }
}
