using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 尽量避免 使用Unity的生命周期函数 而是靠外界来调用开始与移除
/// </summary>
public interface IOtherObjectMono 
{
    /// <summary>
    /// 初始化
    /// </summary>
    void Init();

    /// <summary>
    /// 移除
    /// </summary>
    void Remove();
}
