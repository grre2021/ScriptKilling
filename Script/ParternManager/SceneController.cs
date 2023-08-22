using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public GameObject[] donDestory;
    Dictionary<string, int> dicScene = new Dictionary<string, int>();



    private void Start()
    {
        foreach(var d in donDestory)
        {
            DontDestroyOnLoad(d);
        }       
        SceneManager.sceneLoaded += OnSceneLoad;
        EventCenter.Instance.AddEventListener<string>("changeload", _ChangerScene);
    }
    public void ChangerScene(string sceneName)
    {
        Debug.Log("change scene");
        EventCenter.Instance.EventTrigger("StartChangeScene");
        SceneManager.LoadScene(sceneName);
    }

    private void _ChangerScene(string sceneName)
    {
        Debug.Log("change scene");
        EventCenter.Instance.EventTrigger("StartChangeScene");
        SceneManager.LoadScene(sceneName);
    }
    void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        EventCenter.Instance.EventTrigger("OnSceneLoad");
        Debug.Log("onsceneload");

    }
}
