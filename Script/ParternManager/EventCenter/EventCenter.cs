using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IEventInfo//������һ���ǳ���Ȥ������ת��ԭ���Ӧ��
{ }
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}
public class EventCenter : Singleton<EventCenter>
{
    Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();



    /// <summary>
    /// ��Ӽ����¼�,��д������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="event_name"></param>
    /// <param name="action"></param>
    public void AddEventListener<T>(string event_name, UnityAction<T> action)
    {
        
        if (eventDic.ContainsKey(event_name))
        {
            (eventDic[event_name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(event_name, new EventInfo<T>(action));
        }
    }

    public void AddEventListener(string event_name, UnityAction action)
    {
       
        if (eventDic.ContainsKey(event_name))
        {
            (eventDic[event_name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(event_name, new EventInfo(action));
        }
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="event_name"></param>
    /// <param name="info"></param>

    public void EventTrigger<T>(string event_name, T info)
    {
        if (eventDic.ContainsKey(event_name))
        {
            (eventDic[event_name] as EventInfo<T>).actions.Invoke(info);
        }
    }
    public void EventTrigger(string event_name)
    {
       
        if (eventDic.ContainsKey(event_name))
        {
            (eventDic[event_name] as EventInfo).actions.Invoke();
        }
    }


    /// <summary>
    /// �Ƴ��¼�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="event_name"></param>
    /// <param name="action"></param>
    public void RemoveEventListener<T>(string event_name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(event_name))
        {
            (eventDic[event_name] as EventInfo<T>).actions -= action;
        }

    }

    public void RemoveEventListener(string event_name, UnityAction action)
    {
        if (eventDic.ContainsKey(event_name))
        {
            (eventDic[event_name] as EventInfo).actions -= action;
        }

    }

    /// <summary>
    /// ��������¼�
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
