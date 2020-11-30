using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 按下任何键就开始的UI
/// </summary>
public class txtPressAnyKey : MonoBehaviour,IOtherObjectMono
{
    /// <summary>
    /// 表示是否有任何键按下 初始化为false
    /// </summary>
    private bool isAnyKeyDown = false;

    /// <summary>
    /// 初始化这个控件
    /// </summary>
    public void Init() 
    {
        MonoMgr.GetInstance().AddUpdateListener(UpdateCheckKey);
    }

    /// <summary>
    /// 当被销毁的时候 通常是场景切换
    /// </summary>
    public void Remove() 
    {
        MonoMgr.GetInstance().RemoveUpdateListener(UpdateCheckKey);
    }

    private void UpdateCheckKey()
    {
        //如果与他按下了任何按键
        if (Input.anyKey && isAnyKeyDown == false) 
        {
            //显示按钮
            ShowButton();
        }
    }

    void ShowButton() 
    {
        //找到控件并且关闭显隐
        StartPanel panel= UIManager.GetInstance().GetPanel<StartPanel>("StartPanel");
        panel.GetControl<Button>("btnNewGame").gameObject.SetActive(true);
        panel.GetControl<Button>("btnLoadGame").gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        //由于这个功能只要一次 直接移除监听
        Remove();
    }
}
