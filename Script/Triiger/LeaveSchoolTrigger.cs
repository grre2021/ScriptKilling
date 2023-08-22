using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSchoolTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ButtonPrompt("Push G to leave");
            Debug.Log(other.name);
            EventCenter.Instance.AddEventListener("eventTriggerG", SetPosition);

        }
    }

    void SetPosition()
    {
        Debug.Log("srtposition");
        EventCenter.Instance.RemoveEventListener("eventTriggerG", SetPosition);
        EventCenter.Instance.EventTrigger<string>("changeload", "Corridor");

       

    }


    private void OnTriggerExit(Collider other)
    {
        EventCenter.Instance.RemoveEventListener("eventTriggerG", SetPosition);
    }
}
