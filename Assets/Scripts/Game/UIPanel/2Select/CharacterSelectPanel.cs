using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;

public class CharacterSelectPanel : BasePanel
{
    #region MVC相关
    /// <summary>
    /// MVC 视图的名字
    /// </summary>
    public override string Name
    {
        get { return Consts.MVCView_Name.CharacterSelectPanel; }
    }

    /// <summary>
    /// 处理MVC的消息
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public override void HandleEvent(string eventName, LBMVC_Args data = null)
    {

    }
    #endregion

    /// <summary>
    /// 添加对按钮的监听
    /// </summary>
    /// <param name="btnName"></param>
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            //向前选择角色的按钮
            case "btnPrev":
                EventCenter.GetInstance().EventTrigger<int>
                    (Consts.EventCenter_Events.SwitchCharacter, -1);
                break;
            //向后选择角色的按钮
            case "btnNext":
                EventCenter.GetInstance().EventTrigger<int>
                     (Consts.EventCenter_Events.SwitchCharacter, 1);
                break;
            //确定建立角色的按钮    
            case "btnOK":
                //确定之后 需要跳转到第三个场景里面
                //发送消息 跳转到游戏场景 
                //TODO 其实这里是要参数的 比如玩家数据什么的
                SendEvent(Consts.MVCEvents_Name.GoToPlayScene,
                   new GoToPlaySceneArgs());
                break;
        }
    }

    /// <summary>
    /// 打开这个面板的时候
    /// </summary>
    public override void ShowMe()
    {
        base.ShowMe();

        //找到初始化角色生成器 启动它
        GameObject.Find("CharacterCreator").GetComponent<IOtherObjectMono>().Init();
    }
}
