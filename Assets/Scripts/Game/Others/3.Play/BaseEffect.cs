using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有特效的基类 都要根据对象池来实现
/// </summary>
public class BaseEffect : ReusbleObject
{
    protected float destoryTime = 1f;


    public override void OnSpawn()
    {
        //刚生成的时候你就要想什么时候结束自己
        Invoke("DestroyEff", destoryTime);
    }

    void DestroyEff()
    {
        PoolMgr.GetInstance().PushObj("Effects/Click/Click_Green", this.gameObject);
    }

    public override void OnUnspawn()
    {

    }
}
