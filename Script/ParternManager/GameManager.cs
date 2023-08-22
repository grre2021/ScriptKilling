using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject transition;

    [SerializeField] private Transform trRoom;
    private void Start()
    {
        EventCenter.Instance.AddEventListener("StartGame", StartGame);
        EventCenter.Instance.AddEventListener<Transform>("opemdoor", eventOpenDoor);
      
      
    }
    void StartGame()
    {
        TriggerFlashlight();
        EventCenter.Instance.EventTrigger<Transform>("Deliver", trRoom);
    }
    public void TriggerFlashlight()
    {
        if (flashlight == null) return;
        if (flashlight.activeSelf)
            flashlight.SetActive(false);
        else
            flashlight.SetActive(true);
    }


    public void eventTriggerG()
    {       
        EventCenter.Instance.EventTrigger("eventTriggerG");
    }
   

    public void eventNewInformation(Title title)
    {
        Debug.Log("new title");
        EventCenter.Instance.EventTrigger<Title>("NewInformation", title);
    }

    public void eventOpenDoor(Transform trDoor)
    {
        trDoor.Rotate(Vector3.up,-90);
    }  

   
    public void ChangeToCartoon()
    {
        EventCenter.Instance.AddEventListener("OnSceneLoad", eventChangeToCartoon);
    }

    void eventChangeToCartoon()
    {
        //���µ�ǰ����
        EventCenter.Instance.EventTrigger<string>("UpdateCurrentTask", "����ʳ��");
        //��ʾ��ǰ�����Ѹ���
        EventCenter.Instance.EventTrigger<string>("UpdateButtonPrompt", "��ǰĿ���Ѹ���");

        EventCenter.Instance.RemoveEventListener("OnSceneLoad", eventChangeToCartoon);
    }
}
 
