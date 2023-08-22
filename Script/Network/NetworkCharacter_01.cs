using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkCharacter_01 : NetworkBehaviour
{
    [SerializeField] private GameObject character;
    private GameObject trackedAlias;
    private Transform fllow;
    public override void OnStartLocalPlayer()
    {
        FindAlias();
    }
    private void Start()
    {
       
    }
    private void Update()
    {
        if (!isLocalPlayer)
        {
            character.SetActive(true);            
        }
        
    }

    private void FixedUpdate()
    {
        SyncPosition();
       
    }
    void FindAlias()
    {
        if (isLocalPlayer)
        {
            trackedAlias = GameObject.FindWithTag("HeadAlias");
            if (trackedAlias == null)
            {
                Debug.Log("FindPlayerFail");
            }
            else
            {
                fllow = trackedAlias.GetComponent<Transform>();
            }
        }
    }

    void SyncPosition()
    {
        if (!isLocalPlayer)
        {
            if (fllow != null) return;
            if (!character.activeSelf)
                character.SetActive(true);
            else
            {

                Vector3 direction = fllow.forward;
                character.transform.position = new Vector3(fllow.position.x, character.transform.position.y, fllow.position.z);
                Vector3 ro_direction = new Vector3(direction.x, 0, direction.z);
                character.transform.rotation = Quaternion.LookRotation(ro_direction);
            }
        }
    }
}
