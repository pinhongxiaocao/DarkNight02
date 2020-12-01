using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Consts;

public class QuestPanel : BasePanel
{
    public override string Name
    {
        get { return Consts.MVCView_Name.QuestPanel; }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //和背包面板一样 打开的时候在MVC系统中注册自己
        GameMain.GetInstance().RegisterView(this);
    }

    public override void HideMe()
    {
        base.HideMe();
        //和背包面板一样 关闭的时候在MVC系统中移除自己
        GameMain.GetInstance().RemoveView(this);
    }

    //对按钮的事件监听
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnAccept"://Acc按钮代表接受了任务
                EventCenter.GetInstance().EventTrigger(Consts.EventCenter_Events.AcceptQuest);
                break;
            case "btnOK"://OK按钮代表完成了任务
                EventCenter.GetInstance().EventTrigger(Consts.EventCenter_Events.CompleteQuest);
                break;
            case "btnCancel":
                break;
            case "btnClose":
                UIManager.GetInstance().HidePanel("QuestPanel");
                break;
        }
    }

    #region 任务面板特有 给外部调用的接口
    public void UpdateQuestPanel(E_Quest_Status status,string questTips) 
    {
        switch (status)
        {
            //处于等待状态未接收
            case E_Quest_Status.Waiting:
                GetControl<Button>("btnOK").gameObject.SetActive(false);
                GetControl<Button>("btnAccept").gameObject.SetActive(true);
                GetControl<Button>("btnCancel").gameObject.SetActive(true);

                GetControl<Text>("txtTips").text = questTips;
                break;
            case E_Quest_Status.Accepted:
                GetControl<Button>("btnOK").gameObject.SetActive(false);
                GetControl<Button>("btnAccept").gameObject.SetActive(false);
                GetControl<Button>("btnCancel").gameObject.SetActive(false);

                GetControl<Text>("txtTips").text = questTips;
                break;
            //没有做战斗 所以这里先不写
            case E_Quest_Status.Completed:
                break;
        }
    }

    #endregion
}
