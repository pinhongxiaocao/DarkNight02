using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LB_MVC
{
    /// <summary>
    /// 空接口 在本框架所有的事件参数 都必须继承他
    /// </summary>
    public interface LBMVC_Args
    {

    }

    /// <summary>
    /// 中间者
    /// </summary>
    public static class MVC
    {
        #region 存储MVC
        /// <summary>
        /// 模型名字-模型
        /// </summary>
        public static Dictionary<string, Model> Models = new Dictionary<string, Model>();

        /// <summary>
        /// 视图名字-视图
        /// </summary>
        public static Dictionary<string, View> Views = new Dictionary<string, View>();

        /// <summary>
        /// 由于控制器是动态生成 所以这里存放类型变量就可
        /// 事件名字-控制器类型
        /// </summary>
        public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

        #endregion

        #region 注册
        public static void RegisterModel(Model model)
        {
            Models[model.Name] = model;
        }

        public static void RegisterView(View view)
        {
            Views[view.Name] = view;
        }

        public static void RegisterController(string eventName, Type controllerType)
        {
            CommandMap[eventName] = controllerType;
        }
        #endregion

        #region 移除
        public static void RemoveView(View view)
        {
            //如果有这个视图的名字
            if (Views.ContainsKey(view.Name))
            {
                //我们就移除它
                Views.Remove(view.Name);
            }
            else
            {
                Debug.LogError($"没有{view.Name}视图可以让我们移除");
            }
        }
        #endregion

        #region 获取

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetModel<T>()
            where T : Model
        {
            foreach (Model m in Models.Values)
            {
                if (m is T)
                    return m as T;
            }
            //找不到对应类型只能返回空
            return null;
        }

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetView<T>()
        where T : View
        {
            foreach (View v in Views.Values)
            {
                if (v is T)
                    return v as T;
            }
            //找不到对应类型只能返回空
            return null;
        }
        #endregion

        #region 发送事件

        /// <summary>
        /// 发送事件 要有事件名 还要有事件参数
        /// 控制器和视图都可以响应事件 
        /// 优先1控制器 然后2视图
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        public static void SendEvent(string eventName, LBMVC_Args data = null)
        {
            //控制器响应事件 
            if (CommandMap.ContainsKey(eventName))
            {
                //这里开始 控制器已经和事件一一对应了
                Type t = CommandMap[eventName];
                //根据类型使用匹配度最高的构造函数 来生成实例
                Controller c = Activator.CreateInstance(t) as Controller;
                //控制器执行
                c.Execute(data);
            }

            //视图响应事件
            foreach (View v in Views.Values)
            {
                //如果有视图关心了这个事件 我们就处理他来
                if (v.AttationEvents.Contains(eventName))
                {
                    //视图响应事件
                    v.HandleEvent(eventName, data);
                }
            }
        }

        #endregion
    }
}


