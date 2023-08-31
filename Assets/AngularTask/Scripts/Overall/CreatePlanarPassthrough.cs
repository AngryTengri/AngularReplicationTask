using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.CompositionLayer.Passthrough;
using Wave.OpenXR.CompositionLayer;

public class CreatePlanarPassthrough : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CompositionLayerPassthroughAPI.CreatePlanarPassthrough(LayerType.Underlay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
