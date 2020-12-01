using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LB_MVC;

public class GameMain : ApplicationBase<GameMain>
{
    /// <summary>
    /// 注册视图
    /// </summary>
    /// <param name="view"></param>
    public void RegisterView(View view)
    {
        LB_MVC.MVC.RegisterView(view);
    }

    /// <summary>
    /// 移除视图
    /// </summary>
    public void RemoveView(View view)
    {
        LB_MVC.MVC.RemoveView(view);
    }

    /// <summary>
    /// 获取模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>();
    }


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        //先注册第一个(游戏开始)的控制器
        RegisterController(Consts.MVCEvents_Name.StartGame, typeof(StartGameCommand));

        //发送开启游戏的事件
        SendEvent(Consts.MVCEvents_Name.StartGame);

        //初始化数据
        GameDataMgr.GetInstance().Init();
    }
}
