using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//需要我们自己保证脚本的唯一性
public class SingletonMono<T> : MonoBehaviour 
   where T:MonoBehaviour
{
   private static T instance;

   public static T GetInstance()
   {
      //继承了Mono的不能直接new
      //都是直接加脚本 或者代码来加
      return instance;
   }

   protected virtual void Awake()
   {
      instance = this as T;
   }
}
