using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Tilia.Interactions.SpatialButtons;
using UnityEngine.UI;
public class ServerManager : NetworkBehaviour
{
    [Header("Chose_UI")]
    [SerializeField] private GameObject UI;
    [SerializeField] private Transform point;   
    [Header("Point")]
    [SerializeField] private Transform start_point;

    private string[] characterName;
    private Transform current_buttom;

    private GameObject _ui;

    //

    [SyncVar]
    int connect_player;
    private void Start()
    {
        EventCenter.Instance.AddEventListener<GameObject>("ChoseButtonDown", AddPlayer);
        characterName = new string[6];
        characterName[0] = "name0";
        characterName[1] = "name1";
        characterName[2] = "name2"; 
        characterName[3] = "name3";
        characterName[4] = "name4";
        characterName[5] = "name5";
        StartCoroutine(nameof(DeliverPoint));
        InChoseCharacter();
        
        
    }
  
    void InChoseCharacter()
    {
        if (isServer)
        {
            int i = 0;
            Debug.Log("isserver");
            for (i = 0; i < 6; i++)
            {
                Vector3 _position = new Vector3(point.position.x - i, point.position.y, point.position.z);
                _ui = GameObject.Instantiate(UI, _position, point.rotation);
                _ui.name = characterName[i];
                _ui.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = characterName[i];
                NetworkServer.Spawn(_ui);
            }

        }
    }
    IEnumerator DeliverPoint()
    {
        yield return new WaitForSeconds(0.5f);       
        EventCenter.Instance.EventTrigger<Transform>("deliver_point", start_point);       
    }
  
    void AddPlayer(GameObject gameObject)
    {
        Debug.Log("addplayer");
        connect_player++;
        WaitingPlayer();
    }
   
    void WaitingPlayer()
    {

        if(connect_player>=1)
        {
            EventCenter.Instance.EventTrigger("StartGame");
        }
    }
    
   
}
