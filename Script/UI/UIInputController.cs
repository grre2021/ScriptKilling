using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
public class UIInputController : Singleton<UIInputController>
{
   private InputSystemUIInputModule inputModule;
    private void Start()
    {
        inputModule = GetComponent<InputSystemUIInputModule>();
        //inputModule.enabled = false;
    }

    public void SelectUI(Selectable selectable)
    {
        selectable.Select();
        selectable.OnSelect(null);
        inputModule.enabled = true;
        
    }
}
