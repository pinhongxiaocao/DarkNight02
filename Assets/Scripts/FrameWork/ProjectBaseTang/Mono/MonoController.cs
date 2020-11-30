using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Mono 管理者
public class MonoController :MonoBehaviour
{
    private event UnityAction updateEvent;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (updateEvent != null)
        {
            updateEvent();
        }
    }
    /// <summary>
    /// 给外部提供的添加帧更新事件
    /// </summary>
    /// <param name="func"></param>
    public void AddUpdateListener(UnityAction func)
    {
        updateEvent += func;
    }

    /// <summary>
    /// 给外部提供的移除帧更新事件
    /// </summary>
    /// <param name="func"></param>
    public void RemoveUpdateListener(UnityAction func)
    {
        updateEvent -= func;
    }
}
