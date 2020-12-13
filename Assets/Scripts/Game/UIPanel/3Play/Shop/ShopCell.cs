using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 格子复合组件 不用加入到MVC系统中
/// </summary>
public class ShopCell : BasePanel
{
    private ShopItemInfo info;

    /// <summary>
    /// 初始化 商店物品 复合控件的显示信息
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(ShopItemInfo info)
    {
        this.info = info;

        //根据售卖的道具id 得到道具表信息
        Item item = GameDataMgr.GetInstance().GetItemInfo(info.itemInfo.id);
        //图标
        GetControl<Image>("imgIcon").sprite = ResMgr.GetInstance().Load<Sprite>("Icon/" + item.icon);
        //个数
        GetControl<Text>("txtNum").text = info.itemInfo.num.ToString();
        //名字
        GetControl<Text>("txtName").text = item.name;
        //图标
        GetControl<Image>("imgPType").sprite = ResMgr.GetInstance().Load<Sprite>("Icon/" +
            (info.priceType == 1 ? "5" : "6"));
        //价格
        GetControl<Text>("txtPrice").text = info.price.ToString();
        //描述信息
        GetControl<Text>("txtTips").text = info.tips;
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnBuy":
                Buy();
                break;
        }
    }

    void Buy()
    {
        //先获取玩家数据
        PlayerModel playerModel = GameMain.GetInstance().GetModel<PlayerModel>();
        //首先判断货币够不够
        if (info.priceType == 1 &&          //金币够不够
            playerModel.money >= info.price)
        {
            //添加物品给玩家
            playerModel.AddItem(info.itemInfo);
            //购买成功要去 减少货币
            playerModel.ChangeMoney(-info.price);
            //打开提示面板 告诉他购买成功
            TipMgr.GetInstance().ShowOneBtnTip("购买成功");
        }
        else if (info.priceType == 2 &&     //宝石够不够
        GameMain.GetInstance().GetModel<PlayerModel>().money >= info.price)
        {
            //添加物品给玩家
            playerModel.AddItem(info.itemInfo);
            //购买成功要去 减少货币
            playerModel.ChangeGem(-info.price);
            //打开提示面板 告诉他购买成功
            TipMgr.GetInstance().ShowOneBtnTip("购买成功");
        }
        else //货币不足
        {
            //打开提示面板 告诉他不足
            TipMgr.GetInstance().ShowOneBtnTip("金额不足");
        }
    }
}
