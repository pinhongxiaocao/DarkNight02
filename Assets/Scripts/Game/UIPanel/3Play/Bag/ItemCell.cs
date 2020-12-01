using System;
using System.Collections;
using System.Collections.Generic;
using Consts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 每一个格子的复合组件 
/// 也要继承MVC中的View
/// 但是目的只是拿数据而已 不用把自己存入MVC系统
/// </summary>
public class ItemCell : BasePanel
{
    [HideInInspector] public Image imgBK;
    [HideInInspector] public Image imgIcon;

    protected override void Awake()
    {
        base.Awake();
        //一开始就把图标隐藏 初始化有了信息后 有了图标 才显示
        GetControl<Image>("imgIcon").gameObject.SetActive(false);
        //得到imgBK
        imgBK = GetControl<Image>("imgBK");
        //得到imgIcon
        imgIcon = GetControl<Image>("imgIcon");
        //初始化EventTrigger事件监听
        InitEventTrigger();
    }


    //基础道具信息 复合数据结构
    //1 id  2num
    private PlayerItemInfo itemInfo;
    public PlayerItemInfo ItemInfo { get => itemInfo;}
    public Consts.E_Item_Type type;



    /// <summary>
    /// 初始化格子信息
    /// </summary>
    public void InitInfo(PlayerItemInfo info)
    {
        this.itemInfo = info;
        //如果传入的是空 隐藏图片
        if (info == null) 
        {
            imgIcon.gameObject.SetActive(false);
            return;
        }
        //根据道具的信息 来更新对象
        Item itemData = GameDataMgr.GetInstance().GetItemInfo(info.id);
        //只要设置了信息 就初始化
        GetControl<Image>("imgIcon").gameObject.SetActive(true);

        //使用我们道具表中的数据
        //图标 用数据管理器的信息来加载(公用的)
        GetControl<Image>("imgIcon").sprite = ResMgr.GetInstance().Load<Sprite>
            ("Icon/" + itemData.icon);
        //数量 用玩家的数据来加载 每一个人是不一样的
        if(type==E_Item_Type.Bag)//只有背包中的物体 才会有数量
        GetControl<Text>("txtNum").text = info.num.ToString();
        //来判断道具类型 
        if (itemData.type == (int)E_Bag_Type.Equip) 
        {
            //如果是装备 那它就可以被拖 开启这个事件
            OpenDragEvent();
        }
    }

    #region EventTrigger相关
    /// <summary>
    /// 初始化EventTrigger 移入 移出 拖拽
    /// </summary>
    private void InitEventTrigger()
    {
        //首先要给它加一个EventTrigger组件
        GetControl<Image>("imgBK").gameObject.AddComponent<EventTrigger>();
        //肯定要开启这个拖入拖出的事件
        OpenEnterExitEvent();
    }

    /// <summary>
    /// 开启进入 移出相关
    /// </summary>
    private void OpenEnterExitEvent()
    {
        ///EventTrigger中的 进入离开
        //拿到Trigger
        EventTrigger trigger = GetControl<Image>("imgBK").gameObject.GetComponent<EventTrigger>();

        //申明一个 鼠标进入的事件类对象
        EventTrigger.Entry enter = new EventTrigger.Entry();
        //设置事件ID
        enter.eventID = EventTriggerType.PointerEnter;
        //设置回调函数
        enter.callback.AddListener(EnterItemCell);
        //把它加到事件列表里面去
        trigger.triggers.Add(enter);

        //申明一个 鼠标离开的事件类对象
        EventTrigger.Entry exit = new EventTrigger.Entry();
        //设置事件ID
        exit.eventID = EventTriggerType.PointerExit;
        //设置回调函数
        exit.callback.AddListener(ExitItemCell);
        //把它加到事件列表里面去
        trigger.triggers.Add(exit);
    }

    /// <summary>
    /// 开启拖动相关
    /// </summary>
    private void OpenDragEvent()
    {
        //拿到Trigger
        EventTrigger trigger = GetControl<Image>("imgBK").gameObject.GetComponent<EventTrigger>();
        ///EventTrigger中的 拖动
        //开始拖动事件
        EventTrigger.Entry beginDrag = new EventTrigger.Entry();
        beginDrag.eventID = EventTriggerType.BeginDrag;
        beginDrag.callback.AddListener(BeginDragItemCell);
        trigger.triggers.Add(beginDrag);
        //拖动中事件
        EventTrigger.Entry drag = new EventTrigger.Entry();
        drag.eventID = EventTriggerType.Drag;
        drag.callback.AddListener(DragItemCell);
        trigger.triggers.Add(drag);
        //结束拖动事件
        EventTrigger.Entry endDrag = new EventTrigger.Entry();
        endDrag.eventID = EventTriggerType.EndDrag;
        endDrag.callback.AddListener(EndDragItemCell);
        trigger.triggers.Add(endDrag);
    }

    #region 事件监听
    /// <summary>
    /// 开始拖动
    /// </summary>
    /// <param name="data"></param>
    void BeginDragItemCell(BaseEventData data)
    {
        EventCenter.GetInstance().EventTrigger<ItemCell>(Consts.EventCenter_Events.ItemCellBeginDrag, this);
    }

    /// <summary>
    /// 拖动中
    /// </summary>
    /// <param name="data"></param>
    void DragItemCell(BaseEventData data)
    {
        EventCenter.GetInstance().EventTrigger<BaseEventData>(Consts.EventCenter_Events.ItemCellDrag, data);
    }

    /// <summary>
    /// 结束拖动
    /// </summary>
    /// <param name="data"></param>
    void EndDragItemCell(BaseEventData data)
    {
        EventCenter.GetInstance().EventTrigger<ItemCell>(Consts.EventCenter_Events.ItemCellEndDrag, this);
    }

    /// <summary>
    /// 进入这个格子
    /// </summary>
    /// <param name="data"></param>
    void EnterItemCell(BaseEventData data)
    {
        EventCenter.GetInstance().EventTrigger<ItemCell>(Consts.EventCenter_Events.ItemCellEnter, this);

    }

    /// <summary>
    /// 移出这个格子
    /// </summary>
    /// <param name="data"></param>
    void ExitItemCell(BaseEventData data)
    {
        EventCenter.GetInstance().EventTrigger<ItemCell>(Consts.EventCenter_Events.ItemCellExit, this);

    }
    #endregion

    #endregion
}
