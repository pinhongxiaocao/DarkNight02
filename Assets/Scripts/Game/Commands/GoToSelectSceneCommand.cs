using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;

public class GoToSelectSceneCommand : Controller
{
    public override void Execute(LBMVC_Args data = null)
    {
        Debug.Log("欢迎来到第二个角色选择场景");

        #region 处理上一个场景
        //先把旧场景的StartPanel面板关闭
        UIManager.GetInstance().HidePanel("StartPanel");
        //再关闭摄像机移动的监听
        Camera.main.GetComponent<IOtherObjectMono>().Remove();
        #endregion

        //调用场景管理器 去下一个场景
        ScenesMgr.GetInstance().LoadSceneAsyn("02_Select", null);

        #region 设置这一个场景

        //打开角色选择的UI面板 并且注册视图
        UIManager.GetInstance().ShowPanel<CharacterSelectPanel>("CharacterSelectPanel", E_UI_Layer.Mid,
            (panel) =>
            {
                //把这个UI面板注册到MVC系统当中
                RegisterView(panel);
            });

        //注册控制器
        RegisterController(Consts.MVCEvents_Name.GoToPlayScene,typeof(GoToPlaySceneCommand)); ;
        #endregion
    }
}
