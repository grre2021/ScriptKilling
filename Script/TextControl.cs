using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Zinnia.Tracking.Collision.Active.ActiveCollisionRegisteredConsumerContainer;

/// <summary>
/// 使用单例模式
/// </summary>
public class TextControl :Singleton<TextControl>
{

    
     private GameObject pushing_permat_test;
     private GameObject test_display;

    private void Start()
    {
        pushing_permat_test = GameObject.Find("ButtonPrompt");
        if (pushing_permat_test != null)
        {
            Debug.Log(pushing_permat_test.name);
            pushing_permat_test.SetActive(false);
        }


        test_display = GameObject.Find("Clue");
        if (test_display != null)
        {
            Debug.Log(test_display.name);
            test_display.SetActive(false);
        }

    }
    public void TextPrompt()
    {
        Debug.Log("TextPrompt");
        pushing_permat_test.SetActive(true);
        
    }
     public void TextPromptStop()
    {
        pushing_permat_test.SetActive(false);
    }
   public void TextDisplay()
    {
        if (!pushing_permat_test.activeSelf) return;
        if (!test_display.activeSelf)
            test_display.SetActive(true);
        else
            test_display.SetActive(false);

    }
   
}
