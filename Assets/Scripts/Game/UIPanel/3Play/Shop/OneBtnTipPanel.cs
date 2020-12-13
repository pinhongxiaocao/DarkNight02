using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店的提示面板
/// </summary>
public class OneBtnTipPanel : BasePanel
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(string info) 
    {
        //修改提示的内容
        GetControl<Text>("txtInfo").text = info;
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnSure":
                UIManager.GetInstance().HidePanel("OneBtnTipPanel");
                break;
        }
    }
}
