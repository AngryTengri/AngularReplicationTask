using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit.CompositionLayer.Passthrough;
using Wave.OpenXR.CompositionLayer.Passthrough;
using Wave.OpenXR.CompositionLayer;

public class Partial : MonoBehaviour
{
    [SerializeField] Mesh UsingMesh;
    [SerializeField] Transform Trans;
    [SerializeField] Transform Cam;
    int ID;

    void Start()
    {
        ID = CompositionLayerPassthroughAPI.CreateProjectedPassthrough(LayerType.Underlay);

        int[] indices = new int[UsingMesh.triangles.Length];
        for (int i = 0; i < UsingMesh.triangles.Length; i++)
        {
            indices[i] = UsingMesh.triangles[i];
        }

        CompositionLayerPassthroughAPI.SetProjectedPassthroughMesh(ID, UsingMesh.vertices, indices);
        CompositionLayerPassthroughAPI.SetProjectedPassthroughMeshTransform(ID, ProjectedPassthroughSpaceType.Worldlock, Cam.InverseTransformPoint(Trans.position), Quaternion.Inverse(Cam.transform.rotation) * Trans.rotation, Trans.lossyScale);
    }

    void Update()
    {
        CompositionLayerPassthroughAPI.SetProjectedPassthroughMeshTransform(ID, ProjectedPassthroughSpaceType.Worldlock, Cam.InverseTransformPoint(Trans.position), Quaternion.Inverse(Cam.transform.rotation) * Trans.rotation, Trans.lossyScale);
    }
}


