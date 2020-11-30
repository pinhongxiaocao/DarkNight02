using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// </summary>
public class ScenesMgr : BaseManager<ScenesMgr>
{
    /// <summary>
    /// 切换场景 同步加载
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name,UnityAction func)
    {
        //场景同步加载
        SceneManager.LoadScene(name);
        //加载完成后才会执行 func
        func();
    }

    /// <summary>
    /// 切换场景 异步加载
    /// </summary>
    /// <param name="name"></param>
    public void LoadSceneAsyn(string name, UnityAction func)
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name,func));
    }
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction func)
    {
        AsyncOperation ao= SceneManager.LoadSceneAsync(name);
        //可以得到场景加载的进度
        while (ao.isDone)
        {
            //事件中心向外分发进度
            EventCenter.GetInstance().EventTrigger("进度条更新", ao.progress);
            //在这里去更新进度条
            yield return ao;
        }
        //加载完成后才会执行 func 不为空才执行
        func?.Invoke();
    }
}
