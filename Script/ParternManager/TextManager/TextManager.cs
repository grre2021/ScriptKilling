using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{   
    /*

    private void Start()
    {
        EventCenter.Instance.AddEventListener<Transform>("deliver_alias", GetAlias);
    }
    
    void GetAlias(Transform _aliasTr)
    {
        Debug.Log(_aliasTr.name);
        aliasTr = _aliasTr;
    }


    public void DisplayText(string _title,string _content)
    {
        textTitle.text = _title;
        textContent.text = _content;
        DisplayUI(clue);
    }

    public void  DisplayUI(GameObject _objectUI)
    {
        if (aliasTr != null)
        {
            Debug.Log("display");
            Vector3 position = new Vector3(aliasTr.position.x , aliasTr.position.y , aliasTr.position.z );                    
            _objectUI.transform.position = position;
           // aliasTr.LookAt(clue.transform);
            _objectUI.transform.Translate(aliasTr.transform.forward*2f);
            _objectUI.transform.LookAt(aliasTr);
            //clue.transform.rotation = Quaternion.LookRotation(aliasTr.position - clue.transform.position);            
            if(!_objectUI.activeSelf)            
            _objectUI.SetActive(true);
            else
             _objectUI.SetActive(false);
        }

    }
    
    public void AddInstTitle(string _titleContent)
    {
        if (titleNumber < spawn_position.Length)
        {
            Instantiate(prefadTitle, spawn_position[titleNumber].position, Quaternion.identity, phoneUI.transform);
            DisplayUI(phoneUI);
            titleNumber++;
        }
    }   
    */
}
