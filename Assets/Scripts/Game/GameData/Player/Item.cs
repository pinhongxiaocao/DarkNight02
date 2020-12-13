using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 最基本的物品数据结构  
/// 对应着一个物体
/// </summary>
[System.Serializable]
public class Item 
{
    public int id;
    public string name;
    public string icon;
    public int type;
    public int equipType;
    public int price;
    public string tips;
}
