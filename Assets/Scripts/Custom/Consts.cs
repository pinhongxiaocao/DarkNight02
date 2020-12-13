using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consts
{
    /// <summary>
    /// 一些特效
    /// </summary>
    public static class Effects 
    {
        public static string GreenClickEffect = "GreenClickEffect";
    }

    /// <summary>
    /// 一些标签
    /// </summary>
    public static class Tags 
    {
        public static string Player = "Player";
        public static string NPC = "NPC";
        public static string Ground = "Ground";
    }

    /// <summary>
    /// 任务种类
    /// </summary>
    public enum E_Quest_Type
    {
        None = 0,
        Gathering,//击杀类任务
        Talk,//谈话类任务
        Reach//探险类任务
    }

    /// <summary>
    /// 任务状态 TODO状态机
    /// </summary>
    public enum E_Quest_Status
    {
        Waiting,//等待 未接收状态
        Accepted,//接收状态 但是未完成
        Completed,//已完成状态
    }

    /// <summary>
    /// 背包的页签枚举
    /// </summary>
    public enum E_Bag_Type
    {
        None=0,
        Item,
        Equip,
        Gem
    }

    /// <summary>
    /// 格子的类型 0代表的是背包
    /// 后面1-6代表的是装备
    /// </summary>
    public enum E_Item_Type 
    {
        Bag=0,
        Head,
        Neck,
        Weapon,
        Cloth,
        Trousers,
        Shoes,
    }

    /// <summary>
    /// MVC框架下的事件名
    /// </summary>
    public static class MVCEvents_Name
    {
        public static string StartGame = "StartGame";
        public static string GoToSelectScene = "GoToSelectScene";
        public static string GoToPlayScene = "GoToPlayScene";

    }

    /// <summary>
    /// MVC框架外 事件中心下的事件名
    /// </summary>
    public static class EventCenter_Events
    {
        #region  第一个开始场景的事件
        public static string StartGame = "StartGame";
        #endregion

        #region 第二个角色选择场景的事件
        public static string CreateCharacter = "CreateCharacter";

        /// <summary>
        /// 按按钮切换角色显示 一个int参数 1表示往后切 -1表示往前切
        /// </summary>
        public static string SwitchCharacter = "SwitchCharacter";

        #endregion

        #region 第三个游戏场景的事件

        /// <summary>
        /// 注册背包面板
        /// </summary>
        public static string RegisterBagPanel = "RegisterBagPanel";

        //********背包系统相关的事件(参数除了拖动中全为ItemCell)**********//
        /// <summary>
        /// 开始拖格子
        /// </summary>
        public static string ItemCellBeginDrag = "ItemCellBeginDrag";
        /// <summary>
        /// 正在拖动格子
        /// </summary>
        public static string ItemCellDrag = "ItemCellDrag";
        /// <summary>
        /// 结束了拖格子
        /// </summary>
        public static string ItemCellEndDrag = "ItemCellEndDrag";

        /// <summary>
        /// 进入了格子
        /// </summary>
        public static string ItemCellEnter= "ItemCellEnter";
        /// <summary>
        /// 离开了格子
        /// </summary>
        public static string ItemCellExit = "ItemCellExit";

        //****************************************************//

        //*********任务系统的事件************//

        /// <summary>
        /// 接受任务
        /// </summary>
        public static string AcceptQuest = "AcceptQuest";
        /// <summary>
        /// 完成任务
        /// </summary>
        public static string CompleteQuest = "CompleteQuest";
        //**********************************//

        #endregion
    }

    /// <summary>
    /// 控制器的名字
    /// </summary>
    public static class MVCCommand_Name
    {
        public static string StartGameCommand = "StartGameCommand";
        public static string GoToSelectSceneCommand = "GoToSelectSceneCommand";
        public static string GoToPlaySceneCommand = "GoToPlaySceneCommand";
    }

    /// <summary>
    /// 视图的名字 这里大部分指的是UI面板
    /// </summary>
    public static class MVCView_Name
    {
        #region UI面板
        /// <summary>
        /// 所有面板的基类
        /// </summary>
        public static string BasePanel = "BasePanel";

        /// <summary>
        /// 第一个场景游戏刚开始的面板
        /// </summary>
        public static string StartPanel = "StartPanel";

        /// <summary>
        /// 第二个场景角色选择的面板
        /// </summary>
        public static string CharacterSelectPanel = "CharacterSelectPanel";

        /// <summary>
        /// 第三个场景的主面板 一直跟着游戏人物移动
        /// </summary>
        public static string MainPanel = "MainPanel";

        /// <summary>
        /// 第三个场景的背包面板 用来显示背包
        /// </summary>
        public static string BagPanel = "BagPanel";

        /// <summary>
        /// 第三个场景的角色面板 用来显示角色属性
        /// </summary>
        public static string EquipPanel = "EquipPanel";

        /// <summary>
        /// 第三个场景的任务面板 用于显示角色的任务
        /// </summary>
        public static string QuestPanel = "QuestPanel";

        #endregion
    }

    public static class MVCModel_Name
    {
        /// <summary>
        /// 玩家的数据信息
        /// </summary>
        public static string PlayerModel = "PlayerModel";
    }
}




