using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public override string Name
    {
        get { return Consts.MVCView_Name.StartPanel; }
    }

    /// <summary>
    /// 添加按钮的事件监听
    /// </summary>
    /// <param name="btnName"></param>
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnNewGame":
                //发送MVC事件 去角色选择场景
                SendEvent(Consts.MVCEvents_Name.GoToSelectScene);
                break;
            case "btnLoadGame":
                //加载旧进度TODO
                break;
        }
    }

    //添加显示时候的逻辑
    public override void ShowMe()
    {
        base.ShowMe();
        //把两个按钮关掉
        GetControl<Button>("btnNewGame").gameObject.SetActive(false);
        GetControl<Button>("btnLoadGame").gameObject.SetActive(false);
        //开启他的检测
        GetControl<Text>("txtPressAnyKey").GetComponent<txtPressAnyKey>().Init();
    }

    
}
