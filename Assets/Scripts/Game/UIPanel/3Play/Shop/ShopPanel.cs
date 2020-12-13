using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店面板 目前不需要传送数据 
/// 不必注册到MVC系统中
/// </summary>
public class ShopPanel : BasePanel
{
    //直接拖 格子控件们的父对象
    public Transform content;

    #region 控件的事件监听
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnClose":
                UIManager.GetInstance().HidePanel("ShopPanel");
                break;
        }
    }
    #endregion

    public override void ShowMe()
    {
        base.ShowMe();
        //显示商店面板时 初始化商店面板中的售卖信息
        //根据商店数据 来初始化
        for (int i = 0; i < GameDataMgr.GetInstance().shopItemInfosList.Count; ++i)
        {
            //实例化一个 商店物品信息UI组合控件
            ShopCell cell = ResMgr.GetInstance().Load<GameObject>("UI/ShopCell").GetComponent<ShopCell>();
            //设置父对象
            cell.transform.SetParent(content);
            cell.transform.localScale = Vector3.one;
            //初始化商店售卖格子信息
            cell.InitInfo(GameDataMgr.GetInstance().shopItemInfosList[i]);
        }
    }
}
