using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagMgr : BaseManager<BagMgr>
{
    //当前拖动着的格子(从背包中出来)
    private ItemCell nowSelectItem;
    //当前鼠标进入的格子(准备进入到装备中去)
    private ItemCell nowInItem;
    //当前选中的装备 图片信息(在中途中需要被创建要被拖的图片)
    private Image nowDragingItemImg;

    /// <summary>
    /// 初始化背包管理器
    /// </summary>
    public void Init() 
    {
        //开始监听事件们
        EventCenter.GetInstance().AddEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellBeginDrag, BeginDragItemCell);
        EventCenter.GetInstance().AddEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellDrag, DragItemCell);
        EventCenter.GetInstance().AddEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellEndDrag, EndDragItemCell);
        EventCenter.GetInstance().AddEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellEnter, EnterItemCell);
        EventCenter.GetInstance().AddEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellExit, ExitItemCell);
    }
    /// <summary>
    /// 关闭背包管理器
    /// </summary>
    public void Remove() 
    {
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellBeginDrag, BeginDragItemCell);
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellDrag, DragItemCell);
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellEndDrag, EndDragItemCell);
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellEnter, EnterItemCell);
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellExit, ExitItemCell);
    }

    #region 监听的事件们

    private void BeginDragItemCell(ItemCell itemCell) 
    {
        30.31
        //利用缓存池 创建一张图片
        PoolMgr.GetInstance().GetObj("UI/imgIcon")
    }

    private void DragItemCell(ItemCell itemCell)
    {

    }

    private void EndDragItemCell(ItemCell itemCell)
    {

    }

    private void EnterItemCell(ItemCell itemCell)
    {
        //显示提示面板
        UIManager.GetInstance().ShowPanel<TipsPanel>("TipsPanel", E_UI_Layer.Top, (panel) =>
        {
            //异步加载结束后 去设置位置 去设置信息
            //更新信息
            panel.InitInfo(itemCell.ItemInfo);
            //更新位置
            panel.transform.position = itemCell.imgBK.transform.position;
        });
    }
    private void ExitItemCell(ItemCell itemCell)
    {
        //隐藏提示面板
        UIManager.GetInstance().HidePanel("TipsPanel");
    }

    #endregion
}
