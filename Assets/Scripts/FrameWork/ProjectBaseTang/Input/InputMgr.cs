using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManager<InputMgr>
{
    private bool isStart = false;

    /// <summary>
    /// 构造函数里面添加Update监听
    /// </summary>
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    /// <summary>
    /// 用来监测按键抬起 按下 分发事件
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            //事件中心模块 告诉某键按下
            EventCenter.GetInstance().EventTrigger("某键按下", key);
        }
        if (Input.GetKeyUp(key))
        {
            //事件中心模块 告诉某键抬起
            EventCenter.GetInstance().EventTrigger("某键抬起", key);
        }
    }
    /// <summary>
    /// 是否开启或关闭我的输入监测
    /// </summary>
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    private void Update()
    {
        //没有开启输入监测就不去监测
        if (!isStart)
        {
            return;
        }
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
    }
}
