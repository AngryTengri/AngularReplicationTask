using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    List<gazeInteractionPlain> infos = new List<gazeInteractionPlain>();

    void Update()
    {
        infos = FindObjectsOfType<gazeInteractionPlain>().ToList();

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {
                OpenInfo(go.GetComponent<gazeInteractionPlain>());
            }
            else
            {
                CloseAll();
            }
        }
        else
        {
            CloseAll();
        }
    }

    void OpenInfo(gazeInteractionPlain desiredInfo)
    {
        foreach (gazeInteractionPlain info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }

    void CloseAll()
    {
        foreach (gazeInteractionPlain info in infos)
        {
            info.CloseInfo();
        }
    }
}
