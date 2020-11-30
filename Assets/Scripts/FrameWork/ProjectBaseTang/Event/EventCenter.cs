using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

public class EventInfo<T>: IEventInfo
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

/// <summary>
/// 事件中心
/// 采用空接口来让一个事件可以存不同的参数 避免装拆箱
/// 注意(弊端):一个事件不能被多个不同参数(个数和种类)的函数监听 
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    //key-------事件的名字
    //value-----监听这个事件的委托函数们
    //1就用一个泛型object 如果没有必要的参数就随便传(过时 存在装拆箱)
    //2得让这个类继承这个接口才可以放进这个字典 里式转换原则(现取)
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();


    #region 添加事件监听
    /// <summary>
    /// 添加监听事件
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">准备用来处理事件的委托函数</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        //有没有对应的事件监听
        if (eventDic.ContainsKey(name))
        {  
            //有的话继续加进去 如果之前他添加了一个不同类型的委托 他会报错
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            //没有就新建一个存起来
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    //重载不需要参数的版本
    public void AddEventListener(string name, UnityAction action)
    {
        //有没有对应的事件监听
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }
    #endregion

    #region 移除事件监听
    /// <summary>
    /// 移除对应的事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">对应之前添加的委托函数</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name]as EventInfo<T>).actions -= action;
        }
 
    }
    //重载不需要参数的版本
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }

    }
    #endregion

    #region 事件触发
    /// <summary>
    /// 事件触发
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">要触发的事件</param>
    /// <param name="info">传递的参数</param>
    public void EventTrigger<T>(string name,T info)
    {
        //有没有对应的事件监听
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo<T>).actions!=null)
            {
                (eventDic[name]as EventInfo<T>).actions.Invoke(info);// 执行委托
            }
        }
        else 
        {
            Debug.Log("事件中心报错:" + "没有这个事件");
        }
    }
    //重载不需要参数的版本
    public void EventTrigger(string name)
    {
        //有没有对应的事件监听
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
            {
                (eventDic[name] as EventInfo).actions.Invoke();// 执行委托
            }
        }
        else
        {
            Debug.LogError("事件中心报错:" + "没有这个事件");
        }
    }

    #endregion

    public void Clear()
    {
        eventDic.Clear();
    }
}
