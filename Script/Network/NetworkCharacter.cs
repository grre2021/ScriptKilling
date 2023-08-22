using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Tilia.CameraRigs.TrackedAlias;
using Zinnia.Tracking.CameraRig;
using Tilia.Indicators.ObjectPointers;

public class NetworkCharacter : NetworkBehaviour
{

    public  GameObject Character;
    
    //public GameObject RightController;
    public Transform Track_head;
    public TrackedAliasFacade aliasFacade;
    public GameObject alias_camera;
    //public GameObject UI_button;
    public GameObject Pre_body;
    //private Transform start_point;

   




    public override void OnStartLocalPlayer()
    {
        Debug.Log("On start");
        // EventCenter.Instance.AddEventListener("StartGame", SetPosition);
        //EventCenter.Instance.AddEventListener<Transform>("deliver_point", DeliverPoint);
        EventCenter.Instance.AddEventListener<GameObject>("ChoseButtonDown", DestoryCommand);        
        EventCenter.Instance.AddEventListener<Transform>("Deliver", Deliver);
        EventCenter.Instance.AddEventListener("StartChangeScene", Clear);
        EventCenter.Instance.AddEventListener("StartChangeScene", SetPositionZero);
        EventCenter.Instance.AddEventListener("OnSceneLoad", FindAlais);
       // StartCoroutine(nameof(DelayStart));

        if (isLocalPlayer)
        {          
            aliasFacade.CameraRigs.Add(FindObjectOfType<LinkedAliasAssociationCollection>());
            /*
            VRIK.solver.spine.headTarget = aliasFacade.HeadsetAlias.transform;
            VRIK.solver.leftArm.target=aliasFacade.LeftControllerAlias.transform;
            VRIK.solver.rightArm.target=aliasFacade.RightControllerAlias.transform;
            */
            //aliasFacade.CameraRigs.Clear();
        }
      
    }

    private void FixedUpdate()
    {

        /*
             
        */
        
        if (!isLocalPlayer)
        {
           
            //AudioListener.enabled = false;
            
            

            if (!Character.activeSelf)
                Character.SetActive(true);
            else
            {
                Vector3 direction = Track_head.forward;
                Vector3 ro_direction = new Vector3(direction.x, 0, direction.z);
                Vector3 tr_position = Track_head.position;
                
                
                Character.transform.position = new Vector3(tr_position.x, Character.transform.position.y, tr_position.z);
                
                Character.transform.rotation = Quaternion.LookRotation(ro_direction);
            }
        }
    }
    
    /*
    void SetPosition()
    {
              
        if(start_point != null)
        {
            Debug.Log("dddd");
            Pre_body.SetActive(false);
            this.transform.position=start_point.position;
            Pre_body.SetActive(true);
        }

    }
    */
   [Command]
    private void DestoryCommand(GameObject _gameobject)
    {           
            Destroy(_gameobject);   
    }

    /*
    void DeliverPoint(Transform _deliverPoint)
    {
        Debug.Log(_deliverPoint.name);
        start_point = _deliverPoint;
    }
    */
    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.6f);
        EventCenter.Instance.EventTrigger<Transform>("deliver_alias", Track_head);
    }


    void Deliver(Transform _deliverposition)
    {
        Debug.Log (_deliverposition.name);
        Pre_body.SetActive(false);
        this.transform .position = _deliverposition.position;
        Pre_body.SetActive(true);
        if(!isLocalPlayer)
        {
            Character.transform.position=_deliverposition.position;
            Debug.Log(Character.transform.position);
        }
    }

    
    
    void Clear()
    {
        aliasFacade.CameraRigs.Clear();
    }
    
    void SetPositionZero()
    {
        
        Pre_body.SetActive(false);
        this.transform.position = Vector3.zero;
        Pre_body.SetActive(true) ;
        Debug.Log(this.transform.position);
    }
    
    void FindAlais()
    {
        
        if (isLocalPlayer)
        {
            var a = FindObjectOfType<LinkedAliasAssociationCollection>();
            Pre_body.SetActive(false);
            //this.transform.position = a.transform.position;
            Pre_body.SetActive(true);
            aliasFacade.CameraRigs.Add(a);
           
            
                  
        };
    }




}
