using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        //if()
        TextControl.Instance.TextPrompt();

    }
    private void OnTriggerExit(Collider other)
    {
        TextControl.Instance.TextPromptStop();
    }
}
