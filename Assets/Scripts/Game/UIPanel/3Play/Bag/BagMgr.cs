using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consts;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagMgr : BaseManager<BagMgr>
{
    //当前拖动着的格子(从背包中出来)
    private ItemCell nowSelectItem;
    //当前鼠标进入的格子(准备进入到装备中去)
    private ItemCell nowInItem;
    //当前选中的装备 图片信息(在中途中需要被创建要被拖的图片)
    private Image nowDragingItemImg;

    //是否拖动中
    private bool isDraging = false;

    /// <summary>
    /// 初始化背包管理器
    /// </summary>
    public void Init()
    {
        //开始监听事件们
        EventCenter.GetInstance().AddEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellBeginDrag, BeginDragItemCell);
        EventCenter.GetInstance().AddEventListener<BaseEventData>(Consts.EventCenter_Events.ItemCellDrag, DragItemCell);
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
        EventCenter.GetInstance().RemoveEventListener<BaseEventData>(Consts.EventCenter_Events.ItemCellDrag, DragItemCell);
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellEndDrag, EndDragItemCell);
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellEnter, EnterItemCell);
        EventCenter.GetInstance().RemoveEventListener<ItemCell>(Consts.EventCenter_Events.ItemCellExit, ExitItemCell);
    }

    /// <summary>
    /// 更换装备
    /// </summary>
    public void ChangeEquip()
    {
        //从背包中拖装备
        if (nowSelectItem.type == E_Item_Type.Bag)
        {
            //存在进入的格子 并且 格子并不是背包中的格子
            if (nowInItem != null && nowInItem.type != E_Item_Type.Bag)
            {
                //读表
                Item info = GameDataMgr.GetInstance().GetItemInfo(nowSelectItem.ItemInfo.id);
                //装备交换
                //1.判断格子类型和装备类型 是否一致
                if ((int)nowInItem.type == info.equipType)
                {
                    //2判断 装备栏是不是空的 如果是直接放
                    if (nowInItem.ItemInfo == null)
                    {
                        //1直接装备 2直接从背包中移除 
                        GameMain.GetInstance().GetModel<PlayerModel>().nowEquips.Add(nowSelectItem.ItemInfo);
                        GameMain.GetInstance().GetModel<PlayerModel>().equips.Remove(nowSelectItem.ItemInfo);

                    }
                    else //交换装备 比如一把铁剑和一把铜剑
                    {
                        //1把当前的旧装备移除 2 把新装备加上
                        GameMain.GetInstance().GetModel<PlayerModel>().nowEquips.Remove(nowInItem.ItemInfo);
                        GameMain.GetInstance().GetModel<PlayerModel>().nowEquips.Add(nowSelectItem.ItemInfo);
                        //3把当前选择的格子从背包中移除 4把旧的装备从背包中加上
                        GameMain.GetInstance().GetModel<PlayerModel>().equips.Remove(nowSelectItem.ItemInfo);
                        GameMain.GetInstance().GetModel<PlayerModel>().equips.Add(nowInItem.ItemInfo);
                    }
                    //更新背包
                    UIManager.GetInstance().GetPanel<BagPanel>("BagPanel").ToggleValueChange(E_Bag_Type.Equip);
                    //更新装备面板
                    UIManager.GetInstance().GetPanel<EquipPanel>("EquipPanel").UpdateEquipPanel();
                    //保存数据 TODO

                }
            }
        }
        else//从装备栏往外拖 
        {
            
            //当前从角色装备栏 拖出一个装备 并且没有进入任何装备
            if (nowInItem == null||nowInItem.type!=E_Item_Type.Bag) 
            {
                //就把要下的装备从Equip上删除 在背包中添加
                GameMain.GetInstance().GetModel<PlayerModel>().nowEquips.Remove(nowSelectItem.ItemInfo);
                GameMain.GetInstance().GetModel<PlayerModel>().equips.Add(nowSelectItem.ItemInfo);

                //更新背包
                UIManager.GetInstance().GetPanel<BagPanel>("BagPanel").ToggleValueChange(E_Bag_Type.Equip);
                //更新装备面板
                UIManager.GetInstance().GetPanel<EquipPanel>("EquipPanel").UpdateEquipPanel();
                //保存数据 TODO
            }
            //如果想拖进背包面板
            else if (nowInItem == null || nowInItem.type == E_Item_Type.Bag) 
            {
                //读表
                Item info = GameDataMgr.GetInstance().GetItemInfo(nowSelectItem.ItemInfo.id);
                //如果装备类型一样 我们就替换
                if ((int)nowSelectItem.type == info.equipType) 
                {
                    GameMain.GetInstance().GetModel<PlayerModel>().nowEquips.Remove(nowSelectItem.ItemInfo);
                    GameMain.GetInstance().GetModel<PlayerModel>().nowEquips.Add(nowInItem.ItemInfo);

                    GameMain.GetInstance().GetModel<PlayerModel>().equips.Remove(nowInItem.ItemInfo);
                    GameMain.GetInstance().GetModel<PlayerModel>().equips.Add(nowSelectItem.ItemInfo);
                }
                //更新背包
                UIManager.GetInstance().GetPanel<BagPanel>("BagPanel").ToggleValueChange(E_Bag_Type.Equip);
                //更新装备面板
                UIManager.GetInstance().GetPanel<EquipPanel>("EquipPanel").UpdateEquipPanel();
                //保存数据 TODO
            }
        }
    }

    #region 监听的事件们
    private void BeginDragItemCell(ItemCell itemCell)
    {
        //一开始拖动直接隐藏 TipsPanel
        UIManager.GetInstance().HidePanel("TipsPanel");
        isDraging = true;

        //记录当前选中的格子
        nowSelectItem = itemCell;

        //利用缓存池 创建一张图片
        PoolMgr.GetInstance().GetObj("UI/imgIcon", (obj) =>
         {
             //获取组件
             nowDragingItemImg = obj.GetComponent<Image>();
             //修改图片
             nowDragingItemImg.sprite = itemCell.imgIcon.sprite;

             //设置父对象
             nowDragingItemImg.transform.SetParent(UIManager.GetInstance().canvas);
             nowDragingItemImg.transform.localScale = Vector3.one;

             //如果异步加载 拖动的非常快(拖动结束 图还没创建出来)那么就移除它
             if (!isDraging)
             {
                 PoolMgr.GetInstance().PushObj(nowDragingItemImg.name, nowDragingItemImg.gameObject);
                 nowDragingItemImg = null;
             }
         });
    }

    private void DragItemCell(BaseEventData eventData)
    {
        //由于异步加载 很有可能到这里图片还没有加载出来
        if (nowDragingItemImg == null)
            return;
        //拖动中 更新这个图片的位置
        //把鼠标位置 转换到UI相关的位置 让图片跟着鼠标移动
        Vector2 localPos;
        //用于坐标转换的API
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UIManager.GetInstance().canvas,//希望得到坐标结果的父对象
            (eventData as PointerEventData).position,//相当于 鼠标位置
            (eventData as PointerEventData).pressEventCamera,//相当于UI摄像机
            out localPos);
        nowDragingItemImg.transform.localPosition = localPos;
    }

    private void EndDragItemCell(ItemCell itemCell)
    {
        isDraging = false;

        ChangeEquip();

        //结束拖动的时候 置空
        nowSelectItem = null;
        nowInItem = null;

        if (nowDragingItemImg == null)
            return;
        //结束拖动 移除这个图片
        PoolMgr.GetInstance().PushObj(nowDragingItemImg.name, nowDragingItemImg.gameObject);
        nowDragingItemImg = null;
    }

    private void EnterItemCell(ItemCell itemCell)
    {
        //如果在拖 也不能显示
        if (isDraging)
        {
            //记录现在要进入的格子 是我这个
            nowInItem = itemCell;
            return;
        }

        //判空不显示提示面板
        if (itemCell.ItemInfo == null)
        {
            return;
        }

        //显示提示面板
        UIManager.GetInstance().ShowPanel<TipsPanel>("TipsPanel", E_UI_Layer.Top, (panel) =>
        {
            //异步加载结束后 去设置位置 去设置信息
            //更新信息
            panel.InitInfo(itemCell.ItemInfo);
            //更新位置
            panel.transform.position = itemCell.imgBK.transform.position;

            //如果面板异步加载结束 发现已经开始拖动了 直接隐藏TipsPanel
            if (isDraging)
                UIManager.GetInstance().HidePanel("TipsPanel");
        });
    }
    private void ExitItemCell(ItemCell itemCell)
    {
        //隐藏提示面板
        if (isDraging)
        {
            //拖动中离开格子 清空记录要进的格子物体
            nowInItem = null;
            return;
        }

        //判空不显示提示面板
        if (itemCell.ItemInfo == null)
        {
            return;
        }
        UIManager.GetInstance().HidePanel("TipsPanel");
    }

    #endregion
}
