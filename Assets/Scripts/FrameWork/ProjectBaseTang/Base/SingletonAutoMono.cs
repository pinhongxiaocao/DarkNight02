using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自动创建的单例模式 一般应该是管理器
public class SingletonAutoMono<T> : MonoBehaviour 
    where T:MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        //继承了Mono的不能直接new
        //都是直接加脚本 或者代码来加
        if (instance == null)
        {
            //第一次直接生成一个物体
            GameObject obj=new GameObject();
            //保护他过场景
            DontDestroyOnLoad(obj);
            //修改名字
            obj.name = typeof(T).ToString();
            //加脚本赋值
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
}