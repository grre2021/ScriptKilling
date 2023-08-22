using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomTrigger : MonoBehaviour
{
    [SerializeField] private Transform next_Tr;
    private Collider _collider;
 
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ButtonPrompt("Push G to leave");                      
            EventCenter.Instance.AddEventListener("eventTriggerG", SetPosition);            
        }
    }

    void SetPosition()
    {
        
        if (next_Tr ==null) return;
        Debug.Log("setposition");
        EventCenter.Instance.EventTrigger<Transform>("Deliver", next_Tr);
        EventCenter.Instance.EventTrigger("tran");
        EventCenter.Instance.RemoveEventListener("eventTriggerG", SetPosition);

        /*
        
        */
    }
   
    private void OnTriggerExit(Collider other)
    {
        EventCenter.Instance.RemoveEventListener("eventTriggerG", SetPosition);
    }
}
