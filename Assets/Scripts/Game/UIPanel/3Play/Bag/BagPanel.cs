using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consts;


/// <summary>
/// 背包面板
/// </summary>
public class BagPanel : BasePanel
{
    /// <summary>
    /// MVC系统的视图名字
    /// </summary>
    public override string Name
    {
        get { return Consts.MVCView_Name.BagPanel; }
    }

    //存放格子的数据结构
    private List<ItemCell> itemCellsList = new List<ItemCell>();

    /// <summary>
    /// 由于没有挂组件 我们找不到它 所以在ShowMe的时候 手动找
    /// </summary>
    public Transform content;


    public override void ShowMe()
    {
        base.ShowMe();
        //把自己注册到MVC系统中
        GameMain.GetInstance().RegisterView(this);
        //初始化显示道具信息
        ToggleValueChange(E_Bag_Type.Item);
        //打开自己所对应的背包管理器
        BagMgr.GetInstance().Init();
    }

    public override void HideMe()
    {
        base.HideMe();
        //把自己从MVC系统中移除
        GameMain.GetInstance().RemoveView(this);
        //关掉自己所对应的背包管理器
        BagMgr.GetInstance().Remove();
    }
    #region  控件的事件监听

    /// <summary>
    /// 对按钮的事件监听
    /// </summary>
    /// <param name="btnName"></param>
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnClose":
                UIManager.GetInstance().HidePanel("BagPanel");
                break;
        }
    }

    /// <summary>
    /// 对Toggle的事件监听
    /// </summary>
    /// <param name="toggleName"></param>
    /// <param name="value"></param>
    protected override void OnValueChanged(string toggleName, bool value)
    {
        base.OnValueChanged(toggleName, value);
        switch (toggleName)
        {
            case "togItem":
                ToggleValueChange(E_Bag_Type.Item);
                break;
            case "togEquip":
                ToggleValueChange(E_Bag_Type.Equip);
                break;
            case "togGem":
                ToggleValueChange(E_Bag_Type.Gem);
                break;
        }
    }

    /// <summary>
    /// 页签的切换
    /// </summary>
    /// <param name="type"></param>
    void ToggleValueChange(E_Bag_Type type) 
    {
        //默认数值 是道具列表信息
        List<ItemInfo> tempInfo=GetModel<PlayerModel>().items;

        switch (type)
        {
            case Consts.E_Bag_Type.Equip:
                tempInfo = GetModel<PlayerModel>().equips;
                break;
            case Consts.E_Bag_Type.Gem:
                tempInfo = GetModel<PlayerModel>().gems;
                break;
            default:
                break;
        }

        //更新cv
        //删除旧的格子
        for (int i = 0; i < itemCellsList.Count; ++i)
        {
            Destroy(itemCellsList[i].gameObject);
        }
        itemCellsList.Clear();
        //更新新的格子
        //动态创建新的ItemCell预设体 并且把它放到List里面 方便下一次更新的时候 删除
        for (int i = 0; i < tempInfo.Count; ++i)
        {
            //实例化预设体 并且得到脚本
            ItemCell cell = ResMgr.GetInstance().Load<GameObject>
                ("UI/ItemCell").GetComponent<ItemCell>();
            //设置父对象
            cell.transform.SetParent(content);
            //初始化数据
            cell.InitInfo(tempInfo[i]);
            //放入列表内存中
            itemCellsList.Add(cell);
        }
    }

    #endregion

}
