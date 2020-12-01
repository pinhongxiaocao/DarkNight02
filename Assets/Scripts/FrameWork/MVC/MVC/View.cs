using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LB_MVC 
{
    /// <summary>
    /// 视图基类 
    /// 需要能 1获取模型 2发送事件 3处理事件
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        //视图标识
        public abstract string Name { get;}

        //事件监听
        /// <summary>
        /// 由于视图也可以处理事件 
        /// 视图来一个监听事件的表
        /// </summary>
        public List<string> AttationEvents = new List<string>();

        //处理事件
        /// <summary>
        /// 处理事件监听`
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        public abstract void HandleEvent(string eventName, LBMVC_Args data = null);

        //获取模型
        protected T GetModel<T>()
            where T : Model
        {
            return MVC.GetModel<T>();
        }

        //发送事件
        protected void SendEvent(string eventName, LBMVC_Args data = null)
        {
            //让中间者帮忙发消息
            MVC.SendEvent(eventName, data);
        }
    }

}
