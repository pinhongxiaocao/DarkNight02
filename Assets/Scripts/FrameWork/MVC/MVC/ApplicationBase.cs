using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LB_MVC 
{
    /// <summary>
    /// 程序入口 继承不自动创建的Mono单例基类
    /// 1能注册控制器 2能发送事件 都是为了游戏开启 3注册视图
    /// </summary>
    public abstract class ApplicationBase<T> : SingletonMono<T>
        where T : MonoBehaviour
    {
        /// <summary>
        /// 注册控制器 然后让第一个被注册的控制器 去注册其他
        /// </summary>
        /// <param name="name"></param>
        /// <param name="controllerType"></param>
        protected void RegisterController(string name, Type controllerType)
        {
            MVC.RegisterController(name, controllerType);
        }


        
        /// <summary>
        ///  发送事件告诉大家 游戏开启了 不需要参数
        /// </summary>
        /// <param name="eventName"></param>
        protected void SendEvent(string eventName)
        {
            MVC.SendEvent(eventName);
        }

    }
}


