using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 可以提供给外部帧更新
/// 可以提供给外部协程
/// </summary>
public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController controller;
    public MonoMgr()
    {
        //保证了MonoController的唯一性 单例模式第一次的时候才new
        GameObject obj = new GameObject("MonoController");
        controller=obj.AddComponent<MonoController>();
      
    }
    /// <summary>
    /// 给外部提供的添加帧更新事件
    /// </summary>
    /// <param name="func"></param>
    public void AddUpdateListener(UnityAction func)
    {
        controller.AddUpdateListener(func);
    }

    /// <summary>
    /// 给外部提供的移除帧更新事件
    /// </summary>
    /// <param name="func"></param>
    public void RemoveUpdateListener(UnityAction func)
    {
        controller.RemoveUpdateListener(func);
    }

    //直接封装一次
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
}
