using UnityEngine;
using System.Collections;

public class MovieCamera : MonoBehaviour,IOtherObjectMono {

    /// <summary>
    /// 摄像机移动速度
    /// </summary>
    private float speed = 10;

    /// <summary>
    /// 目的地的Z轴
    /// </summary>
    private float endZ = -20;


    public void Init()
    {

        //再添加Update的监听
        MonoMgr.GetInstance().AddUpdateListener(Move);
    }

    public void Remove()
    {
        //先移除Update的监听
        MonoMgr.GetInstance().RemoveUpdateListener(Move);
    }

    /// <summary>
    /// 摄像机移动
    /// </summary>
    private void Move() 
    {
        if (transform.position.z < endZ)
        {   
            //还没有达到目标位置，需要移动
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
