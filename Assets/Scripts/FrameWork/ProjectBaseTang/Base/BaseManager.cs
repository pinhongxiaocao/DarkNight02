using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager <T>where T:new()//约束继承的必须要有一个无参构造函数
{
    private static T instance;


    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}
