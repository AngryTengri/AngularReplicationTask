using UnityEngine;
using Wave.OpenXR.Toolkit.CompositionLayer.Passthrough;
using Wave.OpenXR.CompositionLayer;
using Wave.OpenXR.CompositionLayer.Passthrough;

public class Projected : MonoBehaviour
{
    [SerializeField] Mesh UsingMesh;
    int ID;

    void Start()
    {
        CreatePassthrough();
        SetPassthroughShape();
        SetPassthroughTransform();
    }

    void CreatePassthrough()
    {
        ID = CompositionLayerPassthroughAPI.CreateProjectedPassthrough(LayerType.Overlay);
    }

    void SetPassthroughShape()
    {
        int[] indices = new int[UsingMesh.triangles.Length];
        for (int i = 0; i < UsingMesh.triangles.Length; i++)
        {
            indices[i] = UsingMesh.triangles[i];
        }

        CompositionLayerPassthroughAPI.SetProjectedPassthroughMesh(ID, UsingMesh.vertices, indices, true);
    }

    void SetPassthroughTransform()
    {
        CompositionLayerPassthroughAPI.SetProjectedPassthroughMeshTransform(ID, ProjectedPassthroughSpaceType.Worldlock, Vector3.forward * 2 + Vector3.up, Quaternion.identity, Vector3.one);
    }
}
