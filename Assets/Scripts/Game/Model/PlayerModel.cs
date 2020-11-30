﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;

/// <summary>
/// 玩家拥有的基础道具信息
/// 就是ID和数量
/// </summary>
[System.Serializable]
public class ItemInfo
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
    public List<ItemInfo> items;
    public List<ItemInfo> equips;
    public List<ItemInfo> gems;

    //当前穿戴的装备
    public List<ItemInfo> nowEquips;

    /// <summary>
    /// TODO 
    /// 未来这里肯定是有参构造函数 根据data来设置姓名 ID 之类
    /// </summary>
    public PlayerModel()
    {
        //TODO

        money = 9999;
        gem = 100;

        items = new List<ItemInfo>()
        {
            //系统给它几瓶道具
            //10个红药 道具ID为3
            new ItemInfo(){id=3,num=10},
            //20个蓝药 道具ID为4
            new ItemInfo(){id=4,num=20}
        };
        equips = new List<ItemInfo>()
        {
            //1个匕首 道具ID为1
            new ItemInfo(){id=1,num=1}
        };
        gems = new List<ItemInfo>();

        nowEquips = new List<ItemInfo>();
    }
}