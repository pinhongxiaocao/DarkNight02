using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LB_MVC
{
    /// <summary>
    /// 控制器基类  1处理事件 2获取模型 3获取视图
    /// 4注册视图 5注册模型 (一般用在游戏开始事件中) 6注册事件
    /// 一个控制器 对应一个事件
    /// </summary>
    public abstract class Controller
    {
        /// <summary>
        /// 处理事件监听
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        public abstract void Execute(LBMVC_Args data = null);

        //获取视图
        protected T GetView<T>()
        where T : View
        {
            return MVC.GetView<T>();
        }

        //获取模型
        protected T GetModel<T>()
            where T : Model
        {
            return MVC.GetModel<T>();
        }

        #region 注册三个部分 M V C
        protected void RegisterModel(Model model)
        {
            MVC.RegisterModel(model);
        }

        protected void RegisterView(View view)
        {
            MVC.RegisterView(view);
        }

        /// <summary>
        /// 注册控制器
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="controllerType">控制器的类型</param>
        protected void RegisterController(string name, Type controllerType)
        {
            MVC.RegisterController(name, controllerType);
        }
        #endregion
    }
}


