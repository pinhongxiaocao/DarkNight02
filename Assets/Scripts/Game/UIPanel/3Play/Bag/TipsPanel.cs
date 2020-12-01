using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 道具 装备 宝石 详细信息面板 由于不获取数据相关 没有必要放进MVC系统中
/// </summary>
public class TipsPanel : BasePanel
{
    /// <summary>
    /// 初始化tips 面板信息
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(PlayerItemInfo info) 
    {
        //根据道具表中的信息 来更新对象
        Item itemData = GameDataMgr.GetInstance().GetItemInfo(info.id);
        //使用我们道具表中的数据
        //图标 用道具表中的信息来加载(公用的)
        GetControl<Image>("imgIcon").sprite = ResMgr.GetInstance().Load<Sprite>
            ("Icon/" + itemData.icon);
        //数量 用玩家的数据来加载 每一个人是不一样的
        GetControl<Text>("txtNum").text = info.num.ToString();
        //名字 从道具表中加载
        GetControl<Text>("txtName").text = itemData.name;
        //描述 从道具表中加载
        GetControl<Text>("txtTips").text = itemData.tips;
    }
}
