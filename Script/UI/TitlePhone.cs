using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New MainTitle",menuName ="Bag/New MainTitle")]
public class TitlePhone : ScriptableObject
{
    public List<Title> titleList=new List<Title>();
}
