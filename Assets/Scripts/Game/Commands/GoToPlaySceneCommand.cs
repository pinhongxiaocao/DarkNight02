using System.Collections;
using System.IO;
using System.Collections.Generic;
using MVC;
using UnityEngine;

/// <summary>
/// 去第三个场景的
/// </summary>
public class GoToPlaySceneCommand : Controller
{
    public override void Execute(LBMVC_Args data = null)
    {
        #region 先解决上一个场景
        //关掉面板
        UIManager.GetInstance().HidePanel("CharacterSelectPanel");
        //关掉角色生成器
        GameObject.Find("CharacterCreator").GetComponent<IOtherObjectMono>().Remove();
        //调用场景管理器 
        ScenesMgr.GetInstance().LoadSceneAsyn("03_Play", null);
        #endregion

        #region 处理这一个场景
        //打开主面板 并且注册它
        UIManager.GetInstance().ShowPanel<MainPanel>("MainPanel", E_UI_Layer.Mid,
            (panel) =>
            {
                //把这个UI面板注册到MVC系统当中
                RegisterView(panel);
            });

        //初始化 角色信息 数据存在本地
        InitPlayerInfo();
        #endregion
    }

    /// <summary>
    /// 初始化玩家信息
    /// </summary>
    void InitPlayerInfo()
    {
        PlayerModel model = GetModel<PlayerModel>();
        //从视图层来找是否有玩家数据
        if (null != model)
        {

        }
        else
        {
            //默认构造一个数据 并且注册它
            RegisterModel(new PlayerModel());
        }
    }
}
