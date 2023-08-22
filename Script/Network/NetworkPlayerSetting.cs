using UnityEngine;

public class NetworkPlayerSetting : MonoBehaviour
{
   
    public void ButtonCharacterChose(GameObject _Button)
    {
        EventCenter.Instance.EventTrigger<GameObject>("ChoseButtonDown", _Button);
    }
   
}
