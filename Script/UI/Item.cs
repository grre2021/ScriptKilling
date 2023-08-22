using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Item", menuName = "Bag/New item")]
public class Item : ScriptableObject
{
    public GameObject generatePlatform;
    public GameObject item;
    public Button bu_item;
    public Image im_item;

}
