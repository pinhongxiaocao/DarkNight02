using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;

public class GameMain : ApplicationBase<GameMain>
{
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
