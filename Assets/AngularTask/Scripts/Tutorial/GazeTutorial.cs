using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GazeTutorial : MonoBehaviour
{
    List<gazeInteractionTutorial> infos = new List<gazeInteractionTutorial>();

    void Update()
    {
        infos = FindObjectsOfType<gazeInteractionTutorial>().ToList();

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {
                OpenInfo(go.GetComponent<gazeInteractionTutorial>());
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

    void OpenInfo(gazeInteractionTutorial desiredInfo)
    {
        foreach (gazeInteractionTutorial info in infos)
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
        foreach (gazeInteractionTutorial info in infos)
        {
            info.CloseInfo();
        }
    }
}
