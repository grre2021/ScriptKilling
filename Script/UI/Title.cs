using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Title", menuName = "PhoneNews/New Title")]
public class Title : ScriptableObject
{
    public Button button;
    public Image titleButton;
    public string titleText;
    public string contentText;
}
