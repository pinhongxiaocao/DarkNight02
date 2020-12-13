using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店售卖物品信息的数据
/// </summary>
[System.Serializable]
public class ShopItemInfo
{
    public int id;
    public PlayerItemInfo itemInfo;
    public int priceType;
    public int price;
    public string tips;
}
