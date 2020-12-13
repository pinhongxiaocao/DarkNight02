using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consts;
using LB_MVC;

/// <summary>
/// 玩家拥有的基础道具信息
/// 就是ID和数量
/// </summary>
[System.Serializable]
public class PlayerItemInfo
{
    public int id;
    public int num;
}

public class PlayerModel : Model
{
    /// <summary>
    /// MVC系统中 这份数据的名字
    /// </summary>
    public override string Name
    {
        get { return Consts.MVCModel_Name.PlayerModel; }
    }

    public string name;
    public int lev;
    public int money;
    public int gem;
    public int pro;
    //主要用下面这三个
    public List<PlayerItemInfo> items;
    public List<PlayerItemInfo> equips;
    public List<PlayerItemInfo> gems;

    //当前穿戴的装备
    public List<PlayerItemInfo> nowEquips;

    //玩家所拥有的任务
    public Dictionary<string, Quest> questList = new Dictionary<string, Quest>();

    /// <summary>
    /// TODO 
    /// 未来这里肯定是有参构造函数 根据data来设置姓名 ID 之类
    /// </summary>
    public PlayerModel()
    {
        //TODO

        money = 9999;
        gem = 100;

        items = new List<PlayerItemInfo>()
        {
        };
        equips = new List<PlayerItemInfo>()
        {
            //1个匕首 道具ID为1
            new PlayerItemInfo(){id=1,num=1}
        };
        gems = new List<PlayerItemInfo>();

        nowEquips = new List<PlayerItemInfo>();
    }


    /// <summary>
    /// 添加物品 给玩家
    /// </summary>
    public void AddItem(PlayerItemInfo info)
    {
        Item item = GameDataMgr.GetInstance().GetItemInfo(info.id);

        switch (item.type)
        {
            //道具
            case (int)E_Bag_Type.Item:
                items.Add(info);
                break;
            //装备
            case (int)E_Bag_Type.Equip:
                equips.Add(info);
                break;
            //宝石
            case (int)E_Bag_Type.Gem:
                gems.Add(info);
                break;
        }
    }

    /// <summary>
    /// 金钱交互
    /// </summary>
    public void ChangeMoney(int money)
    {
        //判断钱够不够减 避免减成负数
        if (money < 0 && this.money < money)
        {
            return;
        }
        //改变玩家的钱
        this.money += money;
    }

    /// <summary>
    /// 宝石交互
    /// </summary>
    public void ChangeGem(int gem)
    {
        //判断钱够不够减 避免减成负数
        if (gem < 0 && this.money < gem)
        {
            return;
        }
        //改变玩家的钱
        this.money += gem;
    }
}
