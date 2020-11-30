using System.Collections;
using System.Collections.Generic;
using MVC;
using UnityEngine;

public class StartGameCommand : Controller
{
    public override void Execute(LBMVC_Args data = null)
    {

        //打开开始游戏的UI面板 
        //因为异步加载 这个面板(视图)只能在回调里面注册
        UIManager.GetInstance().ShowPanel<StartPanel>("StartPanel", E_UI_Layer.Mid,
            (panel) =>
            {
                //把这个UI面板注册到MVC系统当中
                RegisterView(panel);
            });

        //去告诉摄像机 让他初始化
        Camera.main.GetComponent<MovieCamera>().Init();
        //然后注册下一个加载角色选择场景控制器
        RegisterController(Consts.MVCEvents_Name.GoToSelectScene,typeof(GoToSelectSceneCommand));
    }
}
