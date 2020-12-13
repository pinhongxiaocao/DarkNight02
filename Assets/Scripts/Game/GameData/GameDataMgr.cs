using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : BaseManager<GameDataMgr>
{
    /// <summary>
    /// 把数据表类中的list转换成字典 方便我们以后查找数据
    /// key 每个物品的ID
    /// value 每个游戏物体
    /// 这个数据结构是存放了每一个游戏物体的信息 我们通常不会修改它
    /// </summary>
    private Dictionary<int, Item> itemsTableInfoDic = new Dictionary<int, Item>();

    /// <summary>
    /// 商店面板的数据
    /// </summary>
    public List<ShopItemInfo> shopItemInfosList = new List<ShopItemInfo>();


    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Init() 
    {
        ///加载道具表
        //把json表读取成字符串
        string jsonTableInfo = ResMgr.GetInstance().Load<TextAsset>("Json/ItemInfo").text;
        //把它序列化成我们要的类的格式
        ItemsTable itemsTable = JsonUtility.FromJson<ItemsTable>(jsonTableInfo);
        //初始化字典 把表的数据存起来
        for (int i = 0; i < itemsTable.info.Count; ++i)
        {
            //不断的存起来
            itemsTableInfoDic.Add(itemsTable.info[i].id, itemsTable.info[i]);
        }
        ///加载商店表
        //把json表读取成字符串
        string shopTableInfo = ResMgr.GetInstance().Load<TextAsset>("Json/ShopInfo").text;
        //把它序列化成我们要的类的格式
        ShopItemTable shopItemsTable = JsonUtility.FromJson<ShopItemTable>(shopTableInfo);
        //记录一下解析下来的商店数据
        shopItemInfosList = shopItemsTable.info;
    }

    /// <summary>
    /// 根据道具ID获取道具信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Item GetItemInfo(int id) 
    {
        if (itemsTableInfoDic.ContainsKey(id)) 
        {
            return itemsTableInfoDic[id];
        }
        return null;
    }
}
