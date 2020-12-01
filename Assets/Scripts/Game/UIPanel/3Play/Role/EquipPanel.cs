using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备面板 以后要涉及到数据交互
/// 所以放到MVC系统当中
/// </summary>
public class EquipPanel : BasePanel
{
    //对装备子物体的引用
    private ItemCell itemHead;
    private ItemCell itemNeck;
    private ItemCell itemWeapon;
    private ItemCell itemCloth;
    private ItemCell itemTrousers;
    private ItemCell itemShoes;

    public override string Name
    {
        get { return Consts.MVCView_Name.EquipPanel; }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //把自己注册到MVC框架中
        GameMain.GetInstance().RegisterView(this);
        //找到子物体的引用
        itemHead = transform.Find("itemHead").GetComponent<ItemCell>();
        itemNeck = transform.Find("itemNeck").GetComponent<ItemCell>();
        itemWeapon = transform.Find("itemWeapon").GetComponent<ItemCell>();
        itemCloth = transform.Find("itemCloth").GetComponent<ItemCell>();
        itemTrousers = transform.Find("itemTrousers").GetComponent<ItemCell>();
        itemShoes = transform.Find("itemShoes").GetComponent<ItemCell>();
        //更新自己
        UpdateEquipPanel();
    }

    public override void HideMe()
    {
        base.HideMe();
        //把自己从MVC框架中移除
        GameMain.GetInstance().RemoveView(this);
    }

    #region 事件监听
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnClose":
                UIManager.GetInstance().HidePanel("EquipPanel");
                break;

        }
    }
    #endregion

    /// <summary>
    /// 更新装备面板
    /// </summary>
    public void UpdateEquipPanel() 
    {
        //获得现在装备的物体
        List<PlayerItemInfo> nowEquips = GetModel<PlayerModel>().nowEquips;
        Item itemInfo;
        //初始化置空
        itemHead.InitInfo(null);
        itemNeck.InitInfo(null);
        itemWeapon.InitInfo(null);
        itemCloth.InitInfo(null);
        itemTrousers.InitInfo(null);
        itemShoes.InitInfo(null);
        for (int i = 0; i < nowEquips.Count; i++)
        {
            //根据id 来查一下获取了哪一个格子
            itemInfo = GameDataMgr.GetInstance().GetItemInfo(nowEquips[i].id);
            //然后根据拿到了格子 来判断应该更新哪一个
            switch (itemInfo.equipType)
            {
                case (int)Consts.E_Item_Type.Head:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
                case (int)Consts.E_Item_Type.Neck:
                    itemNeck.InitInfo(nowEquips[i]);
                    break;
                case (int)Consts.E_Item_Type.Weapon:
                    itemWeapon.InitInfo(nowEquips[i]);
                    break;
                case (int)Consts.E_Item_Type.Cloth:
                    itemCloth.InitInfo(nowEquips[i]);
                    break;
                case (int)Consts.E_Item_Type.Trousers:
                    itemTrousers.InitInfo(nowEquips[i]);
                    break;
                case (int)Consts.E_Item_Type.Shoes:
                    itemShoes.InitInfo(nowEquips[i]);
                    break;
            }
        }
    }
}
