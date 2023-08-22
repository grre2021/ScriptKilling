using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tilia.Interactions.Interactables.Interactables;
public class UIManager : Singleton<UIManager>
{

    [Header("Bag")]
    public GameObject Bag;

   [Header("wakeup")]
    public Image imWakeup;
    public GameObject animWakeUp;

    [Header("Taskupdate")]
    public GameObject textDisplay;
    public TMPro.TextMeshProUGUI titleText;
    public TMPro.TextMeshProUGUI contentText;

    [Header("Phone")]
    public GameObject Titles;
    public TitlePhone titlePhone;
    

    [Header("ButtonPrompt")]
    public GameObject buttonPrompt;

    [Header("CurrentTask")]
    public GameObject currentTaskPrompt;
    public TMPro.TextMeshProUGUI currentTaskText;

    [Header("Item")]
    public Transform ItemPosition;
    

    [Header("Transition")]
    public GameObject transition;
    public bool endTran=true;
    private void Start()
    {
        EventCenter.Instance.AddEventListener("StartGame",eventWakeup);
        EventCenter.Instance.AddEventListener<Title>("NewInformation", AddTitleList);
        EventCenter.Instance.AddEventListener<Image>("GraduallyShow", eventGraduallyShow);
        EventCenter.Instance.AddEventListener<Transform>("deliver_alias", eventAddTR);
        EventCenter.Instance.AddEventListener("tran",eventTransition_im);
        EventCenter.Instance.AddEventListener<string>("UpdateCurrentTask", UpdateCurrentTaskPrompt);
        EventCenter.Instance.AddEventListener<string>("UpdateButtonPrompt", ButtonPrompt);
    }


    /// <summary>
    /// Wakeup
    /// </summary>
    /// <param name="_Button"></param>
    void eventWakeup()
    {        
        animWakeUp.SetActive(true);
        StartCoroutine(nameof(GraduallyShow), imWakeup);
    }
    
    void eventGraduallyShow(Image image)
    {
         StartCoroutine(nameof(GraduallyShow), image);
    }
    IEnumerator GraduallyShow(Image _imGraduallyShow)
    {
        yield return new WaitForSeconds(0.3f);
        while(true)
        {            
            if(_imGraduallyShow != null)
            {
                _imGraduallyShow.color=new Color(_imGraduallyShow.color.r, _imGraduallyShow.color.g, _imGraduallyShow.color.b, _imGraduallyShow.color.a-0.1f*Time.deltaTime);
                
            }
            if(_imGraduallyShow.color.a<=0)
            {
                StopCoroutine(nameof(GraduallyShow)); 
            }
            yield return null;
        }
    }



    /// <summary>
    /// Title
    /// </summary>
    void DisplayText()
    {
        TriggerTitle();
        textDisplay.SetActive(true);
    }
    public void TriggerTitle()
    {
        if (Titles.activeSelf)
        {
            Titles.SetActive(false);
            currentTaskPrompt.SetActive(false);
            
        }
        else if (!Titles.activeSelf&&!textDisplay.activeSelf)
        {
           
            Titles.SetActive(true);
            currentTaskPrompt.SetActive(true);
            Transform[] _tr=Titles.GetComponentsInChildren<Transform>();
            if (GetComponentsInChildren<Transform>(true).Length < 1)
            {
                Debug.Log("没有子物体");
               
            }
            else
            {
                Debug.Log(_tr[1].name);
                Button button=_tr[1].GetComponent<Button>();
                UIInputController.Instance.SelectUI(button);
            }
            /*
            if(_tr!=null)
            {
                Debug.Log(_tr.name);
                Button _button= _tr.GetComponent<Button>();
                if (_button != null)
                {
                    UIInputController.Instance.SelectUI(_button);
                    Debug.Log(_button.name);
                }
                else
                {
                    Debug.Log("button false");
                }
            }
            else
            {
                Debug.Log("false");
            }
            */
        }
        else if(textDisplay.activeSelf)
        {
            textDisplay.SetActive(false);
        }
       
    }
    void UpdateCurrentTask(string _currentTask)
    {
        Debug.Log("Update text");
        contentText.text = _currentTask;
    }

    void UpdateCurrentTitle(string _title)
    {
        titleText.text = _title;
    }

    void UpdateCurrentTaskPrompt(string _currentTaskPrompt)
    {
        currentTaskText.text = _currentTaskPrompt;
    }

    public void AddTitleList(Title title)
    {
        titlePhone.titleList.Add(title);
        Button button = GameObject.Instantiate(title.button,Titles.transform);        
        button.onClick.AddListener(delegate () { UpdateCurrentTask(title.contentText); });
        button.onClick.AddListener(delegate () { UpdateCurrentTitle(title.titleText); });
        button.onClick.AddListener(DisplayText);
        TMPro.TextMeshProUGUI textMeshProUGUI = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = title.titleText;
        }
        else
        {
            Debug.Log("text is null");
        }
    }


    /// <summary>
    /// ButtonPrompt
    /// </summary>
    /// <param name="_prompt"></param>
    public void ButtonPrompt(string _prompt)
    {
        if(!buttonPrompt.activeSelf)
        buttonPrompt.SetActive(true);
        TMPro.TextMeshProUGUI textMeshPro=buttonPrompt.GetComponent<TMPro.TextMeshProUGUI>();
        textMeshPro.text= _prompt;
        StartCoroutine(nameof(_ButtonPrompt));
    }

    IEnumerator _ButtonPrompt()
    {
        yield return new WaitForSeconds(3f);
        buttonPrompt.SetActive(false);
    }

    
    public void TriigerBag()
    {
        if(Bag.activeSelf)
        {
            Bag.SetActive(false);
        }
        else if(!Bag.activeSelf)
        {
            Bag.SetActive(true);
        }
    }

    /// <summary>
    /// Additem
    /// </summary>
    /// <param name="_item"></param>
    public void AddItem(Item _item)
    {
        Button _button=GameObject.Instantiate(_item.bu_item,Bag.transform);
        if (_button.image != null)
        {
            _button.image = _item.im_item;
        }
        if(_item.item!=null&&ItemPosition!=null)
        {
            Debug.Log("Addlisten sc");
            _button.onClick.AddListener(delegate
            {
                //show item
                //Vector3 position = new Vector3(ItemPosition.position.x, ItemPosition.position.y, ItemPosition.position.z + 1f);

                Vector3 position = ItemPosition.position + ItemPosition.forward * 1;
                GameObject.Instantiate(_item.generatePlatform, position, Quaternion.identity);
                
            });
        }
    }

    void eventAddTR(Transform transform)
    {
        ItemPosition = transform;
        EventCenter.Instance.RemoveEventListener<Transform>("deliver_alias", eventAddTR);
    }
    

    void eventTransition_im()
    {
        transition.SetActive(true);      
        StartCoroutine(nameof(IEtransition));
    }

    IEnumerator IEtransition()
    {
        while(true)
        {
            if(endTran)
            {
                Image image=transition.GetComponent<Image>();
                if (image != null)
                    eventGraduallyShow(image);
                StopCoroutine(nameof(IEtransition));
            }
            yield return null;
        }
    }
}
