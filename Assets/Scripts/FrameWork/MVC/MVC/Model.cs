using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LB_MVC
{
    /// <summary>
    /// 模型基类 无需增加V和C的引用 只需要发送消息即可
    /// </summary>
    public abstract class Model
    {
        public abstract string Name { get; }

        protected void SendEvent(string eventName, LBMVC_Args data = null)
        {
            //让中间者帮忙发消息
            MVC.SendEvent(eventName, data);
        }
    }
}


